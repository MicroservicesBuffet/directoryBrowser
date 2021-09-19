using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DirBrowserBackground
{
    public class CheckFoldersService : BackgroundService
    {
        //private readonly IServiceProvider sp;
        private readonly IConfiguration configure;

        public CheckFoldersService(IConfiguration configure)
        {            
            this.configure = configure;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string script = "a.ps";
                var folders = new FolderToSupervise[0];
                //var folders = sp.GetService(typeof(FolderToSupervise[])) as FolderToSupervise[];
                if (folders?.Length > 0)
                    foreach (var item in folders)
                    {
                        var dir = item.FullPath;
                        Console.WriteLine($"process {dir}");
                        using (var ps = PowerShell.Create())
                        {
                            //var results = ps.AddScript("Get-Verb -Verb get").Invoke();
                            var scriptData = ps.AddScript(await File.ReadAllTextAsync(script));
                            scriptData = scriptData.AddCommand("Out-String");
                            scriptData = scriptData.AddArgument(dir);
                            var res = scriptData.InvokeAsync();
                            Console.WriteLine(res);
                        }
                    }

                await Task.Delay(60_000);
            }
        }
    }
}
