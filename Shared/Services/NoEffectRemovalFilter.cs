namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class NoEffectRemovalFilter : IEffectRemovalFilter
{
    bool IEffectRemovalFilter.ShouldRemove(BasicEffectModel effect)
    {
        return false;
    }
}