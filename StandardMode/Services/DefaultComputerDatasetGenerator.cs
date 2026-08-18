namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public class DefaultComputerDatasetGenerator : IComputerDatasetGenerator
{
    Task IComputerDatasetGenerator.GenerateAsync()
    {
        return Task.CompletedTask;
    }
}