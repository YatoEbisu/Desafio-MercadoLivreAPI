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
using MercadoLivre.Service.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MercadoLivre.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper, INotifier _notifier) : base(_notifier)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UsuarioReqDTO usuarioReqDTO)
        {
            var usuario = await _usuarioService.VerifyLogin(usuarioReqDTO.Login, usuarioReqDTO.Senha);
            if (usuario == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = Token.GenerateToken(usuario);

            return CustomResponse(new
            {
                usuario = new UsuarioResDTO(usuario.Login),
                token = token
            });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UsuarioReqDTO usuarioDTO)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var usuario = _mapper.Map<Usuario>(usuarioDTO);

                await _usuarioService.Insert(usuario);

                return CustomResponseCreated(nameof(Create), new UsuarioResDTO(usuario.Login));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
