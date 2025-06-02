namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class JsonQuestResultPersistenceService : IQuestResultPersistenceService
{
    private const string FileName = "pending_quest_result.json";
    private readonly string _filePath;
    public JsonQuestResultPersistenceService()
    {
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileName);
    }
    async Task IQuestResultPersistenceService.ClearPendingAsync()
    {
        await ff1.DeleteFileAsync(_filePath);
    }
    async Task<QuestResultModel?> IQuestResultPersistenceService.LoadPendingAsync()
    {
        if (ff1.FileExists(_filePath) == false)
        {
            return null;
        }
        QuestResultModel result = await js1.RetrieveSavedObjectAsync<QuestResultModel>(_filePath);
        return result;
    }
    async Task IQuestResultPersistenceService.SaveAsync(QuestResultModel result)
    {
        await js1.SaveObjectAsync(_filePath, result);
    }
}