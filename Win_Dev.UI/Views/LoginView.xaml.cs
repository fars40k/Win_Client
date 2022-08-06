using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Win_Dev.Business;

namespace Win_Dev.UI.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public INetworkClient NetworkClient { get; set; }

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

        public RelayCommand LogInCommand { get; set; }


        public LoginView()
        {

            LogInCommand = new RelayCommand(() =>
            {
                
            });

            InitializeComponent();
        }
    }
}
