using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Abstraction
{
    public interface ICreatedAt
    {
        public DateTime CreatedAt { get; set; }
    }
}
