using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Business.Data.Base
{
    public interface IDataService<TEntity>
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(object id);
        Task CreateAsync(TEntity data);
        Task UpdateAsync(TEntity data);
        Task DeleteAsync(TEntity data);
    }
}
