using MadPay24.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MadPay24.Data.DatabaseContext
{
    class MadpayDbContext:DbContext
    {
        //Connection String
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Madpay24Db;integrated Security=True;MultipleActiveResultSets=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BankCard> BankCards { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
