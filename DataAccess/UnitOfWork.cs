using Core.DataAccess;
using DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ATLSmsContext _context;

        public UnitOfWork(ATLSmsContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
