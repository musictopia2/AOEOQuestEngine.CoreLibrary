namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface IGlobalTechStrategy
{
    BasicList<BasicEffectModel> GetGlobalTechEffects();
    abstract bool HasGlobalTech { get; }
}