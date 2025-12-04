namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
public static class UnitHandlerExtensions
{
    extension <T>(T handler)
        where T: IUnitHandler
    {
        public void RegisterStandardUnit(XElement root)
        {
            var unitElement = root.Elements()
               .SingleOrDefault(x => (string?)x.Attribute("name") == T.SupportedProtoName);
            if (unitElement == null)
            {
                // Optional: log warning
                return;
            }
            var trainPoints = unitElement.Element("TrainPoints");
            trainPoints?.Value = "0.0000";
        }
    }
}