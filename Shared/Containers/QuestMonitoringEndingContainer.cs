namespace AOEOQuestEngine.CoreLibrary.Shared.Containers;
public class QuestMonitoringEndingContainer
{
    //i like the idea of populating the actions for 3 different quest endings including no monitoring.
    public Action? QuestSuccessful { get; set; }
    public Action? QuestFailed { get; set; }
    public Action? NoMonitor { get; set; }
}