using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Database
{
    public class DeparterContext : DbContext
    {
        public DeparterContext(DbContextOptions<DeparterContext> options) : base(options)
        {
        }
    }
}
