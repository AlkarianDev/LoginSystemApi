using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllegicWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {

        //Main class from which all others controllers heritate settings
    }
}
