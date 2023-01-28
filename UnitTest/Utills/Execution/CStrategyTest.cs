using Api.Utills.Execution;
using CliWrap.Buffered;
using FluentAssertions;

namespace UnitTest.Utills.Execution
{
    public class CStrategyTest
    {
        [Fact]
        public void CheckType()
        {
            Type classType = typeof(CStrategy);

            Type ActualType = CStrategy.Type;

            classType.Should().Be(ActualType);
        }

        [Fact]
        public async Task RunShouldExecuted()
        {
            var expected = new BufferedCommandResult(1, new(), new(), "", "gcc: error: Dummy: No such file or directory");
            var python = new CStrategy();

            var res = await python.Run("Dummy", CliWrap.PipeSource.Null);

            res.ExitCode.Should().Be(expected.ExitCode);
            res.StandardOutput.Should().Be(expected.StandardOutput);
            res.StandardError.Should().Contain(expected.StandardError);
        }
    }
}
