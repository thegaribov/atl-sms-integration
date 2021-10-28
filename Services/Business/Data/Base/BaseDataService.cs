using Core.DataAccess.Repositories.Base;
using Core.Entities.Abstraction;
using Core.Services.Business.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Business.Data.Base
{
    public class BaseDataService<TEntity, TRepository> : IDataService<TEntity>
        where TEntity : class, IEntity, new()
        where TRepository : class, IRepository<TEntity>
    {
        protected readonly TRepository _repository;

        public BaseDataService(TRepository repository)
        {
            _repository = repository;
        }

        public async virtual Task<List<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async virtual Task<TEntity> GetAsync(object id)
        {
            return await _repository.GetAsync(id);
        }

        public async virtual Task CreateAsync(TEntity data)
        {
            await _repository.CreateAsync(data);
        }

        public async virtual Task UpdateAsync(TEntity data)
        {
            await _repository.UpdateAsync(data);
        }

        public async virtual Task DeleteAsync(TEntity data)
        {
            await _repository.DeleteAsync(data);
        }
    }
}
