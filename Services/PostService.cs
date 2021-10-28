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
            var itemRemoved = _db.Posts.Remove(SearchById(id));

            _db.SaveChanges(); //salva le modifiche della tabella

            return itemRemoved.Entity;
        }

     /*   public Post Delete(string ssn)
        {
            throw new NotImplementedException();
        }*/


        public Post SearchById(int id)
        {
            var itemFound = _db.Posts.FirstOrDefault(Posts => Posts.Id == id);

            if (itemFound is null)
            {
                throw new Exception("Non esiste il post con l'id= "+ id);
            }
            return itemFound;
        }

        /* public Post SearchBySsn(string ssn)
         {
             throw new NotImplementedException();
         }*/

        //PUT
        public Post UpdateById(Post item)
        {
            /* var itemExists =SearchById(item.Id);

             if (itemExists is null)
             {
                 throw new Exception("Post non trovato con id" + item.Id);
             }
             else
            */
            

            var itemModified = _db.Posts.Update(item);

            _db.SaveChanges();

            return itemModified.Entity;
        
        }

       /* public Post Put(Post item)
        {
            var itemAdded = _db.Posts.Update(item);

            _db.SaveChanges();

            return itemAdded.Entity;
        }*/


        /*   public Post UpdateBySsn(string ssn, Post item)
           {
               throw new NotImplementedException();
           }*/
    }
}
