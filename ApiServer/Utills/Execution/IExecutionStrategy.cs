using CliWrap;
using CliWrap.Buffered;

namespace Api.Utills.Execution
{
    public interface IExecutionStrategy
    {
        public static Type Type { get; } = typeof(IExecutionStrategy);
        public string FileExtension { get; }
        public Task<BufferedCommandResult> Run(string fileName, PipeSource pipeSource);
    }
}
