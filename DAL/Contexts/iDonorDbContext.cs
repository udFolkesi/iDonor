using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contexts
{
    public class iDonorDbContext : DbContext
    {
        public iDonorDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
