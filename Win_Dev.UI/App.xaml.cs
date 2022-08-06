using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using Win_Dev.UI.ViewModels;
using Win_Dev.Business;
using Win_Dev.UI.Views;
using Ninject;

namespace Win_Dev.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IKernel kernel;
        private RegistryWorker registryWorker;
        private ApplicationCultures applicationCultures;
        private INetworkClient client;

        private static ViewModelLocator _viewModelLocator;
        public static ViewModelLocator ViewModelLocator
        {
            get { return _viewModelLocator; }
        }

        static App()
        {           
            DispatcherHelper.Initialize();

            _viewModelLocator = new ViewModelLocator();
            kernel = ViewModelLocator.kernel;
        }

        protected void App_Startup(object sender, StartupEventArgs e)
        { 
            client = kernel.Get<INetworkClient>();

            kernel.Bind<RegistryWorker>().ToSelf().InSingletonScope();
            kernel.Bind<ApplicationCultures>().ToSelf().InSingletonScope();
            kernel.Bind<MainWindow>().ToSelf().InSingletonScope(); 

            applicationCultures = kernel.Get<ApplicationCultures>();
            registryWorker = kernel.Get<RegistryWorker>();

            // Setting application localisation

            registryWorker.SetAvalableCultures(applicationCultures.Cultures);
            string storedLangSelection = registryWorker.ReadLanguageRegistryEntry();
            applicationCultures.LocalisationDictionary = new ResourceDictionary();
            applicationCultures.LocalisationDictionary.Source =
            applicationCultures.MapCultureToResourceUri(storedLangSelection);
            Application.Current.Resources.MergedDictionaries.Add(applicationCultures.LocalisationDictionary);

            // Creating main window

            MainWindow mainWindow = kernel.Get<MainWindow>();
            mainWindow.DataContext = ViewModelLocator.Main;
            mainWindow.Resources.MergedDictionaries.Add(applicationCultures.LocalisationDictionary);
            mainWindow.MainWindowInit();
            mainWindow.Show();

        }
    }
}
