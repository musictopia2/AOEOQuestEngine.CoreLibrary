namespace AOEOQuestEngine.CoreLibrary.Shared.Utilities;
public static class ReportHelpers
{
    public static async Task<BasicList<PossibleChooseQuestModel>> ListAllQuestFilesAsync(EnumHoliday holiday)
    {
        BasicList<PossibleChooseQuestModel> output = [];
        BasicList<string> files = await ff1.FileListAsync(dd1.NewQuestFileDirectory);
        foreach (var file in files)
        {
            string content = await ff1.AllTextAsync(file);
            XElement source = XElement.Parse(content);
            string title = source.QuestTitle;
            string name = ff1.FileName(file);
            EnumHoliday temp;
            if (name.Contains("summer", StringComparison.CurrentCultureIgnoreCase))
            {
                temp = EnumHoliday.Summer;
            }
            else if (name.Contains("winter", StringComparison.CurrentCultureIgnoreCase))
            {
                temp = EnumHoliday.Christmas;
            }
            else if (name.Contains("halloween", StringComparison.CurrentCultureIgnoreCase))
            {
                temp = EnumHoliday.Halloween;
            }
            else
            {
                temp = EnumHoliday.None;
            }
            PossibleChooseQuestModel quest = new()
            {
                Title = title,
                FileName = name,
                IsSelected = false,
            };
            if (temp == holiday)
            {
                output.Add(quest); //so its always filtered.
            }
        }
        if (output.Count == 0)
        {
            throw new CustomBasicException("There has to be at least one quest in the system.");
        }
        return output;
    }
    public static async Task<BasicList<CivilizationBasicModel>> GetCivilizationsAsync()
    {
        ICivilizationDataService data = new InMemoryCivilizationDataService();
        var list = await data.GetCivilizationsAsync();
        return list;
    }
}