﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCenter.Services.Auth
{
    public interface ILoginService
    {
        string Login(string username, string password);
    }
}
