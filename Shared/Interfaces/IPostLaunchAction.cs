namespace AOEOQuestEngine.CoreLibrary.Shared.Interfaces;
/// <summary>
/// Represents a unit of work to be executed immediately after the game (Spartan) has been launched.
/// </summary>
/// <remarks>
/// This interface is designed for actions that must occur after the game window is opened,
/// but that are independent of the game launch process itself. Examples include:
/// - Populating UI click locations
/// - If console tests, then do nothing since nothing is required but of course, can print to console as well to let you know something happened.
/// 
/// Implementations should be safe to call in sequence and should not assume exclusive control.
/// The main ISpartanLaunchHandler can't know what was done here.  so this has to do the work required regardless of whoever is actually handling the complex actions like quest monitoring.
/// </remarks>
public interface IPostLaunchAction
{
    void RunAfterLaunch();
}