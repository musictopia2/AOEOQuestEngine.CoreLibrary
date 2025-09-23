using System.Text.RegularExpressions;
namespace AOEOQuestEngine.CoreLibrary.StandardMode.Extensions;
public static partial class ValidationExtensions
{
    private static readonly Regex _milestoneRegex = MilestoneRegEx();
    public static void EnsureValidMilestoneNames(this IReadOnlyList<string> techs)
    {
        foreach (var tech in techs)
        {
            if (!_milestoneRegex.IsMatch(tech))
            {
                throw new CustomBasicException($"Tech '{tech}' does not follow the milestone naming pattern.");
            }
        }
    }
    [GeneratedRegex("^C(0[1-8])_L(05|10|20|30|40)_[AB]$", RegexOptions.Compiled)]
    private static partial Regex MilestoneRegEx();
}