using Api.Controllers.v1;
using Api.Utills.Execution;
using CliWrap.Buffered;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTest.Systems.Controllers
{
    public class RunTest
    {
        [Fact]
        public async Task Python()
        {
            var output = new BufferedCommandResult(0, new(), new(), "hi", "");
            var mockExcution = new Mock<IExecution>(); 
            mockExcution
                .Setup(service => service.Run(It.IsAny<Type>(),It.IsAny<SourceCode>()))
                .ReturnsAsync(output); 
            var ctr = new Run(mockExcution.Object);

            var resaultA = await ctr.Python(new SourceCode()); 

            var resault = resaultA.Result as OkObjectResult;
            var ActualResault = resault!.Value as BufferedResultDto;

            ActualResault.Should().NotBeNull("All completed and uncompleted must return report");
            ActualResault.Should().BeEquivalentTo(output);

        }


        [Fact]
        public async Task C()
        {
            var output = new BufferedCommandResult(0, new(), new(), "hi", "");
            var mockExcution = new Mock<IExecution>(); 
            mockExcution
                .Setup(service => service.Run(It.IsAny<Type>(),It.IsAny<SourceCode>()))
                .ReturnsAsync(output); 
            var ctr = new Run(mockExcution.Object);

            var resaultA = await ctr.C(new SourceCode()); 

            var resault = resaultA.Result as OkObjectResult;
            var ActualResault = resault!.Value as BufferedResultDto;

            ActualResault.Should().NotBeNull("All completed and uncompleted must return report");
            ActualResault.Should().BeEquivalentTo(output);

        }

        [Fact]
        public void DtoTest() 
        {
            var data = new BufferedCommandResult(1, new(), new(), "Hello", "Error:12TestErrorReplacment");

            var resNoReplace = new BufferedResultDto(data);
            var resReplace = new BufferedResultDto(data,new(@"\w+:\d\d"));

            resNoReplace.Should().BeEquivalentTo(data);
            resReplace.StandardError.Should().Be("TestErrorReplacment");
        }
    }
}