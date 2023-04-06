namespace EmailPlugin;

public class EmailSettings
{
    public string? To { get; set; }
    public string? From { get; set; }
    public string? Host { get; set; }
    public int? Port { get; set; }
    public bool UseDefaultCredentials { get; set; }
}
