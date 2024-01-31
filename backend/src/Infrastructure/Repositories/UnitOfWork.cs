using System;
using System.Collections;
using System.Threading.Tasks;
using Ecommerce.Application.Abstracts;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly EcommerceDBContext _context;

        public UnitOfWork(EcommerceDBContext context)
        {
            _context = context;
        }

        public async Task<int> Completed()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error en transaccion", e);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if(_repositories is null){
                _repositories = new Hashtable();
            }
            var type = typeof(TEntity).Name;

            if(!_repositories.ContainsKey(type)){
                var RepositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator
                        .CreateInstance(RepositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}