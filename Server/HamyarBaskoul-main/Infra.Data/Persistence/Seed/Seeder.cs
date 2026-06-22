using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Seed
{
    internal class Seeder
    {
        private readonly ModelBuilder modelBuilder;
        public Seeder(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void Seed()
        {
            modelBuilder.Entity<BaseTable>().HasData(
                new BaseTable { Id = 1, GroupName = "TransactionType", ValueFarsi = "فروش", IsDeleted = false },
             
                new BaseTable { Id = 2, GroupName = "RequestPropertyUsage", ValueFarsi = "سایر", IsDeleted = false }

                
            );
        

        }
    }
}

