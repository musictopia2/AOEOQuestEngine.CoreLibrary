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
            XElement? trainPoints = unitElement.Element("TrainPoints");

            if (trainPoints is null)
            {
                trainPoints = new XElement("TrainPoints", "0.0000");
                unitElement.Add(trainPoints);
            }
            else
            {
                trainPoints.Value = "0.0000";
            }
        }
    }
}