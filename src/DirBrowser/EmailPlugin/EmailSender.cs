
using IPluginDirBrowser;
using System.Net.Mail;
using System.Text.Json;

namespace EmailPlugin;
public class EmailSender : ISaveFile
{
    private EmailSettings settings=new();
    public  Task<bool> SetSettings(string settings)
    {
        var dict = JsonSerializer.Deserialize<Dictionary<string,string>>(settings);
        ArgumentNullException.ThrowIfNull(dict);
        var name=GetName();
        this.settings.To = dict[$"{name}:To"];
        this.settings.From = dict[$"{name}:From"];
        this.settings.Host = dict[$"{name}:Host"];
        this.settings.UseDefaultCredentials =bool.Parse( dict[$"{name}:UseDefaultCredentials"]);
        var port= dict[$"{name}:Port"];
        if(port != null)
        {
            this.settings.Port = int.Parse(port);
        }


        return Task.FromResult(true);
    }

    public async Task<bool> Save(string user, string nameFile, string prevFileContent, string actFileContent)
    {
        ArgumentNullException.ThrowIfNull(settings);
        await Task.Delay(1000);
        var mm = new MailMessage(settings.To ?? "", settings.From ?? "");
        mm.Subject = $"Changed {nameFile} by {user}";
        mm.Body = $"Changed {nameFile} by {user}";

        mm.Attachments.Add(Attachment.CreateAttachmentFromString(prevFileContent, "old_" + nameFile));
        mm.Attachments.Add(Attachment.CreateAttachmentFromString(actFileContent, "new_" + nameFile));
        SmtpClient smtpClient = new ();
        smtpClient.Host = settings.Host ?? "localhost";
        
        if (settings.Port != null)
            smtpClient.Port = settings.Port??0;

        smtpClient.UseDefaultCredentials = settings.UseDefaultCredentials;
        
        smtpClient.Send(mm);
        return true;
    }

    public string GetName()
    {
        var type = this.GetType();
        return type.Name;
    }
}