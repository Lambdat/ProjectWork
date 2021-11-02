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



        public Post Add(string userSsn,Post item)
        {
            item.UserSsn = userSsn.ToUpper();  //Quando si aggiunge un post, lo si aggiunge al proprio profilo (chiave esterna data dal token di login)

            item.CreatedTime = DateTime.Now; //Impostiamo la data automatica attuale, quando viene creato un nuovo post

            var itemAdded = _db.Posts.Add(item);

            _db.SaveChanges();

            return itemAdded.Entity;
        }

        public Post Delete(string userSsn,int id)
        {

            var itemToBeRemoved = Search(id);

            if(itemToBeRemoved.UserSsn.ToUpper()==userSsn.ToUpper())
            {
                _db.Posts.Remove(Search(id));
            }
            

            _db.SaveChanges(); //salva le modifiche della tabella

            return itemToBeRemoved;
        }




        public Post Search(int id)
        {
            var itemFound = _db.Posts.FirstOrDefault(Posts => Posts.Id == id);


            return itemFound;
        }


       
        public Post Update(string userSsn,Post item)
        {


            if (item.UserSsn.ToUpper() == userSsn.ToUpper())
            {
                var itemModified = _db.Posts.Update(item);
                _db.SaveChanges();
                return itemModified.Entity;
            }
            else
                return item;
       
        }

        public List<Post> GetAllPersonalPosts(string username)
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
