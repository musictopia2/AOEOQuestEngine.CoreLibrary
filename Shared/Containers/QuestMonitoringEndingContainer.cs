namespace AOEOQuestEngine.CoreLibrary.Shared.Containers;
public class QuestMonitoringEndingContainer
{
    //i like the idea of populating the actions for 3 different quest endings including no monitoring.
    public Action? OnQuestSuccessful { get; set; }
    public Action? OnQuestFailed { get; set; }
    public Action? OnNoMonitorDetected { get; set; }
}