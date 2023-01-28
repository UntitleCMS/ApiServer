using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using CliWrap.Buffered;
using Newtonsoft.Json;
using Api.Utills.Execution;

namespace Api.Controllers
{
    [Route("api/v1/run")]
    [ApiController]
    public class Run : ControllerBase
    {
        private readonly IExecution _execution;
        public Run(IExecution execution)
        {
            _execution = execution;
        }

        [HttpPost("python38")]
        public async Task<ActionResult<BufferedResultDto>> Python([FromForm] SourceCode sourceCode)
        {
            var res = await _execution.Run(PythonStrategy.Type, sourceCode);

            var dto = new BufferedResultDto(res, new(@"\s*File\s*\"".*""\s*,\s*"));
            return Ok(dto);
        }

        [HttpPost("c")]
        public async Task<ActionResult<BufferedResultDto>> C([FromForm] SourceCode sourceCode)
        {
            var res = await _execution.Run(CStrategy.Type, sourceCode);
            var dto = new BufferedResultDto(res, new(@"code_.*\.c:\s+"));
            return Ok(dto);
        }
    }

    public class BufferedResultDto
    {
        public string StandardOutput { get; set; } = string.Empty;
        public string StandardError { get; set; } = string.Empty;
        public int ExitCode { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset ExitTime { get; set; }
        public TimeSpan RunTime { get; set; }

        public BufferedResultDto(BufferedCommandResult data)
        {
            add(data);
        }

        public BufferedResultDto(BufferedCommandResult data, Regex replace)
        {
            add(data);
            StandardError = replace.Replace(StandardError, string.Empty);
        }

        private void add(BufferedCommandResult data)
        {
            StandardOutput = data.StandardOutput;
            StandardError = data.StandardError;
            ExitCode = data.ExitCode;
            RunTime = data.RunTime;
            StartTime = data.StartTime;
            ExitTime = data.ExitTime;
        }
    }
}
