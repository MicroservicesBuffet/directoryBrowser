namespace IPluginDirBrowser;

public interface ISaveFile<T>
{
    public Task<bool> Save(string user,string nameFile,string prevFileContent,string actFileContent);
    public Task<bool> GetSettings(T settings); 
}