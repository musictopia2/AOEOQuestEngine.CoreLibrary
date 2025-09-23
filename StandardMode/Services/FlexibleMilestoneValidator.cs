namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public class FlexibleMilestoneValidator : IMilestoneValidator
{
    void IMilestoneValidator.Validate(IReadOnlyList<string> techs)
    {
        techs.EnsureValidMilestoneNames();
    }
}