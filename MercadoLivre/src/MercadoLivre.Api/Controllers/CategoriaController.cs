using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MercadoLivre.Api.DTO;
using MercadoLivre.Domain.Entities;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Service.Notifications;

namespace MercadoLivre.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : BaseController
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;
        public CategoriaController(INotifier notifier, ICategoriaService categoriaService, IMapper mapper) : base(notifier)
        {
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CategoriaReqDTO categoriaReqDTO)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var categoria = _mapper.Map<Categoria>(categoriaReqDTO);

                await _categoriaService.Insert(categoria);

                return CustomResponseCreated(nameof(Create), categoriaReqDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
