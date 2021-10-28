using ProjectWork.Data;
using ProjectWork.DTOs;
using ProjectWork.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ProjectWork.Services
{
    public class AuthService : IAuthService
    {

        private readonly DataContext _db;

        public AuthService(DataContext db)
        {
            _db = db;
        }






        //Metodo per effettuare Registrazione di un nuovo Utente
        public bool Registester(UserRegistrationRequest request)
        {
            
            var (hash, salt) = HashPassword(request.Password);

            User u = new User
            {

                FirstName = request.FirstName,
                LastName = request.LastName,
                Dob = request.Dob,
                Gender = request.Gender,
                Pob = request.Pob,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                HashedPassword = hash,
                PasswordSalt = salt,
                

            };

            u.Ssn=u.CreateSsn();

            if (UserExists(u.Ssn))
            {
                return false; //Registrazione Non Avvenuta
            }

            _db.Users.Add(u);

            _db.SaveChanges();

            return true;

        }

        //Metodo che dato un codice fiscale controlla se già vi è uno esistente
        private bool UserExists(string ssn)
        {

            return _db.Users.Any(utente => utente.Ssn == ssn);
        }

        //Metodo che data una password restituisce una Tupla(insieme di valori)rappresentanti
        //gli array di byte della password e del sale
        private (byte[] passwordHash, byte[] passwordSalt) HashPassword(string password)
        {
            using (var hms = new HMACSHA512()) //Facciamo uno using interno per non fare hms.Dispose()
            {


                var salt = hms.Key; //Prendiamo il sale che corrisponde alla chiave dell'algoritmo

                //Creaiamo una variabile, all'interno
                //Calcoliamo l hash del nostro oggetto passandogli
                //il Buffer della nostra password data in input
                var hash = hms.ComputeHash(Encoding.UTF8.GetBytes(password));


                return (hash, salt); //Restituiamo la Tupla(byte[] ... ,byte[])


            }

        }

        //Metodo per effettuare l'accesso
        public string Login(string ssn, string password)
        {
            throw new NotImplementedException();
        }


    }
}
