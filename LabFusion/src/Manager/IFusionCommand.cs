public interface IFusionCommand
{
    string Name { get; }
    Task<object> ExecuteAsync(List<string> args);
}