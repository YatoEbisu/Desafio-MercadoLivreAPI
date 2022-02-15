using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Service.Notifications;
using MercadoLivre.Service.Services.Base;
using MercadoLivre.Service.Utils;
using MercadoLivre.Service.Validations;
using Microsoft.AspNetCore.Server.IIS.Core;
using Newtonsoft.Json;

namespace MercadoLivre.Service.Services
{
    public class PagamentoService : BaseService<Pagamento>, IPagamentoService
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IProdutoCompraRepository _produtoCompraRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public PagamentoService(INotifier notifier, AuthenticatedUser user, IPagamentoRepository pagamentoRepository, IProdutoCompraRepository produtoCompraRepository, IProdutoRepository produtoRepository, IUsuarioRepository usuarioRepository) : base(notifier, user)
        {
            _pagamentoRepository = pagamentoRepository;
            _produtoCompraRepository = produtoCompraRepository;
            _produtoRepository = produtoRepository;
            _usuarioRepository = usuarioRepository;
        }
        public async Task Insert(Pagamento obj)
        {
            var usuarioLogado = await _usuarioRepository.FindOneAsync(y => y.Login.ToLower() == _user.Email.ToLower());

            if (!Validate(obj, Activator.CreateInstance<PagamentoValidation>()))
                return;

            var pagamento = await _pagamentoRepository.FindOneAsync(y => y.TransacaoId == obj.TransacaoId);
            if (pagamento != null && pagamento.Status == StatusCompra.Sucesso)
            {
                Notify("Essa transação ja teve sucesso");
                return;
            }

            var produtoCompra = await _produtoCompraRepository.FindOneAsync(y => y.Id == obj.ProdutoCompraId);
            var produto = await _produtoRepository.FindOneAsync(y => y.Id == produtoCompra.ProdutoId);

            var usuarioProduto = await _usuarioRepository.FindOneAsync(y => y.Id == produto.UsuarioId);

            if (obj.Status == StatusCompra.Falha)
            {
                await _pagamentoRepository.Insert(obj);

                string link = "";
                switch (produtoCompra.Gateway)
                {
                    case GatewayType.Paypal:
                        link = $"https://paypal.com/{produtoCompra.Id}?redirectUrl=/api/Produto/{produto.Id}/Buy";
                        break;

                    case GatewayType.PagSeguro:
                        link = $"https://pagseguro.uol.com.br/?returnId={produtoCompra.Id}&redirectUrl=/api/Produto/{produto.Id}/Buy";
                        break;
                }
                var emailErro = new Email(
                    usuarioLogado.Login,
                    "Erro ao processar pagamento",
                    "Houve um erro ao processor o pagamento, acesse este link para tentar novamente " + link
                );

                emailErro.SendEmail();
                return;
            }

            var notaFiscal = new
            {
                ProdutoCompraId = obj.ProdutoCompraId,
                UsuarioId = usuarioLogado.Id
            };

            var ranking = new
            {
                ProdutoCompraId = obj.ProdutoCompraId,
                UsuarioId = usuarioProduto.Id
            };

            #region Ignore SSL
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            #endregion

            using (var httpClient = new HttpClient(handler))
            {
                var notaFiscalResult = await httpClient.PostAsync("https://localhost:44342/api/Fake/NotaFical",
                    new StringContent(JsonConvert.SerializeObject(notaFiscal), Encoding.UTF8, "application/json"));

                if (!notaFiscalResult.IsSuccessStatusCode)
                {
                    Notify("Erro na geração da nota fiscal");
                    return;
                }

                var rankingResult = await httpClient.PostAsync("https://localhost:44342/api/fake/ranking",
                    new StringContent(JsonConvert.SerializeObject(ranking), Encoding.UTF8, "application/json"));

                if (!rankingResult.IsSuccessStatusCode)
                {
                    Notify("Erro ao alterar ranking do vendedor");
                    return;
                }
            }

            List<string> caracteristicas = new List<string>();
            produto.Caracteristicas.ForEach(y => caracteristicas.Add($"{y.Nome}: {y.Descricao} \n"));

            var mensagem = @$"Sua compra foi efeteuada com sucesso 
Email do vendendor: {usuarioProduto.Login}
Produto: {produto.Nome}
Descricão: {produto.Descricao}
";

            for (int i = 0; i < caracteristicas.Count; i++)
            {
                mensagem = mensagem + "\n" + caracteristicas[i];
            }
            var email = new Email(
                usuarioLogado.Login,
                "Compra confirmada",
                mensagem);

            email.SendEmail();

            obj.Status = StatusCompra.Sucesso;
            
            await _pagamentoRepository.Insert(obj);

            produtoCompra.Status = StatusType.Completada;
            await _produtoCompraRepository.Update(produtoCompra);
        }
    }
}