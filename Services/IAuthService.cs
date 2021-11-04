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
        bool Register(UserRegistration request);

        string Login(UserLogin userLogin);


    }
}
