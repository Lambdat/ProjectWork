using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.DTOs
{
    //Un DTO è una Data Transfer Object
    //ossia un oggetto che si occupa di trasferire dal client al server
    //delle informazioni

    //In quesuto caso i dati in fase di registrazione di un nuovo utente
    //all'interno sono presenti quasi tutte le proprietà della classe User
    //eccetto la password che non sarà più di tipo byte[]
    //bensì sarà proprio la password stringa che inserisce
    public class UserRegistrationRequest
    {


        public User User { get; set; }
        public string Password { get; set; }









    }
}
