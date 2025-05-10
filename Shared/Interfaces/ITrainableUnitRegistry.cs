namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface ITrainableUnitRegistry
{
    ITrainableUnitHandler GetHandlerFor(string protoName);
}