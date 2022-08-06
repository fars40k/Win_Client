using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win_Dev.Business;

namespace Win_Dev.UI
{
    internal class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<INetworkClient>().To<ClientObject>().InSingletonScope();
        }
    }
}
