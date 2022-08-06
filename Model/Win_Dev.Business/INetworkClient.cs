﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win_Dev.Business
{
    public interface INetworkClient
    {

        void Initialise();

        void LogIn(string login, string password);

        void LogOut();

        
    }
}
