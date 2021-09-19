using DirBrowserBL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DirBrowserBackground
{
    public class CheckFoldersService : BackgroundService
    {
        private readonly IServiceProvider sp;

        public CheckFoldersService(IServiceProvider sp, IConfiguration configure)
        {
            this.sp = sp;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string script = "a.ps";
                var folders = sp.GetService(typeof(FolderToRead[])) as FolderToRead[];
                if (folders?.Length > 0)
                    foreach (var item in folders)
                    {
                        var dir = item.FullPath;
                        Console.WriteLine($"process {dir}");
                        using (var ps = PowerShell.Create())
                        {
                            //var results = ps.AddScript("Get-Verb -Verb get").Invoke();

                            
                        }
                    }

                await Task.Delay(60_000);
            }
        }
    }
}
