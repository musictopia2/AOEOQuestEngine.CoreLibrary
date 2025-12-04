namespace AOEOQuestEngine.CoreLibrary.Shared.Extensions;
internal static class ComputerAdjustmentExtensions
{
    extension (XElement source)
    {
        public BasicList<XElement> GetAllComputerPlayerElements()
        {
            var output = source.GetComputerPlayers();
            return output;
        }
        private BasicList<XElement> GetComputerPlayers()
        {
            var list = source.Elements("playersettings");
            BasicList<XElement> output = [];
            foreach (XElement player in list)
            {
                if (player.Element("color")!.Value == "1")
                {
                    continue; //don't consider anymore.
                }
                if (player.Element("team")!.Value == "1")
                {
                    continue; //because its an ally.
                }
                if (player.Element("playertype")!.Value != "Computer")
                {
                    continue; //must be controlled by computer.
                }
                if (player.Element("script")!.Value == "")
                {
                    continue; //because if there is no script, then no adjustments to be made to computer player.
                }
                output.Add(player);
            }
            return output;
        }
    }   
}