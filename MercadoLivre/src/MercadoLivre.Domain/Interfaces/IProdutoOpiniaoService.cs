using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Domain.Interfaces
{
    public interface IProdutoOpiniaoService
    {
        Task Insert(ProdutoOpiniao obj);
    }
}