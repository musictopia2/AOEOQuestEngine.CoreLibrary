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
        internal void RemoveOptionalChallenges()
        {
            var list = source.Elements("secondaryobjectives");
            list.Remove();
        }
    }
}