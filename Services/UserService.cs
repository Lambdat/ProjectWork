using Microsoft.EntityFrameworkCore;
using ProjectWork.Data;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectWork.Services
{
    public class UserService : InterfaceService<User>
    {

        private readonly DataContext _db;

        public UserService(DataContext db)
        {
            _db = db;
        }


        //Questo metodo non lo implementiamo per il momento dato che
        //già nell'AuthService avviene la Registrazione/Creazione
        public User Add(string ssn, User item)
        {
            throw new NotImplementedException();
        }

        //metodo implementato nel PostService
        public User Delete(string userSsn, int id)
        {
            throw new NotImplementedException();
        }

        public User Delete(string ssn)
        {

            var utenteDaElimiare = Search(ssn);

            _db.Users.Remove(utenteDaElimiare);

            _db.SaveChanges();

            return utenteDaElimiare;
        }

        public List<User> GetAll()
        {
            return _db.Users.ToList();
        }

        //metodo implementato nel PostService
        public List<User> GetAllPersonalPosts(string username)
        {
            throw new NotImplementedException();
        }

        //Questo metodo non lo implemetiamo dato che lo User ha l ssn non l'id

        //Metodo di Appoggio per Cercare uno Specifico Utente
        public User Search(string ssn)
        {
            User trovato = _db.Users.FirstOrDefault(utente => utente.Ssn == ssn.ToUpper());

            if (trovato is null)
            {
                throw new UserNotFoundException();
            }

            return trovato;
        }

        public List<User> SearchUsers(string firstName, string lastName)
        {
            // .Include() innerjoin in Entity Framework
            return _db.Users.Include(user => user.Posts).Where(user => user.FirstName.ToLower().Contains(firstName.ToLower())).Where(user => user.LastName.ToLower().Contains(lastName.ToLower())).ToList();
        }

        //Modifica dei dati che possono subire variazioni come Email, Indizzo Residenza
        // e numero di telefono
        public User Update(string ssn, string phoneNumber, string email, string address)
        {

            var found = Search(ssn);

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                found.PhoneNumber = phoneNumber;
                found.CreateUsername();
            }

            if (!string.IsNullOrEmpty(email))
                found.Email = email;

            if (!string.IsNullOrEmpty(address))
                found.Address = address;

            _db.Users.Update(found);

            _db.SaveChanges();

            return found;
        }

        //metodo implementato nel PostService
        public User Update(string userSsn, User item)
        {
            throw new NotImplementedException();
        }

        //Con questo metodo andiamo a restituire il nome e il cognome dell'utente connesso
        public Dictionary<string, string> WhoAmI(string ssn)
        {


            var found = _db.Users.Where(user => user.Ssn.ToUpper() == ssn.ToUpper()).Select(user => new Dictionary<string, string>{
                                                                                                                                   { "firstName",user.FirstName},
                                                                                                                                   { "lastName",user.LastName }
                                                                                                                                  });

            return found.FirstOrDefault();



        }
    }
}
