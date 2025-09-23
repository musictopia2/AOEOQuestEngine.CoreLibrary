namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public class IgnoreIfNoneMilestoneValidator : IMilestoneValidator
{
    void IMilestoneValidator.Validate(IReadOnlyList<string> techs)
    {
        if (techs.Count == 0)
        {
            return; // No milestones to validate;
        }
        if (techs.Count != 12)
        {
            throw new CustomBasicException($"Expected 12 milestone techs, but got {techs.Count}.");
        }
        techs.EnsureValidMilestoneNames();
    }
}
