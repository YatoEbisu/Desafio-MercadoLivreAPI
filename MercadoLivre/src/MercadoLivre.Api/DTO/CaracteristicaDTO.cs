using System.ComponentModel.DataAnnotations;

namespace MercadoLivre.Api.DTO
{
    public class CaracteristicaDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Descricao { get; set; }
    }
}