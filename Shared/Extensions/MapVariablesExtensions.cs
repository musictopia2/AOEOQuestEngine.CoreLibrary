namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
//yuo need public so i can experiment more.
public static class MapVariablesExtensions
{
    extension (XElement source)
    {
        public void DeleteExistingMapVariable(string name)
        {
            var maps = source.Element("randommap")!.Element("mapvariables");
            foreach (var map in maps!.Elements())
            {
                if (map.Attribute("name")!.Value == name)
                {
                    map.Remove();
                    return;
                }
            }
        }
        public void AddMapIntegerVariable(string name, int value)
        {
            source.AddMapVariable("Int", name, value);
        }
        public void AddMapStringVariable(string name, string value)
        {
            source.AddMapVariable("String", name, value);
        }
        public void AddMapBoolVariable(string name, bool value)
        {
            source.AddMapVariable("Bool", name, value);
        }
        private void AddMapVariable(string type, string name, object value)
        {
            var maps = source.Element("randommap")!.Element("mapvariables");
            string input = $"""
            <variable name="{name}" type="{type}" >{value}</variable>
            """;
            XElement adds = XElement.Parse(input);
            maps!.Add(adds);
        }
    }
    
}