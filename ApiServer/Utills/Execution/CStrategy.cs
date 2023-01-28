using CliWrap;
using CliWrap.Buffered;
using System.Text;

namespace Api.Utills.Execution
{
    public class CStrategy : IExecutionStrategy
    {
        public static Type Type { get; } = typeof(CStrategy);
        public string FileExtension => "c";

        public async Task<BufferedCommandResult> Run(string fileName, PipeSource pipeSource)
        {
            var res = await Cli.Wrap("docker")
                    .WithArguments($"exec -i --user compiler -w /compiler compiler-container bash -c \"gcc {fileName} -o {fileName}.out && ./{fileName}.out\"")
                    .WithStandardInputPipe(pipeSource)
                    .WithValidation(CommandResultValidation.None)
                    .ExecuteBufferedAsync(Encoding.UTF8);
            return res;

        }
    }
}
