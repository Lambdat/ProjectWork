﻿using ProjectWork.Data;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public User Add(User item)
        {
            if (_db.Users.Any(utente => utente.Ssn == item.Ssn))
                throw new Exception("Questo oggetto già esiste");

            _db.Users.Add(item);

            _db.SaveChanges();

            return item;
        }

        //per il postservice
        public User Delete(int id)
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

        public List<User> GetAllPostsByUsername(string username)
        {
            throw new NotImplementedException();
        }

        //Questo metodo non lo implemetiamo dato che lo User ha l ssn non l'id
        public User Search(int id)
        {
            throw new NotImplementedException();
        }

        public User Search(string ssn)
        {
            User trovato = _db.Users.FirstOrDefault(utente => utente.Ssn == ssn.ToUpper());

            if(trovato is null)
            {
                throw new UserNotFoundException();
            }

            return trovato;
        }

        //Modifica dei dati che possono subire variazioni come Email, Indizzo Residenza
        // e numero di telefono
        public User Update(string ssn,string phoneNumber,string email,string address)
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

        //Il seguente metodo è per i posts
        public User Update(User item)
        {
            throw new NotImplementedException();
        }


    }
}
