namespace MercadoLivre.Api.DTO
{
    public class CategoriaResDTO
    {
        public string Nome { get; set; }
        public CategoriaResDTO CategoriaMae { get; set; }
    }
}