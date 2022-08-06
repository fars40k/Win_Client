using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Win_Dev.Business;
using Ninject;

namespace Win_Dev.UI.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>    
    public class ViewModelLocator
    {
        public static StandardKernel kernel;
        public static INetworkClient client;
        static ViewModelLocator()
        {
            kernel = new StandardKernel();

            if (ViewModelBase.IsInDesignModeStatic)
            {

                kernel.Bind<INetworkClient>().To<ClientObject>().InSingletonScope();

            }

            else
            {

                kernel.Bind<INetworkClient>().To<ClientObject>().InSingletonScope();

            }

            kernel.Bind<BusinessModel>().ToSelf().InSingletonScope();
            kernel.Bind<MainViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<TableViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<PersonelViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<ProjectViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<LoginViewModel>().ToSelf().InSingletonScope();

            client = kernel.Get<INetworkClient>();
        }



        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                return kernel.Get<MainViewModel>();
            }
        }

        public LoginViewModel Login
        {
            get
            {
                return kernel.Get<LoginViewModel>();
            }
        }

        public TableViewModel Table
        {
            get
            {
                return kernel.Get<TableViewModel>();
            }
        }

        public PersonelViewModel Personel
        {
            get
            {
                return kernel.Get<PersonelViewModel>();
            }
        }     


        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}