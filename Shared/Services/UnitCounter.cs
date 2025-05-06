namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class UnitCounter
{
    private int _counter = 1;
    public void Reset()
    {
        _counter = 1;
    }
    public int GetNextUnitId => _counter++;
}