using Microsoft.EntityFrameworkCore;
using ProjectWork.Data;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Services
{
    public class PostService: InterfaceService<Post>
    {
        private readonly DataContext _db;
        public PostService(DataContext db)
        {
            _db = db;
        }

        //Quest sono tutti i post che leggiamo dalla Home Page
        public List<Post> GetAll()
        {
            return _db.Posts.ToList();
        }



        public Post Add(Post item)
        {
            
            var itemAdded = _db.Posts.Add(item);

            _db.SaveChanges();

            return itemAdded.Entity;
        }

        public Post Delete(int id)
        {
            var itemRemoved = _db.Posts.Remove(Search(id));

            _db.SaveChanges(); //salva le modifiche della tabella

            return itemRemoved.Entity;
        }




        public Post Search(int id)
        {
            var itemFound = _db.Posts.FirstOrDefault(Posts => Posts.Id == id);

            if (itemFound is null)
            {
                throw new Exception("Non esiste il post con l'id= "+ id);
            }
            return itemFound;
        }


        //PUT
        public Post Update(Post item)
        {

            var itemModified = _db.Posts.Update(item);

            _db.SaveChanges();

            return itemModified.Entity;
        
        }

        public List<Post> GetAllPostsByUsername(string username)
        {
            //lo scriviamo sottoforma di Query LINQ

            /*
            var ris = from posts in _db.Posts
                      join utenti in _db.Users on posts.UserSsn equals utenti.Ssn
                      where utenti.Username == username
                      select posts;
            */


            //Metodo altenativo con Entity Framework
            var utenteTrovato = _db.Users.FirstOrDefault(utente => utente.Username == username.ToLower());

            if(utenteTrovato is null)
            {
                throw new UserNotFoundException();
            }

            var ris = _db.Posts.Where(post => post.UserSsn == utenteTrovato.Ssn).ToList();


            return ris;
        }

        public Post Search(string ssn)
        {
            throw new NotImplementedException();
        }

        public Post Delete(string ssn)
        {
            throw new NotImplementedException();
        }

        public Post Update(string ssn, string address, string email, string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
