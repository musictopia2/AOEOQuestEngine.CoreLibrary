namespace AOEOQuestEngine.CoreLibrary.Shared.Containers;
public class QuestDataContainer(ICivilizationContext civContext) : IConfigurableQuestData
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

    // Reset quest-related settings
    internal async Task ClearAsync()
    {
        TechData = new();
        _techNameService = new(); //just create a new one here.
        _techIdManager.Clear(); //i think.
        await _tables.ResetAsync();
        _delayedAttackTime = 0;
        _seeAllMap= false;
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
}