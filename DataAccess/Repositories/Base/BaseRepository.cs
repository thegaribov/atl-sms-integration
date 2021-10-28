using Core.DataAccess.Repositories.Base;
using Core.Entities.Abstraction;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Base
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly ATLSmsContext _db;
        private readonly DbSet<TEntity> _dbTable;

        public BaseRepository(ATLSmsContext db)
        {
            _db = db;
            _dbTable = db.Set<TEntity>();
        }

        public async virtual Task<List<TEntity>> GetAllAsync()
        {
            return await _dbTable.ToListAsync();
        }

        public async virtual Task<TEntity> GetAsync(object id)
        {
            return await _dbTable.FindAsync(id);
        }

        public async virtual Task CreateAsync(TEntity data)
        {
            await _dbTable.AddAsync(data);
        }

        public async virtual Task UpdateAsync(TEntity data)
        {
            _dbTable.Attach(data);
            _db.Entry(data).State = EntityState.Modified;
        }

        public async virtual Task DeleteAsync(TEntity data)
        {
            _dbTable.Remove(data);
        }
    }
}
