using System.Collections.Generic;
using AutoMapper;
using MercadoLivre.Api.DTO;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Api.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Usuario, UsuarioReqDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaReqDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaResDTO>().ReverseMap();
            CreateMap<Produto, ProdutoReqDTO>().ReverseMap();
            CreateMap<Produto, ProdutoResDTO>().ReverseMap();
            CreateMap<Caracteristica, CaracteristicaDTO>().ReverseMap();
            CreateMap<Caracteristica, CaracteristicaResDTO>().ReverseMap();
            CreateMap<ProdutoImagem, ProdutoImagemResDTO>().ReverseMap();
            CreateMap<ProdutoOpiniao, ProdutoOpiniaoReqDTO>().ReverseMap();
            CreateMap<ProdutoOpiniao, ProdutoOpiniaoResDTO>().ReverseMap();
            CreateMap<ProdutoPergunta, ProdutoPerguntaReqDTO>().ReverseMap();
            CreateMap<ProdutoPergunta, ProdutoPerguntaResDTO>().ReverseMap();
            CreateMap<ProdutoCompra, ProdutoCompraReqDTO>().ReverseMap();
            CreateMap<Pagamento, PagamentoReqDTO>().ReverseMap();
        }
    }
}