using Api.Utills.SaveFile;
using CliWrap;
using CliWrap.Buffered;
using System.Runtime.CompilerServices;

namespace Api.Utills.Execution
{
    public class Execution : IExecution, IExecutionStategyFactory
    {
        private static readonly Execution _instance = new();
        private readonly IDictionary<Type, IExecutionStrategy> CommandStrategis;
        private readonly ISaveFileStrategy<SourceCode> SaveFileStrategy;
        private Execution()
        {
            CommandStrategis = new Dictionary<Type, IExecutionStrategy>();
            SaveFileStrategy = SaveSourceCodeToTmpDirDocker.Instant;
        }
        public static Execution Instance { get => _instance; }

        public async Task<BufferedCommandResult> Run(Type strategy, SourceCode sourceCode)
        {
            IExecutionStrategy strategyInstant = GetExecutionStrategy(strategy);
            string extension = strategyInstant.FileExtension;

            SaveFileStrategy.Save(sourceCode, out string fileName, extension);

            PipeSource input = PipeSource.FromString(sourceCode.Input);
            return await strategyInstant.Run(fileName, input);
        }

        public IExecutionStrategy GetExecutionStrategy(Type type)
        {
            if (!type.IsAssignableTo(typeof(IExecutionStrategy)))
                throw new ArgumentException
                    (
                        $"{type.FullName} Can not assign to {typeof(IExecutionStrategy)}"
                    );
            if (!CommandStrategis.ContainsKey(type))
                CommandStrategis.Add(type, CreateExecutionStrategy(type));
            return CommandStrategis[type];
        }

        public IExecutionStrategy CreateExecutionStrategy(Type type)
        {
            return (IExecutionStrategy)Activator.CreateInstance(type)!;
        }
    }
}
