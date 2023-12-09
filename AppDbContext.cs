using Microsoft.EntityFrameworkCore;
using FinalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace FinalTask
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Item> items {  get; set; }
        public AppDbContext() 
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;username=root;password=root;database=finaltask");
        }
    }
}
