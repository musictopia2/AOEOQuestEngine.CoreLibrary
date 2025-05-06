namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
public interface IAddStronglyTypedTechsService
{
    BasicList<BasicTechModel> AdditionalTechs { get; set; }
    void ApplyTechsToTree();
}