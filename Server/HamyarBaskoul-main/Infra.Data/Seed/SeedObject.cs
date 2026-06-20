using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Seed
{
    internal class SeedObject
    {
        private readonly ModelBuilder modelBuilder;
        public SeedObject(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }
        public void Seed()
        {
            modelBuilder.Entity<ObjectForm>().HasData(
                new ObjectForm { Id = 1,Name = "Applicant", NameFarsi = "مشاهده متقاضی" ,GroupName = "Applicant", GroupNameFarsi= "متقاضی" , Departement = "realestate" , UserName = "user1"},
             
				new ObjectForm { Id = 2, Name = "FileRebuildability", NameFarsi = "پروفایل بازسازی", GroupName = "Form", GroupNameFarsi = "فرم", Departement = "design" , UserName = "user2" }


			);
           



        }
    }
}
