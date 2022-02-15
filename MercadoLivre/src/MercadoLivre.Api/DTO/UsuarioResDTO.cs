namespace MercadoLivre.Api.DTO
{
    public class UsuarioResDTO
    {
        public UsuarioResDTO(string login)
        {
            Login = login;
        }
        public string Login { get; set; }
    }
}