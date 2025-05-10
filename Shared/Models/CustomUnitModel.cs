namespace AOEOQuestEngine.CoreLibrary.Shared.Models;
/// <summary>
/// Represents a basic unit model, mainly for template definitions and quantity tracking for consumables.
/// </summary>
public record struct CustomUnitModel(string ProtoName, int HowMany);