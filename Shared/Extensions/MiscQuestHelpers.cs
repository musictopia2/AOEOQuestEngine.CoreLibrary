namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
public static class MiscQuestHelpers
{
    extension (XElement source)
    {
        public string QuestTitle
        {
            get
            {
                string parts = source.Element("displayname")!.Value;
                parts = parts.Replace("$$", "");
                int value = int.Parse(parts);
                string output = value.GetQuestStringValue();
                output = output.Replace(" (Archive)", "");
                output = output.Trim(); //we don't care if its archive or not.
                return output;
            }
        }
        //had to make this one public so can create a custom class to remove challenges but do other things.
        public void RemoveOptionalChallenges()
        {
            var list = source.Elements("secondaryobjectives");
            list.Remove();
        }
        //needs public so i can have a transformer that removes timers and do other things for experiments.
        public void RemoveTimers()
        {
            string content = source.ToString();
            if (content.Contains("<failonexpire>true</failonexpire>"))
            {
                source.Element("timer")!.Element("hideonui")!.Value = "true";
            }
        }
    }
}