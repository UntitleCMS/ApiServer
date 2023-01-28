using Api.Utills.Execution;
using CliWrap.Buffered;
using FluentAssertions;

namespace UnitTest.Utills.Execution
{
    public class PythonStrategyTest
    {
        [Fact]
        public void CheckType()
        {
            Type classType = typeof(PythonStrategy);

            Type ActualType = PythonStrategy.Type;

            classType.Should().Be(ActualType);
        }

        [Fact]
        public async Task RunShouldExecuted()
        {
            var expected = new BufferedCommandResult(2, new(), new(), "", "python3: can't open file 'Dummy'");
            var python = new PythonStrategy();

            var res = await python.Run("Dummy", CliWrap.PipeSource.Null);

            res.ExitCode.Should().Be(expected.ExitCode);
            res.StandardOutput.Should().Be(expected.StandardOutput);
            res.StandardError.Should().Contain(expected.StandardError);
        }
    }
}
