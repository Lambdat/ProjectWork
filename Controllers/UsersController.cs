using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWork.Data;
using System.Security.Claims;
using ProjectWork.Services;
using Microsoft.AspNetCore.Authorization;

namespace ProjectWork.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {

        private readonly InterfaceService<User> _iService;

        public UsersController(InterfaceService<User> iService)
        {
            _iService = iService;
        }

        //Con questa chiamata Get andiamo a restituire il nome e il cognome dell'utente connesso
        [HttpGet("whoAmI")]
        public Dictionary<string,string> WhoAmI()
        {
            string ssn = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return _iService.WhoAmI(ssn);
        }


        [HttpGet("friendList")]
        public List<User> GetAll()
        {
           return  _iService.GetAll();
        }

        [HttpGet("search")]
        public List<User> SearchUsers([FromQuery] Dictionary<string,string> parametri) 
        {
            string firstName = parametri["firstname"];
            string lastName = parametri["lastname"];

            return _iService.SearchUsers(firstName, lastName);
        }


        [HttpDelete("deleteAccount")]
        public User Delete()
        {
            string ssn = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // Da qui troviamo l'ssn dell'utente loggato attualmente
            return _iService.Delete(ssn);
        
        }

        
        [HttpPut("settings")]
        public User Update([FromBody] Dictionary<string,string> impostazioni) //Dalla chiamata put/post ci arriva un Dictionary<string,string> ({chiave:valore})
        {
            string ssn = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // Da qui troviamo l'ssn dell'utente loggato attualmente

            string phoneNumber = impostazioni["phoneNumber"];

            string email = impostazioni["email"];

            string address = impostazioni["address"];

            return _iService.Update(ssn, phoneNumber, email, address);   //mantenere sempre lo stesso ordine dei parametri
        }
        







    }
}
