using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartest.Infrastructure.Objects;

namespace Smartest.Utilities
{
    public static class FoldersHelper
    {

        public static List<DirectoryInfo> GetSubFolders(string path)
        {
            List<DirectoryInfo> retSubdirs = new List<DirectoryInfo>();

            if (CheckDirectory(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);

                var subdirs = di.GetDirectories();
                retSubdirs.AddRange(subdirs);
            }
            return retSubdirs;
        }

        public static string[] GetFiles(string path)
        {
            var files = Directory.GetFiles(path);
            return files;
        }

        public static bool CheckIfConfigFileExists(string path)
        {
            var files = Directory.GetFiles(path);
            bool  ret = false;
            foreach (var file in files)
            {
                if(file.Contains(".conf"))
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        private static bool CheckDirectory(string path)
        {
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return true;
        }

        public static void CopyFileToLocation(string sourcePathWithFileName, string destinationPath,string newConfigFileName)
        {
            if (!File.Exists(Path.Combine(destinationPath, newConfigFileName)))
            {
                Directory.CreateDirectory(destinationPath);
                File.Copy(sourcePathWithFileName, Path.Combine(destinationPath, newConfigFileName));
            }
        }

        public static void DeleteFile(string destinationPath)
        {
            if (File.Exists(destinationPath))
            { 
                File.Delete(destinationPath);
            }
        }
    }
}
