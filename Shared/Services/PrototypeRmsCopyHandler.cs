namespace AOEOQuestEngine.CoreLibrary.Shared.Services;
public class PrototypeRmsCopyHandler : IRmsHandler
{
    async Task IRmsHandler.CopyRmsFilesAsync()
    {
        string fileName = "Utils_GameTuning.cs";
        string newPath = Path.Combine(dd1.SpartanDirectoryPath, "rm", fileName);
        string oldPath = Path.Combine(dd1.RawGameDataPath, fileName);
        if (ff1.FileExists(oldPath) == false)
        {
            throw new FileNotFoundException($"Old File not found: {oldPath}");
        }
        if (ff1.FileExists(newPath) == false)
        {
            throw new FileNotFoundException($"New File not found: {newPath}");
        }
        await ff1.FileCopyAsync(oldPath, newPath);
    }
}