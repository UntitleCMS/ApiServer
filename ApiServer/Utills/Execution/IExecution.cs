using CliWrap.Buffered;

namespace Api.Utills.Execution
{
    public interface IExecution
    {
        Task<BufferedCommandResult> Run(Type stratege, SourceCode sourceCode);
    }
}