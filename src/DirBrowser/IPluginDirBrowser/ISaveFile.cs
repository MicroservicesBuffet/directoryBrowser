namespace IPluginDirBrowser;

public interface ISaveFile
{
    public string GetName();
    public Task<bool> Save(string user,string nameFile,string prevFileContent,string actFileContent);
    public Task<bool> SetSettings(string settings); 
}