namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
public static class UnitHandlerExtensions
{
    public static void RegisterStandardUnit<T>(this T handler, XElement root)
        where T : IUnitHandler
    {
        var unitElement = root.Elements()
           .SingleOrDefault(x => (string?)x.Attribute("name") == T.SupportedProtoName);
        if (unitElement == null)
        {
            // Optional: log warning
            return;
        }
        var trainPoints = unitElement.Element("TrainPoints");
        if (trainPoints != null)
        {
            trainPoints.Value = "0.0000";
        }
    }
}