namespace AOEOQuestEngine.CoreLibrary.Shared.Containers;
public class QuestDataContainer(ICivilizationContext civContext) : IConfigurableQuestData, ILocalizedStringService
{
    public TechMatrixService TechData { get; private set; } = new();
    private readonly TechIDManager _techIdManager = new();
    private TechNameService _techNameService = new();
    private readonly LocalizedStringManager _tables = new();
    // Quest-specific settings
    private int _delayedAttackTime;
    private bool _seeAllMap;
    private EnumQuestNotificationMode _questNotificationMode;
    public string GetNextName(string key) => _techNameService.GetNext(key);
    public string InsertLocalizedString(string value) => _tables.InsertLocalizedString(value);
    int IConfigurableQuestData.DelayedAttackTime
    {
        get => _delayedAttackTime;
        set => _delayedAttackTime = value;
    }
    bool IConfigurableQuestData.SeeAllMap
    {
        get => _seeAllMap;
        set => _seeAllMap = value;
    }
    public int GetNextTechID
    {
        get
        {
            int id = _techIdManager.GetNextID;
            _techIdManager.RegisterTech(); //go ahead and register.
            return id;
        }
    }
    public string CivAbb => civContext!.CivAbb;

    EnumQuestNotificationMode IConfigurableQuestData.QuestNotificationMode
    {
        get => _questNotificationMode;
        set => _questNotificationMode = value;
    }
    public BasicList<TrainableUnitModel> TrainableUnits { get; set; } = [];
    public BasicList<SimpleUnitConsumableModel> Consumables { get; set; } = [];

    // Reset quest-related settings
    //needs to be public because a person may have to clear this and do other things for experiments.
    public async Task ClearAsync()
    {
        TechData = new();
        Consumables.Clear();
        _techNameService = new(); //just create a new one here.
        _techIdManager.Clear(); //i think.
        await _tables.ResetAsync();
        TrainableUnits.Clear();
        _delayedAttackTime = 0;
        _seeAllMap = false;
    }
    public void ApplySimpleQuestSettings(XElement source)
    {
        if (_delayedAttackTime > 0)
        {
            SetDelayedAttacksForAllComputerPlayers(source, _delayedAttackTime);
        }
        if (_seeAllMap)
        {
            source.AddMapBoolVariable("SeeAll", true);
        }
        string serverVariable;
        int value;
        serverVariable = "QuestNotificationMode";
        if (_questNotificationMode == EnumQuestNotificationMode.None)
        {
            return; //default
        }
        if (_questNotificationMode == EnumQuestNotificationMode.OcrFriendly)
        {
            value = 1;
        }
        else if (_questNotificationMode == EnumQuestNotificationMode.UiFriendly)
        {
            value = 2;
        }
        else
        {
            return;
        }
        source.AddMapIntegerVariable(serverVariable, value);
        //do the simple ones here.  the more complex do somewhere else.
    }
    private static void SetDelayedAttacksForAllComputerPlayers(XElement source, int time)
    {
        var list = source.GetAllComputerPlayerElements();
        BasicList<string> fins = [];
        foreach (var temp in list)
        {
            var id = temp.Attribute("id")!.Value;
            fins.Add($"P{id}AttackDelayMod");
        }
        foreach (var player in fins)
        {
            source.DeleteExistingMapVariable(player);
        }
        foreach (var player in fins)
        {
            source.AddMapIntegerVariable(player, time);
        }
    }
    public void ProcessConsumables()
    {
        //later will process.
        foreach (var item in Consumables)
        {
            if (item.DisplayName == "")
            {
                throw new CustomBasicException("Must have a name for the consumable so can keep track of which item for useage");
            }
            if (item.Cooldown <= 0)
            {
                throw new CustomBasicException("Must have a cooldown");
            }
            if (item.Effects.Count > 0)
            {
                if (item.Description == "")
                {
                    throw new CustomBasicException("Must have description for the consumable");
                }
                if (item.ActiveTime <= 0)
                {
                    throw new CustomBasicException("Must have an active time");
                }
                if (item.Cooldown <= item.ActiveTime)
                {
                    throw new CustomBasicException("Must have at least some time in between when the active time expires and when you can activate it again");
                }
            }
            if (item.Effects.Count > 0 && item.Units.Count > 0)
            {
                throw new CustomBasicException("Cannot have both effects and units");
            }
            if (item.ForComputer)
            {
                if (item.VillagersToSpawn > 0 || item.Units.Count > 0)
                {
                    throw new CustomBasicException("The computer cannot get villagers or units");
                }
            }
            if (item.Units.Count == 0 && item.Effects.Count == 0 && item.VillagersToSpawn == 0)
            {
                throw new CustomBasicException("There was no tech here");
            }
            string name;
            if (string.IsNullOrEmpty(item.BaseName))
            {
                name = "DefaultConsumable";
            }
            else
            {
                name = item.BaseName;
            }
            item.MaxUses.Times(x =>
            {
                string generatedName = GetNextName(name);
                //only human would ever do this.
                CustomTechModel model = new();
                model.Name = generatedName;

                model.Units = item.Units;
                if (item.ForComputer == false)
                {
                    model.Effects = item.Effects;
                    model.VillagersToSpawn = item.VillagersToSpawn;
                    model.RecipientType = EnumRecipentType.Human;
                }
                //this will not have any prereqs
                model.Prereqs = item.Prereqs; //no cost because its a consumable so its free.
                model.ResearchPoints = "1.000"; //so its trainable
                model.ActivationType = EnumActivationType.Forever; //hopefully does not matter
                model.DisplayName = item.DisplayName;
                model.Details = item.Description;
                if (item.Units.Count > 0 || item.VillagersToSpawn > 0)
                {
                    model.NormalizeConsumableTechState(x, item.MaxUses, item.ActiveTime, item.Cooldown);
                }
                else
                {
                    model.NormalizeConsumableTechState(item.DisplayName, item.Description, x, item.MaxUses, item.ActiveTime, item.Cooldown);
                }
                if (item.ForComputer == false)
                {
                    model.Validate(); //since this says no techs.  this is exception because something else will have the techs.
                }
                item.GeneratedTechs.Add(model);
            });
            if (item.MaxUses != item.GeneratedTechs.Count)
            {
                throw new CustomBasicException("The number of generated techs does not match the expected MaxUses value.");
            }
            //i don't think that i need a name for the computer tech being enabled.
        }
    }
}