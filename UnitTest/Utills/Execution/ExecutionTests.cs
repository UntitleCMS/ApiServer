using FluentAssertions;
using CliWrap;
using CliWrap.Buffered;

namespace Api.Utills.Execution.Tests
{
    public class ExecutionTests
    {
        [Fact()]
        public async Task RunTest()
        {
            var expected = new BufferedCommandResult(0,new(),new(),"","");
            var ExecutionInstant = Execution.Instance;
            var mockStrategy = MockStrategy.Type;
            var dummySourceCode = new SourceCode() { Code = "", Input = "" };

            var resualt = await ExecutionInstant.Run(mockStrategy, dummySourceCode);

            expected.Should().BeEquivalentTo(resualt);
        }

        [Fact()]
        public void GetExecutionStrategyTest()
        {
            var expected = new PythonStrategy();
            var ExecutionInstant = Execution.Instance;

            var actual = ExecutionInstant.GetExecutionStrategy(PythonStrategy.Type);
            var actual2ed = ExecutionInstant.GetExecutionStrategy(PythonStrategy.Type);

            expected.Should().BeEquivalentTo(actual);
            actual.Should().Be(actual2ed);
        }

        [Fact()]
        public void CreateExecutionStrategyTest()
        {
            var expected = new CStrategy();
            var ExecutionInstant = Execution.Instance;

            var actual = ExecutionInstant.CreateExecutionStrategy(CStrategy.Type);
            var actual2ed = ExecutionInstant.CreateExecutionStrategy(CStrategy.Type);

            expected.Should().BeEquivalentTo(actual);
            actual.Should().NotBe(actual2ed);
        }
    }

    class MockStrategy : IExecutionStrategy
    {
        public string FileExtension => "Mock";
        public static Type Type => typeof(MockStrategy);
        public Task<BufferedCommandResult> Run(string fileName, PipeSource pipeSource)
        {
            return Task.Run(() => new BufferedCommandResult(0, new(), new(), "", ""));
        }
    }
}