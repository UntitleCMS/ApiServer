using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CliWrap;
using CliWrap.Buffered;
using Microsoft.AspNetCore.Cors;
using System.Net.Sockets;

namespace Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class Compiler : ControllerBase
    {
        [HttpGet("compilers")]
        //public ActionResult<IDictionary<string,CompilerStatus>> GetReady()
        public ActionResult GetReady()
        {
            IDictionary<string, CompilerStatus> compilers = new Dictionary<string, CompilerStatus>
            {
                { "Python3.8", CompilerStatus.Ready },
                { "C", CompilerStatus.Ready },
            };

            return Ok(compilers);
        }
    }

    public enum CompilerStatus
    {
        Ready,
        NotReady
    }
}
