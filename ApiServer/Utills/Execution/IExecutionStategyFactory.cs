namespace Api.Utills.Execution
{
    public interface IExecutionStategyFactory
    {
        public IExecutionStrategy CreateExecutionStrategy(Type type);
    }
}
