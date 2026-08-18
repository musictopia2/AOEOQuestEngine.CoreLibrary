namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public class DefaultComputerDatasetGenerator : IComputerDatasetGenerator
{
    Task IComputerDatasetGenerator.GenerateDatasetAsync(string civCode, int tier, int characterLevel)
    {
        return Task.CompletedTask;
    }
}