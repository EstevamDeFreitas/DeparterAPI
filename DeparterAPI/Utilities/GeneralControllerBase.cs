using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;

namespace DeparterAPI.Utilities
{
    public class GeneralControllerBase : ControllerBase
    {
        protected readonly IServiceWrapper _serviceWrapper;

        public GeneralControllerBase(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        public Guid CurrentUserId()
        {
            return Guid.Parse(HttpContext.Items["User"].ToString());
        }
    }
}
