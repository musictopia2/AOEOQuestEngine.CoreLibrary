namespace AOEOQuestEngine.CoreLibrary.Shared.Models;
public class SimpleUnitConsumableModel
{
    //for each consumable, will have a separate tech for each of the uses.
    public bool ForComputer { get; set; } //if this is for computer, then
    //what would happen is the compute rwould get the techs.
    public BasicList<CustomUnitModel> Units { get; set; } = [];
    public int VillagersToSpawn { get; set; } //test with villagers too.
    //in future, can do one or the other but not both.
    public int Cooldown { get; set; }
    public int MaxUses { get; set; }
    // Optional user-friendly label
    public BasicList<BasicPrereqModel> Prereqs { get; set; } = [];
    public string BaseName { get; set; } = "";

    public string DisplayName { get; set; } = "";
    public string Description { get; set; } = ""; //must have a description this time.


    // Will be populated by the helper
    public BasicList<CustomTechModel> GeneratedTechs { get; set; } = [];



    public int ActiveTime { get; set; } //if a unit consumable has active set, then must have another tech.
    public BasicList<BasicEffectModel> Effects { get; set; } = [];

}