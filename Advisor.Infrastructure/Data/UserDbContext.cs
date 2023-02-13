using Advisor.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        public DbSet<Users>? Usersd { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<InvestorInfo>? InvestorInfos { get; set; }
        public DbSet<InvestmentType>? InvestmentTypes { get; set; }
        public DbSet<InvestmentStrategy>? investmentStrategies { get; set; }

        public DbSet<AdvisorInfo>? AdvisorInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>()
                .Property(u => u.SortName)
                .HasComputedColumnSql("[LastName] + ' ' + [FirstName]");
        }

    }
}
