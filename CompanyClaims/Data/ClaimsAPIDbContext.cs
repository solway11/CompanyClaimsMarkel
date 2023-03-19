using Microsoft.EntityFrameworkCore;
using CompanyClaimsApi.Models;

namespace CompanyClaimsApi.Data
{
        public class ClaimsAPIDbContext : DbContext
        {
        public ClaimsAPIDbContext(DbContextOptions options) : base(options)
            {

            }
            public DbSet<Claims> Claims { get; set; }
            public DbSet<Company> Company { get; set; }
    }
    }

