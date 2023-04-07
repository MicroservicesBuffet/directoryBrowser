
using IPluginDirBrowser;
using System.Net.Mail;

namespace EmailPlugin;
public class EmailSender : ISaveFile
{
    private EmailSettings? settings;
    public  Task<bool> SetSettings(string settings)
    {
        
        this.settings = System.Text.Json.JsonSerializer.Deserialize<EmailSettings>(settings);
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
        return type.AssemblyQualifiedName?? type.Name;
    }
}