using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;
using ProjectWork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace ProjectWork.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController
    {
        private readonly InterfaceService<Post> _iService;

        public PostsController(InterfaceService<Post> iService)
        {
            _iService = iService;
        }

        [HttpGet]
        public List<Post> GetAll() 
        {
            return _iService.GetAll();
        }

        [HttpGet("{id}")]
        public Post SearchById([FromRoute] int id)
        {
            return _iService.SearchById(id);
        }

        /*[HttpGet("{ssn}")]
         public Post SearchBySsn([FromRoute] string ssn)
         {

         }    */

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

        /* [HttpDelete("{ssn}")]
         public Post Delete([FromRoute] string ssn) 
         {

         }*/

        [HttpPut]
        public Post Update([FromBody] Post item) 
        {
            return _iService.UpdateById(item);
        }

        /*    [HttpPut("{ssn}")]
            public Post Update([FromRoute] string ssn,[FromRoute] Post item) 
            {

            }    */



    }
}
