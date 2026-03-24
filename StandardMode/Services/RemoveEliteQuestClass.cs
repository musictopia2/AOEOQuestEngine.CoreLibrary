namespace AOEOQuestEngine.CoreLibrary.StandardMode.Services;
public class RemoveEliteQuestClass([FromKeyedServices("Standard")]
    IQuestExtensionApplier applier) : IQuestExtensionApplier
{
    void IQuestExtensionApplier.ApplyExtensions(XElement source)
    {
        //do others too.
        source.Element("elitespawnchance")!.Value = "0"; //hopefully something else saves it (?)
        //if i needed the other adjustments so no longer legendary quest, rethink here and rethink namings.
        applier.ApplyExtensions(source);
    }
}
