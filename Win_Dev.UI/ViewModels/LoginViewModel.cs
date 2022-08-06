using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win_Dev.Business;

namespace Win_Dev.UI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public INetworkClient ClientObject { get; set; }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }

        public RelayCommand SignInCommand { get; set; }

        public LoginViewModel(INetworkClient client)
        {
            ClientObject = client;

            SignInCommand = new RelayCommand(() =>
            {

            });

        }
    }
}