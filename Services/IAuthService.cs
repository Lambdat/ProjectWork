using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Services
{
    public interface IAuthService
    {
        bool Registester(User utente, string password);

        string Login(string ssn, string password);


    }
}
