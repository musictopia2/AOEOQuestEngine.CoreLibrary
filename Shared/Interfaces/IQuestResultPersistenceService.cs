namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface IQuestResultPersistenceService
{
    Task SaveAsync(QuestResultModel result);
    Task<QuestResultModel?> LoadPendingAsync();
    Task ClearPendingAsync();
}