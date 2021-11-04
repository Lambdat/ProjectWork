using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.DTOs
{
    public class UserLogin
    {
        public string Ssn { get; set; } 
        public string Username { get; set; } //In alternativa potrò accedere anche con il mio username
        public string Password { get; set; } 
    }
}
