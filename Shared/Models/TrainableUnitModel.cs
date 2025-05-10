namespace AOEOQuestEngine.CoreLibrary.Shared.Models;
/// <summary>
/// Represents a trainable unit with all the relevant game mechanics (costs, training points, etc.) so a custom unit can be trained
/// </summary>
public class TrainableUnitModel
{
    public string Name { get; set; } = "";
    public BasicList<CostModel> Costs { get; set; } = []; //let c# handle the costs.
    //for now, don't worry about the stats.
    public int AgeRequired { get; set; } = 1; // default at 1.  convert to 0 based.'
    public int TrainingPoints { get; set; } = 2; //should not be right away.
    public int PopulationCount { get; set; } = 1; //has to take at least one pop.
    public string DisplayName { get; set; } = "";
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new CustomBasicException("Trainable unit name is required.");
        }

        if (string.IsNullOrWhiteSpace(DisplayName))
        {
            throw new CustomBasicException($"DisplayName is required for trainable unit '{Name}'.");
        }

        if (Costs.Count == 0)
        {
            throw new CustomBasicException($"Trainable unit '{Name}' must have at least one cost.");
        }

        if (AgeRequired < 1)
        {
            throw new CustomBasicException($"AgeRequired for trainable unit '{Name}' must be >= 1.");
        }

        if (TrainingPoints < 1)
        {
            throw new CustomBasicException($"TrainingPoints for trainable unit '{Name}' must be >= 1.");
        }

        if (PopulationCount < 1)
        {
            throw new CustomBasicException($"PopulationCount for trainable unit '{Name}' must be >= 1.");
        }
    }
}