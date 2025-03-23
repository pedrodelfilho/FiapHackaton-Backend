using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Shared
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApiControllerBase : ControllerBase { }
}
