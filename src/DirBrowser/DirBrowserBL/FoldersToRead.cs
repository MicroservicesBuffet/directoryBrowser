using System;

namespace DirBrowserBL
{
    public class FolderToRead
    {
        public string Id { get; set; }
        public string FullPath { get; set; }

        public string TransformFullPath
        {
            get
            {
                if(FullPath == "./")
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                }
                return FullPath;
            }
        }
    }
}