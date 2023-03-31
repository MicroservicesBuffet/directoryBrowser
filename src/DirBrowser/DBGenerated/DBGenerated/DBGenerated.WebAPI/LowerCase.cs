using System.Text.Json;

namespace DBGenerated.WebAPIWebAPI
{
    public class LowerCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return name;

            return name.ToLower();
        }
    }
}