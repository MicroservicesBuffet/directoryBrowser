
using IPluginDirBrowser;
using System.Net.Mail;

namespace EmailPlugin;
public class EmailSender : ISaveFile<EmailSettings>
{
    private EmailSettings settings;
    public async Task<bool> GetSettings(EmailSettings settings)
    {
        this.settings = settings;
        return true;
    }

    public async Task<bool> Save(string user, string nameFile, string prevFileContent, string actFileContent)
    {
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
}