using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Ioc;
using Win_Dev.Business;
using System.Windows;
using System.Linq;
using Win_Dev.Data;
using System.Windows.Media;
using System.Windows.Controls;
using Win_Dev.UI.Views;
using System.Threading;
using System.Threading.Tasks;
using System;
using GalaSoft.MvvmLight.Messaging;

namespace Win_Dev.UI.ViewModels
{

    public class MainViewModel : ViewModelBase
    {
        private ClientObject _clientObject;

        private RegistryWorker _registryWorker;
        private ApplicationCultures _applicationCultures;

        public ObservableCollection<string> CulturesCB { get; set; }

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

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {

            MessengerInstance.Register<NotificationMessage<string>>(this, BeingNotifed);

            UIInit();

            EventsSubscription();

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
            _registryWorker = SimpleIoc.Default.GetInstance<RegistryWorker>();

            CulturesCB = new ObservableCollection<string>();
            _applicationCultures = SimpleIoc.Default.GetInstance<ApplicationCultures>();
            _applicationCultures.Cultures.ToList().ForEach(CulturesCB.Add);

            _selectedCulture = _registryWorker.ReadLanguageRegistryEntry();
            SelectedCulture = _selectedCulture;

            _clientObject = SimpleIoc.Default.GetInstance<ClientObject>();

            DatabaseUpdating = Visibility.Visible;
            ConnectionStatusColour = new SolidColorBrush(Colors.OrangeRed);
            UserHelpString = (string)Application.Current.Resources["Database_Wait"] ?? "missing";

        }

        private void EventsSubscription()
        {

            ApplicationState state = _clientObject.Initialise();
            
        }       


    }
}