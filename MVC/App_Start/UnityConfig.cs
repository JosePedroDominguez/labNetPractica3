using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace MVC
{
    public static class UnityConfig
    {
        public static IUnityContainer Container { get; internal set; }

        public static void RegisterComponents()
        {
            _ = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

        }
    }
}
