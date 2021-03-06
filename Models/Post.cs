using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Models
{
    public class Post //QUESTA È LA TABELLA LATO N DELLA RELAZIONE
    {
        public int Id { get; set; }  // Chiave Primaria

        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdateTime { get; set; }

        //Il seguente campo E' la chiave esterna.
        //Ci indica l'autore del post
        public string UserSsn { get; set; } // Chiave Esterna dell'Utente

    }
}