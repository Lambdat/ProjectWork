using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWork.Data;


namespace ProjectWork.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public DataContext _ctx { get; set; }

        public UsersController(DataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public List<User> GottaCacthEmAll()
        {
           return  _ctx.Users.ToList();
        }
    }
}
