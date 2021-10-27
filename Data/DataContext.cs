using Microsoft.EntityFrameworkCore;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> opzioni) : base(opzioni)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }






    }
}
