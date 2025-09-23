namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public class StrictMilestoneValidator : IMilestoneValidator
{
    void IMilestoneValidator.Validate(IReadOnlyList<string> techs)
    {
        if (techs.Count != 12)
        {
            throw new CustomBasicException($"Expected 12 milestone techs, but got {techs.Count}.");
        }
        techs.EnsureValidMilestoneNames();
    }
}