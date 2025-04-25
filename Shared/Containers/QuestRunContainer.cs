namespace AOEOQuestEngine.CoreLibrary.Shared.Containers;
public class QuestRunContainer
{
    public void StartPlaying()
    {
        IsPlaying = true;
    }
    public void StopPlaying()
    {
        IsPlaying = false;
    }
    public bool IsPlaying { get; private set; } = false; //if you are playing, then something can decide what to do.
}