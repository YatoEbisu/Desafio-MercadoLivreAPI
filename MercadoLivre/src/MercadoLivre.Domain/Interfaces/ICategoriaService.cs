using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Domain.Interfaces
{
    public interface ICategoriaService
    {
        Task Insert(Categoria obj);
    }
}