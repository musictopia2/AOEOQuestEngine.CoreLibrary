namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface IUnitRegistry
{
    IUnitHandler? GetHandlerFor(string protoName);
}