using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win_Dev.Business
{
    public interface INetworkClient
    {
        ApplicationState Initialise();

        ApplicationState LogIn(string login, string password);

        ApplicationState LogOut();


        
    }
}
