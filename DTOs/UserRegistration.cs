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
    public class UserRegistration
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Pob { get; set; } //Place of Birth
        public string Address { get; set; } //Indirizzo di Residenza
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
