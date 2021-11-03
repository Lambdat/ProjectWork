using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;
using ProjectWork.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ProjectWork.Controllers
{
    /*
    [Authorize]Questo filtro controlla se l'utente è autenticato. In caso contrario, 
    restituisce il codice di stato HTTP 401 (non autorizzato), 
    senza richiamare l'azione.
    */
    // solo coloro che mostrano di essere autenticati posso usare i metodi 
    // di questo controller
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly InterfaceService<Post> _iService;

        public PostsController(InterfaceService<Post> iService)
        {
            _iService = iService;
        }

        [HttpGet("home")]
        public List<Post> GetAll()
        {
            return _iService.GetAll();
        }

        [HttpGet("{id}")] //Quando sono presenti le parentesi graffe nella Route, intediamo una WildCard
        public ActionResult Search([FromRoute] int id)
        {
            if (_iService.Search(id) is not null)
            {
                return Ok(_iService.Search(id));
            }
            else
                return BadRequest("Post non Trovato");

        }

        [HttpGet("myAccount")]
        public List<Post> GetAllPersonalPosts()
        {
            var username = this.User.Identity.Name; // vogliamo tutti i post relativi al token abbia questo username
            return _iService.GetAllPersonalPosts(username);
        }

        [HttpPost]
        public Post Add([FromBody] Post item)
        {
            //andiamo a prendere dal token il claim contenente il NameIdentifier, che nel nostro AuthService corrispone al SSN (CF)
            string ssn = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return _iService.Add(ssn, item);
        }

        [HttpDelete("{id}")] //Quando sono presenti le parentesi graffe nella Route, intediamo una WildCard
        public Post Delete([FromRoute] int id, string userSsn)
        {
            //andiamo a prendere dal token il claim contenente il NameIdentifier, che nel nostro AuthService corrispone al SSN (CF)
            userSsn = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return _iService.Delete(userSsn, id);
        }



        [HttpPut]
        public Post Update([FromBody] Post item,string userSsn)
        {
            userSsn = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (userSsn.ToUpper() == item.UserSsn.ToUpper())
            {
                return _iService.Update(userSsn, item);
            }
            else
                return item;
        }




    }
}
