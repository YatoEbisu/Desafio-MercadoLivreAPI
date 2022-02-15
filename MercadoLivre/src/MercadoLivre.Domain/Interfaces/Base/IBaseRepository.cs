using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task Insert(T obj);
        Task Update(T obj);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindOneAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExistsInDatabaseAsync(Expression<Func<T, bool>> predicate);
    }
}