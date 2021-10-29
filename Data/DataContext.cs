using Microsoft.EntityFrameworkCore;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Data
{
    //Per Convenzione di Entity Framework, questa classe la chiamamo DataContext,
    //Essa deve estendere(diventare figlia) della classe DbContext di Entity Framwork
    public class DataContext : DbContext
    {

        //NEI PARAMETRI DEL METODO COSTRUTTORE

        // "opzioni" è un oggetto di tipo DbContextOptions<DataContext>

        //l' oggetto opzioni deve essere passato al costruttore della classe padre (passato al base)
        public DataContext(DbContextOptions<DataContext> opzioni) : base(opzioni)
        {
           
        }

        //i DbSet scritti sotto forma di proprietà sono le praticamente le tabelle lato MySQL (le scriviamo al plurale un po' come per SQL)
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }






    }
}
