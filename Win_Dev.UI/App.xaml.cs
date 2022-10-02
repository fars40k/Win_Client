using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using Win_Dev.UI.ViewModels;
using Win_Dev.Business;
using Win_Dev.UI.Views;
using Ninject;
using Win_Dev.Data;

namespace Win_Dev.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IKernel _kernel;
        private RegistryWorker _registryWorker;
        private ApplicationCultures _applicationCultures;
        private INetworkClient _client;

        private static ViewModelLocator _viewModelLocator;
        public static ViewModelLocator ViewModelLocator
        {
            get { return _viewModelLocator; }
        }

        static App()
        {           
            DispatcherHelper.Initialize();

            _viewModelLocator = new ViewModelLocator();
            _kernel = ViewModelLocator.Kernel;
        }

        protected void App_Startup(object sender, StartupEventArgs e)
        { 
            _client = _kernel.Get<INetworkClient>();

            _kernel.Bind<RegistryWorker>().ToSelf().InSingletonScope();
            _kernel.Bind<ApplicationCultures>().ToSelf().InSingletonScope();
            _kernel.Bind<MainWindow>().ToSelf().InSingletonScope(); 

            _applicationCultures = _kernel.Get<ApplicationCultures>();
            _registryWorker = _kernel.Get<RegistryWorker>();

            // Setting application localisation

            _registryWorker.SetAvalableCultures(_applicationCultures.Cultures);
            string storedLangSelection = _registryWorker.ReadLanguageRegistryEntry();
            _applicationCultures.LocalisationDictionary = new ResourceDictionary();
            _applicationCultures.LocalisationDictionary.Source =
            _applicationCultures.MapCultureToResourceUri(storedLangSelection);
            Application.Current.Resources.MergedDictionaries.Add(_applicationCultures.LocalisationDictionary);

            // Creating main window

            MainWindow mainWindow = _kernel.Get<MainWindow>();
            mainWindow.DataContext = ViewModelLocator.Main;
            mainWindow.Resources.MergedDictionaries.Add(_applicationCultures.LocalisationDictionary);
            mainWindow.MainWindowInit();
            mainWindow.Show();

        }
    }
}
