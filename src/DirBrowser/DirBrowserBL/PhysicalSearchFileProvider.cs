using Dazinator.Extensions.FileProviders.GlobPatternFilter;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Internal;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirBrowserBL
{

    
    public class PhysicalSearchFileProvider : IFileProvider, IDisposable
    {
        private  PhysicalFileProvider provider;
        private bool disposedValue;

        public PhysicalSearchFileProvider(string root, int depthSearch)
        {
            provider = new PhysicalFileProvider(root);
        }
        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            if (!subpath.Contains("*"))
            {
                var ret= provider.GetDirectoryContents(subpath);
                return ret;
            }
            if (subpath.EndsWith("/") && subpath.Length>1)
                subpath = subpath.Substring(0, subpath.Length - 1);

            if (!subpath.StartsWith("/"))
                subpath = "/"+subpath;

            var glb=new GlobPatternFilterFileProvider(provider, new string[] { "**" + subpath });
            var ret1 = glb.GetDirectoryContents("/");
            return ret1;
        }


        public IFileInfo GetFileInfo(string subpath)
        {
            
            
            var ret= provider.GetFileInfo(subpath);
            return ret;

        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    provider.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~PhysicalSearchFileProvider()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
