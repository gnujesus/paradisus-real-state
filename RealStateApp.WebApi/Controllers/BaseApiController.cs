using Microsoft.AspNetCore.Mvc;

namespace RealStateApp.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public abstract class BaseApiController : ControllerBase { }
}
