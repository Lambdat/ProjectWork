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

        

        [HttpGet]
        public List<User> GetAll()
        {
           return  _iService.GetAll();
        }

        [HttpGet("search")]
        public User Search([FromBody]string ssn) 
        {
            return _iService.Search(ssn);
        }


        [HttpDelete]
        public User Delete()
        {
            string ssn = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // Da qui troviamo l'ssn dell'utente loggato attualmente
            return _iService.Delete(ssn);
        
        }

        
        [HttpPut]
        public User Update([FromBody] string phoneNumber,string email,string address)
        {
            string ssn = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value; // Da qui troviamo l'ssn dell'utente loggato attualmente
            return _iService.Update(ssn, address, email, phoneNumber);
        }
        







    }
}
