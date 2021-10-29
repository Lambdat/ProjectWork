﻿using ProjectWork.DTOs;
using ProjectWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.Services
{
    //Questa interfaccia fungerà da contratto, che i vari services
    //coinvolti implementeranno
    public interface IAuthService //Questa interfaccia è stata realizzata solo ed esclusivamente per gli Utenti (es. gli utenti non vengono gestiti dai CRUD)
    {
        //Questa interfaccia fungerà da contratto, che i vari services
        //coinvolti implementeranno


        bool Register(UserRegistrationRequest request);

        string Login(string ssn,string username, string password);


    }
}
