namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public class StandardSharedQuestProcessor
    (
    ISecondaryObjectiveTransformer secondaryTransformer,
    IPlayQuestService playService,
    ICharacterBusinessService characterService,
    ITechBusinessService businessService,
    ITacticsBusinessService tactics,
    IUnitProcessor units,
    IPostLaunchAction post,
    IQuestPreparationHandler questPreparation,
    IQuestExtensionApplier questExtensions,
    ISpartanLaunchHandler launch,
    ISpartanUtilities spartanUtilities,
    QuestRunContainer questrunContainer,
    IRmsHandler rmsHandler)
{
    public async Task ProcessQuestAsync(string oldQuestPath)
    {
        if (ll1.MainLocation == "")
        {
            throw new CustomBasicException("Must set up ahead of time now.  Since locations can change");
        }
        if (dd1.NewGamePath == "")
        {
            throw new CustomBasicException("Must set the new game path ahead of time now");
        }
        await questPreparation.PrepareAsync();
        await characterService.CopyCharacterFilesAsync();
        //comes from the quest service.
        XElement source = XElement.Load(oldQuestPath);
        secondaryTransformer.Transform(source);
        questExtensions.ApplyExtensions(source);
        source.Save(dd1.NewQuestPath);
        source = units.GetUnitXML();
        source.Save(dd1.NewUnitLocation);
        await businessService.DoAllTechsAsync();
        tactics.DoAllTactics();
        spartanUtilities.ExitSpartan();
        await rmsHandler.CopyRmsFilesAsync();
        playService.OpenOfflineGame(dd1.SpartanDirectoryPath);
        post.RunAfterLaunch();
        questrunContainer.StartPlaying();
        launch.OnSpartanLaunched();
    }
}