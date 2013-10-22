using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using FatCloud.Client.FatDB;
using FatDBGuestBook.Controllers;

namespace FatDBGuestBook
{
    public class ControllerFactory : IControllerFactory
    {
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (controllerName == "Home")
            {
                return new HomeController(CreateConnection());
            }
            else
            {
                throw new HttpException(404, "Not Found");
            }
        }

        private FatDBConnection CreateConnection()
        {
            return new FatDBConnection();
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;
            if (disposable != null) disposable.Dispose();
        }
    }
}