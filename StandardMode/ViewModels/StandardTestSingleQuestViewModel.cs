namespace AOEOQuestEngine.CoreLibrary.StandardMode.ViewModels;
public class StandardTestSingleQuestViewModel
    (IChooseCivViewModel civVM,
    IQuestLocatorService questService,
    QuestTitleContainer questTitleContainer,
    StandardSharedQuestProcessor processor
    ) : IPlayQuestViewModel
{
    private string SpecialTitle() => $"{TestConfiguration.TestingProcess} For Civilization {civVM.CivilizationChosen!.FullName}";
    string IPlayQuestViewModel.Title => civVM.Title(SpecialTitle);
    bool IPlayQuestViewModel.CanGoBack => TestConfiguration.CanGoBack;
    async Task IPlayQuestViewModel.PlayCivAsync()
    {
        if (ll1.MainLocation == "")
        {
            throw new CustomBasicException("Must set up ahead of time now.  Since locations can change");
        }
        if (dd1.NewGamePath == "")
        {
            throw new CustomBasicException("Must set the new game path ahead of time now");
        }
        questTitleContainer.QuestTitle = questService.OldQuestTitle;
        await processor.ProcessQuestAsync(questService.OldQuestPath);
    }
    void IPlayQuestViewModel.ResetCiv()
    {
        civVM.ResetCiv();
    }
}