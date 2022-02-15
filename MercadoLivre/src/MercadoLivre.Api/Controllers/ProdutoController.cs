using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MercadoLivre.Api.DTO;
using MercadoLivre.Domain.Entities;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Service.Notifications;
using Microsoft.AspNetCore.Authorization;

namespace MercadoLivre.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : BaseController
    {
        private readonly IProdutoService _produtoService;
        private readonly IProdutoImagemService _produtoImagemService;
        private readonly IProdutoOpiniaoService _produtoOpiniaoService;
        private readonly IProdutoPerguntaService _produtoPerguntaService;
        private readonly IProdutoCompraService _produtoCompraService;
        private readonly IPagamentoService _pagamentoService;
        private readonly IMapper _mapper;
        public ProdutoController(INotifier notifier, IProdutoService produtoService, IProdutoImagemService produtoImagemService, IProdutoOpiniaoService produtoOpiniaoService, IProdutoPerguntaService produtoPerguntaService, IProdutoCompraService produtoCompraService, IPagamentoService pagamentoService, IMapper mapper) : base(notifier)
        {
            _produtoService = produtoService;
            _produtoImagemService = produtoImagemService;
            _produtoOpiniaoService = produtoOpiniaoService;
            _produtoPerguntaService = produtoPerguntaService;
            _produtoCompraService = produtoCompraService;
            _pagamentoService = pagamentoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("{Id}")]
        public async Task<IActionResult> Detail([FromRoute] Guid Id)
        {
            try
            {
                var produto = await _produtoService.Get(Id);

                return CustomResponse(_mapper.Map<ProdutoResDTO>(produto));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }        
        
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProdutoReqDTO produtoReqDTO)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var produto = _mapper.Map<Produto>(produtoReqDTO);

                await _produtoService.Insert(produto);

                return CustomResponseCreated(nameof(Create), produtoReqDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{ProdutoId:guid}/AddImage")]
        public async Task<IActionResult> Create([FromRoute] Guid ProdutoId, [FromForm] List<IFormFile> files)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                if (files.Count() == 0 )
                {
                    NotifyError("Forneça uma imagem para este produto!");
                    return CustomResponse();
                }

                var imagens = new List<ProdutoImagem>();

                files.ForEach(file => imagens.Add(new ProdutoImagem(file.FileName, "images/" + file.FileName, file.ContentType, ProdutoId)));

                var result = await _produtoImagemService.InsertMany(imagens, ProdutoId);

                if (result == HttpStatusCode.Forbidden) 
                    return Forbid();

                if (result == HttpStatusCode.BadRequest)
                    return CustomResponse();

                var imagensResDTO = new List<ProdutoImagemResDTO>();
                imagens.ForEach(y => imagensResDTO.Add(_mapper.Map<ProdutoImagemResDTO>(y)));

                return CustomResponseCreated(nameof(Create), imagensResDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{ProdutoId:guid}/Opinion")]
        public async Task<IActionResult> Opinion([FromRoute] Guid ProdutoId, [FromBody] ProdutoOpiniaoReqDTO produtoOpiniaoReqDto)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var produtoOpiniao = _mapper.Map<ProdutoOpiniao>(produtoOpiniaoReqDto);
                produtoOpiniao.ProdutoId = ProdutoId;

                await _produtoOpiniaoService.Insert(produtoOpiniao);
                return CustomResponseCreated(nameof(Create), _mapper.Map<ProdutoOpiniaoResDTO>(produtoOpiniao));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{ProdutoId:guid}/Question")]
        public async Task<IActionResult> Question([FromRoute] Guid ProdutoId, [FromBody] ProdutoPerguntaReqDTO produtoPerguntaReqDTO)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var produtoPergunta = _mapper.Map<ProdutoPergunta>(produtoPerguntaReqDTO);
                produtoPergunta.ProdutoId = ProdutoId;

                await _produtoPerguntaService.Insert(produtoPergunta);
                var produtoPerguntaList = await _produtoPerguntaService.FindAllByProduto(ProdutoId);

                var produtoPerguntaDTOList = new List<ProdutoPerguntaResDTO>();
                produtoPerguntaList.ForEach(y => produtoPerguntaDTOList.Add(_mapper.Map<ProdutoPerguntaResDTO>(y)));

                return CustomResponseCreated(nameof(Create), produtoPerguntaDTOList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{ProdutoId:guid}/Buy")]
        public async Task<IActionResult> Buy([FromRoute] Guid ProdutoId, [FromBody] ProdutoCompraReqDTO produtoCompraReqDTO)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                if (!Enum.IsDefined(typeof(GatewayType), produtoCompraReqDTO.Gateway))
                {
                    NotifyError("Gateway inválido");
                    return CustomResponse();
                }

                var produtoCompra = _mapper.Map<ProdutoCompra>(produtoCompraReqDTO);
                produtoCompra.ProdutoId = ProdutoId;

                if (!await _produtoCompraService.Insert(produtoCompra))
                    return CustomResponse();


                switch (produtoCompra.Gateway)
                {
                    case GatewayType.Paypal:
                        return Redirect($"https://paypal.com/{produtoCompra.Id}?redirectUrl=/api/Produto/{ProdutoId}/Buy");

                    case GatewayType.PagSeguro:
                        return Redirect($"https://pagseguro.uol.com.br/?returnId={produtoCompra.Id}&redirectUrl=/api/Produto/{ProdutoId}/Buy");
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{ProdutoId:guid}/Buy/Payment")]
        public async Task<IActionResult> Payment([FromBody] PagamentoReqDTO pagamentoReqDTO)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                if (int.TryParse(pagamentoReqDTO._Status, out int x))
                {
                    if (x == 1)
                        pagamentoReqDTO.Status = StatusCompra.Sucesso;
                    else
                        pagamentoReqDTO.Status = StatusCompra.Falha;
                }
                else
                {
                    if (pagamentoReqDTO._Status == "SUCESSO")
                        pagamentoReqDTO.Status = StatusCompra.Sucesso;
                    else
                        pagamentoReqDTO.Status = StatusCompra.Falha;
                }

                await _pagamentoService.Insert(_mapper.Map<Pagamento>(pagamentoReqDTO));

                return CustomResponse();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
