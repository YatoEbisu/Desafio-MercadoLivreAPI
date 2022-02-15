using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoLivre.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeController : ControllerBase
    {
        [HttpPost("NotaFical")]
        public IActionResult Notafical(object nf)
        {
            return Ok();
        }

        [HttpPost("Ranking")]
        public IActionResult Ranking(object ranking)
        {
            return Ok();
        }
    }
}
