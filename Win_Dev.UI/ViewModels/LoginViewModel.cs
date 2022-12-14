using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Win_Dev.Business;
using Win_Dev.Data;

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
                RaisePropertyChanged("Login");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }

        public RelayCommand SignInCommand { get; set; }

        public LoginViewModel()
        {
            Login = "";
            Password = "";
           
            ClientObject = ViewModelLocator.Client;

            SignInCommand = new RelayCommand(() =>
            {

                ClientObject.LogIn(Login, Password);

            });

        }
    }
}