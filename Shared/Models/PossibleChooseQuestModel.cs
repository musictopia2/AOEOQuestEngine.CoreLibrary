namespace AOEOQuestEngine.CoreLibrary.Shared.Models;
public class PossibleChooseQuestModel : ISelectable
{
    public string Title { get; set; } = "";
    public string FileName { get; set; } = "";
    //public EnumHoliday Holiday { get; set; }
    public bool IsSelected { get; set; } //since this can be modified
}