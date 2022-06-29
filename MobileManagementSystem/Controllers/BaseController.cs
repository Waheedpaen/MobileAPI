using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MobileManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public ServiceResponse<object> _response;
        public BaseController()
        {
            _response = new ServiceResponse<object>();
        }
    }
}
