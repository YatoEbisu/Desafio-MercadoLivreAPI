using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MercadoLivre.Api.DTO
{
    public class ProdutoOpiniaoResDTO
    {
        public int Nota { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
