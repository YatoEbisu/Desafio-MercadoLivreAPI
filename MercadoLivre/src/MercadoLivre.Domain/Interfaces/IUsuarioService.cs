using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task Insert(Usuario obj);
        Task<Usuario> VerifyLogin(string login, string senha);
    }
}