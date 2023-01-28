using Api.Controllers.v1;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace UnitTest.Systems.Controllers
{
    public class CompilerTest
    {
        [Fact]
        public void CompilersShoudBePythonAndC()
        { 
            var ctr = new Compiler();

            var actualResualt = (OkObjectResult)ctr.GetReady();
            var statusCode = actualResualt.StatusCode;
            var value = (IDictionary<string, CompilerStatus>)actualResualt.Value!;

            statusCode.Should().Be(200);
            value.Should().NotBeNull();
            value.Keys.Should().Contain(new[] { "Python3.8", "C" });
        }
    }
}