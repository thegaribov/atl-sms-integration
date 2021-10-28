using Core.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Repositories.Base
{
    public interface IRepository<TEntity> 
        where TEntity: class, IEntity, new()
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(object id);
        Task CreateAsync(TEntity data);
        Task UpdateAsync(TEntity data);
        Task DeleteAsync(TEntity data);
    }
}
