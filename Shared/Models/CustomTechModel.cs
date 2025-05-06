namespace AOEOQuestEngine.CoreLibrary.Shared.Models;
public class CustomTechModel
{
    public string Name { get; set; } = ""; //if you specify a name for this, will use but if not, will generate one automatically when creating the tech.
    public EnumActivationType ActivationType { get; set; }
    public EnumRecipentType RecipientType { get; set; }
    public int Time { get; set; }
    public int StartTime { get; set; }
    public int EndTime { get; set; }
    public BasicList<BasicEffectModel> Effects { get; set; } = [];
    public BasicList<BasicPrereqModel> Prereqs { get; set; } = [];
    public BasicList<CustomUnitModel> Units { get; set; } = [];
}