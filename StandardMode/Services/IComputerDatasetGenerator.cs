namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public interface IComputerDatasetGenerator
{
    void GenerateDatasetAsync(string civCode, int tier, int characterLevel);
}
