using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxCalculator;
using Unity;
using Unity.Lifetime;
using Unity.Wcf;

namespace WcfService1
{
    public class WcfServiceFactory : UnityServiceHostFactory
    {
        private static readonly object lockObject = new object();

        protected static IUnityContainer Container { get; set; }

        protected override void ConfigureContainer(IUnityContainer container)
        {
            UnityContainerSetup.SetupContainer(container);

            container.RegisterType<ITaxService, TaxService>(new ContainerControlledLifetimeManager());

            Container = container;
        }

        public static T Resolve<T>()
        {
            if (Container == null)
            {
                lock(lockObject)
                {
                    if(Container == null)
                    {
                        var container = new UnityContainer();
                        new WcfServiceFactory().ConfigureContainer(container);
                        Container = container;
                    }
                }
            }

            return Container.Resolve<T>();
        }

    }
}