using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;
using ProjectWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet("{id}")]
        public Post SearchById([FromRoute] int id)
        {
            return _iService.SearchById(id);
        }

        [HttpGet]
        public List<Post> GetAllByUsername()
        {
            var username = this.User.Identity.Name; // vogliamo tutti i post relativi al token abbia questo username
            return _iService.GetAllByUsername(username);
        }

        [HttpPost]
        public Post Add([FromBody] Post item) 
        {
            return _iService.Add(item);
        }

        [HttpDelete("{id}")]
        public Post Delete([FromRoute] int id) 
        {
            return _iService.Delete(id);
        }

        [HttpPut]
        public Post Update([FromBody] Post item) 
        {
            return _iService.UpdateById(item);
        }




    }
}
