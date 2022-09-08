using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Ioc;
using Win_Dev.Business;
using System.Windows;
using System.Linq;
using System.Windows.Media;
using System.Windows.Controls;
using Win_Dev.UI.Views;
using System.Threading;
using System.Threading.Tasks;
using System;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using Ninject;
using Win_Dev.Data;

namespace Win_Dev.UI.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        private IKernel _kernel;
        private INetworkClient _client; 
        private RegistryWorker _registryWorker;
        private ApplicationCultures _applicationCultures;

        public ObservableCollection<string> CulturesCB { get; set; }

        #region BindedProperties

        private UserControl _tabControlArea;
        public UserControl TabControlArea
        {
            get { return _tabControlArea; }
            set
            {
                _tabControlArea = value;
                RaisePropertyChanged("TabControlArea");
            }
        }

        private string _selectedCulture;
        public string SelectedCulture
        {
            get { return _selectedCulture; }
            set
            {
                if (_selectedCulture != value)
                {
                    _registryWorker.UpdateLanguageRegistryEntry(value);

                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }

                RaisePropertyChanged("SelectedCulture");
            }
        }

        private SolidColorBrush _connectionStatusColour;
        public SolidColorBrush ConnectionStatusColour
        {
            get { return _connectionStatusColour;  }
            set
            {
                _connectionStatusColour = value;
                RaisePropertyChanged("ConnectionStatusColour");
            }
        }

        private Visibility _databaseUpdating;
        public Visibility DatabaseUpdating
        {
            get { return _databaseUpdating; }
            set
            {
                _databaseUpdating = value;
                RaisePropertyChanged("DatabaseUpdating");
            }
        }

        private string _databaseString;
        public string DatabaseString
        {
            get { return _databaseString; }
            set
            {
                _databaseString = value;
                RaisePropertyChanged("DatabaseString");
            }
        }

        private string _userHelpString;

        public string UserHelpString
        {
            get { return _userHelpString; }
            set
            {
                _userHelpString = value;
                RaisePropertyChanged("UserHelpString");
            }
        }

        #endregion

        public RelayCommand LogOutCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(RegistryWorker registryWorker, ApplicationCultures cultures, IKernel kernel)
        {
            _kernel= kernel;
            _registryWorker = registryWorker;
            _client = ViewModelLocator.client;
            _applicationCultures = cultures;           

            MessengerInstance.Register<NotificationMessage<string>>(this, BeingNotifed);

            UIInit();

        }

        private void BeingNotifed(NotificationMessage<string> obj)
        {
            if (obj.Notification == "Error")
            {
                UserHelpString = obj.Content ?? "missing";
            }
        }

        private void UIInit()
        {
            CulturesCB = new ObservableCollection<string>();
            _applicationCultures.Cultures.ToList().ForEach(CulturesCB.Add);

            _selectedCulture = _registryWorker.ReadLanguageRegistryEntry();
            SelectedCulture = _selectedCulture;

            DatabaseUpdating = Visibility.Visible;
            ConnectionStatusColour = new SolidColorBrush(Colors.OrangeRed);
            UserHelpString = (string)Application.Current.Resources["Database_Wait"] ?? "missing";
            DatabaseUpdating = Visibility.Hidden;

            _client.Initialise();
            NetworkClient _clientObject = _client as NetworkClient;

            if (_clientObject.State.IsServerFound == false)
            {

                UserHelpString = (string)Application.Current.Resources["Database_Fail"] ?? "missing";

            } else
            {
                UserHelpString = String.Empty;

                LogOutCommand = new RelayCommand(() =>
                {
                    _client.LogOut();
                });

                TabControlArea = _kernel.Get<LoginView>();

            }

            _clientObject.ApplicationStateChanged += _clientObject_applicationStateChanged;
        }

        private void _clientObject_applicationStateChanged(ApplicationState obj)
        {
            NetworkClient _clientObject = _client as NetworkClient;

            UserHelpString = _clientObject.State.ToString();

            if (_clientObject.State.LoginAttemptFailed == true)
            {

                UserHelpString = (string)Application.Current.Resources["Attempt_failed"] ?? "missing";

            }
            else 
            
            if (_clientObject.State.DoesUserLoggedIn == false)

            {
                if (!(TabControlArea is LoginView))
                {

                    TabControlArea = _kernel.Get<LoginView>();

                }

            }
            else
            {
                if (!(TabControlArea is TableView))
                {

                    TabControlArea = _kernel.Get<TableView>();

                }

            }
        }
    }
}