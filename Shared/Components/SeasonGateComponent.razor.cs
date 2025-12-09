namespace AOEOQuestEngine.CoreLibrary.Shared.Components;
public partial class SeasonGateComponent(ISeasonRuleEvaluator seasonRule)
{
    bool _allowed;
    string _message = "";
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    protected override void OnInitialized()
    {
        _allowed = seasonRule.IsAllowed(out _message);
        base.OnInitialized();
    }
}