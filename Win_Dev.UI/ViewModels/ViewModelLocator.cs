using GalaSoft.MvvmLight;
using Win_Dev.Business;
using Ninject;
using Win_Dev.Data;

namespace Win_Dev.UI.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>    
    public class ViewModelLocator
    {
        public static StandardKernel Kernel;
        public static INetworkClient Client;
        static ViewModelLocator()
        {
            Kernel = new StandardKernel();

            if (ViewModelBase.IsInDesignModeStatic)
            {

                Kernel.Bind<INetworkClient>().To<NetworkClient>().InSingletonScope();

            }

            else
            {

                Kernel.Bind<INetworkClient>().To<NetworkClient>().InSingletonScope();

            }

            Kernel.Bind<BusinessModel>().ToSelf().InSingletonScope();
            Kernel.Bind<MainViewModel>().ToSelf().InSingletonScope();
            Kernel.Bind<TableViewModel>().ToSelf().InSingletonScope();
            Kernel.Bind<PersonelViewModel>().ToSelf().InSingletonScope();
            Kernel.Bind<ProjectViewModel>().ToSelf().InSingletonScope();
            Kernel.Bind<LoginViewModel>().ToSelf().InSingletonScope();

            Client = Kernel.Get<INetworkClient>();
        }



        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                return Kernel.Get<MainViewModel>();
            }
        }

        public LoginViewModel Login
        {
            get
            {
                return Kernel.Get<LoginViewModel>();
            }
        }

        public TableViewModel Table
        {
            get
            {
                return Kernel.Get<TableViewModel>();
            }
        }

        public PersonelViewModel Personel
        {
            get
            {
                return Kernel.Get<PersonelViewModel>();
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