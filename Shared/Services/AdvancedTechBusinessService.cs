namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class AdvancedTechBusinessService(IAddStronglyTypedTechsService additions) : ITechBusinessService
{
    async Task ITechBusinessService.DoAllTechsAsync()
    {
        BasicList<BasicTechModel> techs = [];
        additions.AdditionalTechs = techs;
        TechTreeServices.Reset(); //i think needs to do here.
        additions.ApplyTechsToTree();
        if (techs.Count == 0)
        {
            await ff1.FileCopyAsync(dd1.RawTechLocation, dd1.NewTechLocation);
            return; //because there was none.
        }
        TechTreeServices.SaveAdditionalTechsToGameFolder(techs);
    }
}