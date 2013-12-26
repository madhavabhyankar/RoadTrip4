using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;

namespace RoadTrip_4.App_Start
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory(IKernel kernel)
        {
            ninjectKernel = kernel;
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return (controllerType == null) ? null : (IController)ninjectKernel.Get(controllerType);
        }
    }
}