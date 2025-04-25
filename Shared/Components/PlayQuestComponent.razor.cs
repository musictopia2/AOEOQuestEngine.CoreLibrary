namespace AOEOQuestEngine.CoreLibrary.Shared.Components;
public partial class PlayQuestComponent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public EventCallback OnPlayingQuest { get; set; }
    [Inject]
    private IProcessQuestService? Quest { get; set; }
    [Inject]
    private QuestFileContainer? DataContext { get; set; }
    protected override void OnInitialized()
    {
        if (DataContext!.QuestTitle is null || DataContext.QuestTitle == "")
        {
            throw new CustomBasicException("No quest title.  Help");
        }
    }
    private async Task PlayQuestAsync()
    {
        await Quest!.ProcessQuestAsync();
        await OnPlayingQuest.InvokeAsync();
    }
}