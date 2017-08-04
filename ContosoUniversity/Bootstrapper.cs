using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.Business.Contracts.Contracts;
using ContosoUniversity.Business.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;

namespace ContosoUniversity
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here   
            //This is the important line to edit   
            container.RegisterType<IStudentDBAccess, StudentDBAccess>();

            RegisterTypes(container);
            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}