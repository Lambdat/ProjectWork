﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWork.DTOs
{
    public class UserLoginRequest
    {
        public string Ssn { get; set; }
        public string Password { get; set; }
    }
}
