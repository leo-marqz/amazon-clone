using System;
using System.Threading.Tasks;

namespace Ecommerce.Application.Abstracts
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> Completed();
    }
}