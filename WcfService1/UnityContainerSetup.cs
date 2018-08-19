using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxCalculator;
using Unity;
using Unity.Lifetime;

namespace WcfService1
{
    public class UnityContainerSetup
    {
        private static IUnityContainer container;

        public static T Resolve<T>()
        {
            if (container == null)
            {
                container = new UnityContainer();
                SetupContainer(container);
            }

            return container.Resolve<T>();
        }

        public static void SetupContainer(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<ITaxTable, TaxTable>(new ContainerControlledLifetimeManager());

            container = unityContainer;
        }
    }

}