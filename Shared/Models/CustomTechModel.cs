namespace AOEOQuestEngine.CoreLibrary.Shared.Models;
public class CustomTechModel
{
    public string Name { get; set; } = "";
    public string DisplayName { get; set; } = "";
    public string Details { get; set; } = "";
    // Quest timer window
    public int Time { get; set; }
    public int StartTime { get; set; }
    public int EndTime { get; set; }

    // Time to research: "0.000" = instant (auto), else on-demand
    public string ResearchPoints { get; set; } = "0.000";

    // Costs related to this tech (e.g., resources, gold, etc.)
    public BasicList<CostModel> Costs { get; set; } = [];
    public BasicList<BasicEffectModel> Effects { get; set; } = [];
    public BasicList<BasicPrereqModel> Prereqs { get; set; } = [];
    public EnumActivationType ActivationType { get; set; }
    public EnumRecipentType RecipientType { get; set; }
    public BasicList<CustomUnitModel> Units { get; set; } = [];
    public int VillagersToSpawn { get; set; }
    public bool IsOnDemand => ResearchPoints != "0.000" || Costs.Count != 0;
    public void NormalizeConsumableTechState(
        string name,
        string description,
        int useIndex,
        int totalUses,
        int? activeDurationSeconds = null,
        int? cooldownSeconds = null)
    {
        // Display line for UI
        DisplayName = $"{name} Consumable (Use {useIndex}/{totalUses})";

        // Build description line
        string details = $"{description} — Use {useIndex} of {totalUses}";

        if (activeDurationSeconds.HasValue)
        {
            details += $"\nActive for {activeDurationSeconds.Value} seconds.";
        }

        if (cooldownSeconds.HasValue)
        {
            details += $"\nCooldown: {cooldownSeconds.Value} seconds.";
        }

        Details = details;
        RecipientType = EnumRecipentType.Human;
    }
    public void NormalizeConsumableTechState(string name, int useIndex, int totalUses)
    {
        string display = "";
        string details = "";

        if (Units.Count == 1)
        {
            var unit = Units.Single();
            display = $"{name} Consumable (Use {useIndex}/{totalUses})";
            details = $"Spawns {unit.HowMany} {unit.ProtoName}(s) — Use {useIndex} of {totalUses}";
        }
        else if (Units.Count > 1)
        {
            display = $"Multi-Unit {name} Consumable (Use {useIndex}/{totalUses})";
            details = $"Spawns multiple units — Use {useIndex} of {totalUses}";
        }
        else if (VillagersToSpawn > 0)
        {
            display = $"Villager Consumable (Use {useIndex}/{totalUses})";
            details = $"Spawns {VillagersToSpawn} Villager(s) — Use {useIndex} of {totalUses}";
        }
        else
        {
            throw new CustomBasicException("Needs to use one with descriptions if no units or villagesr");
        }
        if (string.IsNullOrWhiteSpace(DisplayName) == false && string.IsNullOrWhiteSpace(Details) == false)
        {
            NormalizeConsumableTechState(DisplayName, Details, useIndex, totalUses);
            return;
        }
        if (!string.IsNullOrEmpty(display))
        {
            DisplayName = display;
        }
        if (!string.IsNullOrEmpty(details))
        {
            Details = details;
        }
        RecipientType = EnumRecipentType.Human; //i think must be human
    }

    public void NormalizeTechState()
    {
        // Handle DisplayName and Details normalization based on Units or VillagersToSpawn
        string display = "";
        string details = "";

        // If there's exactly one unit, set display and details accordingly
        if (Units.Count == 1)
        {
            var unit = Units.Single();
            display = $"{unit.ProtoName} Consumable";
            details = $"Spawn {unit.HowMany} {unit.ProtoName}s";
        }
        // If there are villagers to spawn, set display and details for villagers
        else if (VillagersToSpawn > 0)
        {
            display = "Villager Consumable";
            details = $"Spawn {VillagersToSpawn} Villagers";
        }
        // If neither, fall back to custom display name and details
        else if (!string.IsNullOrEmpty(DisplayName) && !string.IsNullOrEmpty(Details))
        {
            display = DisplayName;
            details = Details;
        }

        // Set the normalized DisplayName and Details if they were set during normalization
        if (!string.IsNullOrEmpty(display))
        {
            DisplayName = display;
        }
        if (!string.IsNullOrEmpty(details))
        {
            Details = details;
        }

        // Optionally, you can also set additional logic here, such as adjusting the `RecipientType`
        if (IsOnDemand && RecipientType == EnumRecipentType.Human)
        {
            // The computer won't need to know about on-demand techs, so it becomes GlobalObtainable
            RecipientType = EnumRecipentType.GlobalObtainable;
        }
        if (VillagersToSpawn > 0 && RecipientType == EnumRecipentType.GlobalObtainable)
        {
            RecipientType = EnumRecipentType.Human; //must be human because the random map script must disable a tech after recieving it.
        }
    }
    public void Validate()
    {
        // Ensure there's at least one effect, unit, or villagers
        if (Effects.Count == 0 && VillagersToSpawn == 0 && Units.Count == 0)
        {
            throw new CustomBasicException("There is no real tech here");
        }
        // Check the tech recipient type rules
        if (RecipientType == EnumRecipentType.Computer)
        {
            // Computers can't have villagers or units
            if (VillagersToSpawn > 0 || Units.Count > 0)
            {
                throw new CustomBasicException("The computer can't get units or villagers");
            }
            // Computers also can't have on-demand techs
            if (IsOnDemand)
            {
                throw new CustomBasicException("The computer cannot do on demand techs");
            }
            return;  // If it's a computer tech, no need for further checks
        }
        // Villagers and units can't coexist in the same tech
        if (VillagersToSpawn > 0 && Units.Count > 0)
        {
            throw new CustomBasicException("Must have different techs for villagers vs units");
        }
        // For on-demand techs: Validate specific fields
        if (IsOnDemand)
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new CustomBasicException("On-demand techs require a name.");
            }
            if (string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(Details))
            {
                throw new CustomBasicException("On-demand techs without villagers must have a display name and details.");
            }
        }
        //not sure what other rules i have for this.
    }
}