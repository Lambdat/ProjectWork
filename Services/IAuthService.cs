using ProjectWork.DTOs;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Services
{
    public interface IAuthService
    {
        bool Register(UserRegistrationRequest request);

        string Login(string ssn,string username, string password);


    }
}
