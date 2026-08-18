namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public interface IComputerDatasetGenerator
{
    void GenerateDataset(string civCode, int tier, int characterLevel, string? specialCiv = null);
}