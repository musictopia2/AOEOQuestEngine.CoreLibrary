namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class BasicTacticsBusinessService(ITacticsAutomation automation) : ITacticsBusinessService
{
    void ITacticsBusinessService.DoAllTactics()
    {
        XElement source = XElement.Load(dd1.RawTacticsTownCenterLocation);
        source = automation.GetAutomatedTactics(source, false);
        source.Save(dd1.NewTacticsTownCenterLocation);
    }
}