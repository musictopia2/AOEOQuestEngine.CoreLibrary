namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public interface IComputerDatasetGenerator
{
    Task GenerateDatasetAsync(string civCode, int tier, int characterLevel);
}
