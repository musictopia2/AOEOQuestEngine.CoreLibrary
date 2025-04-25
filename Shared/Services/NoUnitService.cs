namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class NoUnitService : IUnitRegistry
{
    IUnitHandler? IUnitRegistry.GetHandlerFor(string protoName)
    {
        return null; //because this has no units.
    }
}