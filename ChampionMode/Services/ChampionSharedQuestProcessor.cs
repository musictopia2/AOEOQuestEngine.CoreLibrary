﻿namespace AOEOQuestEngine.CoreLibrary.ChampionMode.Services;
public class ChampionSharedQuestProcessor(
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
    IRmsHandler rmsHandler
    )
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
        source.MakeChampionMode();
        questExtensions.ApplyExtensions(source);

        


        source.Save(dd1.NewQuestPath);
        //string content = ff1.AllText(dd1.NewQuestPath);
        //content = content.Replace("<onlycountelites>true</onlycountelites>", "<onlycountelites>false</onlycountelites>");
        //ff1.WriteAllText(dd1.NewQuestPath, content); //i think i need this too.
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