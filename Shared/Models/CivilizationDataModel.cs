namespace AOEOQuestEngine.CoreLibrary.Shared.Models;
public class CivilizationDataModel
{
    public string CivAbb { get; set; } = "";
    public string CivTitle { get; set; } = ""; //this way a process can run which can list civs.
    public int AverageCompletionMinuteTime { get; set; } //0 means not done.
}