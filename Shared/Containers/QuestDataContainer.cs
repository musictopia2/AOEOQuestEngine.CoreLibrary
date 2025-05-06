namespace AOEOQuestEngine.CoreLibrary.Shared.Containers;
public class QuestDataContainer(ICivilizationContext civContext) : IConfigurableQuestData
{
    public TechMatrixService TechData { get; private set; } = new();
    private readonly TechIDManager _techIdManager = new();
    // Quest-specific settings
    private int _delayedAttackTime;
    private bool _seeAllMap;
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

    // Reset quest-related settings
    public void Clear()
    {
        TechData = new();
        _techIdManager.Clear(); //i think.
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