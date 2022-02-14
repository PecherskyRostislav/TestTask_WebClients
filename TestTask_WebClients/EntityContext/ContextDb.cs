using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask_WebClients.Models;

namespace TestTask_WebClients.EntityContext
{
    public class ContextDb : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public ContextDb(DbContextOptions<ContextDb> options)
           : base(options)
        {
        }
    }
}
