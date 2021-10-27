using Microsoft.EntityFrameworkCore;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Data
{
    //Per Convenzione questa classe la chiamamo DataContext,
    //Essa deve estendere(diventare figlia) della classe DbContext di EntityFramwork
    public class DataContext : DbContext
    {
        //opzioni è un oggetto di tipo DbContextOptions<DataContext> opzioni
        //l' oggetto opzioni deve essere passato al costruttore della classe padre
        public DataContext(DbContextOptions<DataContext> opzioni) : base(opzioni)
        {
           
        }

        //i DbSet sono le praticamente le tabelle lato MySQL
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }






    }
}
