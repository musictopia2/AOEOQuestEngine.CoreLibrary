namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public abstract class BaseAddStronglyTypedService : IAddStronglyTypedTechsService
{
    public BasicList<BasicTechModel> AdditionalTechs { get; set; } = [];

    public abstract void ApplyTechsToTree();
}