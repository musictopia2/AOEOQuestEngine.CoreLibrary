namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class NoTrainableUnitsService : ITrainableUnitRegistry
{
    ITrainableUnitHandler ITrainableUnitRegistry.GetHandlerFor(string protoName)
    {
        throw new CustomBasicException("There are no trainable units supported");
    }
}