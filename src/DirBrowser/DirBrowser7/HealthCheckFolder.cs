using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DirBrowser7;

public class HealthCheckFolder : IHealthCheck
{
    private readonly string folder;

    public HealthCheckFolder(string folder)
    {
        this.folder = folder;
    }
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        await Task.Yield();
        try
        {
            if(!Directory.Exists(folder)) {  return HealthCheckResult.Unhealthy($"{folder} do no exists"); }
            var di=new DirectoryInfo(folder);
            var hasItems = (di.EnumerateDirectories().Any() || di.EnumerateFiles().Any());
            if (!hasItems)
                return HealthCheckResult.Degraded($"{folder} has no items");
            return HealthCheckResult.Healthy($"{folder} is found and has items");
        }

        catch(Exception ex)
        {
            return HealthCheckResult.Unhealthy($"try to find {folder} ",ex);
        }
    }
}
