using CliWrap;
using CliWrap.Buffered;
using System.Text;

namespace Api.Utills.Execution
{
    public class PythonStrategy : IExecutionStrategy
    {
        public static Type Type => typeof(PythonStrategy);

        public string FileExtension => "py";

        public async Task<BufferedCommandResult> Run(string fileName, PipeSource pipeSource)
        {
            var res = await Cli.Wrap("docker")
                    .WithArguments($"exec -i --user compiler -w /compiler compiler-container python3 {fileName}")
                    .WithStandardInputPipe(pipeSource)
                    .WithValidation(CommandResultValidation.None)
                    .ExecuteBufferedAsync(Encoding.UTF8);
            return res;
        }
    }
}
