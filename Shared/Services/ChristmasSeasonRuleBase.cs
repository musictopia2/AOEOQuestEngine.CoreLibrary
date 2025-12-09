namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public abstract class ChristmasSeasonRuleBase(IDateOnlyPicker picker) : ISeasonRuleEvaluator
{
    protected bool IsChristmasSeason
    {
        // your extension / logic here
        get
        {
            DateOnly date = picker.GetCurrentDate;
            return date.IsBetweenThanksgivingAndChristmas;
        }
    }
    protected static string ChristmasOnlyMessage => "These quests are only available to play between Thanksgiving and Christmas.";
    protected static string AnyButChristmasMessage => "These quests are not available to play between Thanksgiving and Christmas.";
    public abstract bool IsAllowed(out string message);
}