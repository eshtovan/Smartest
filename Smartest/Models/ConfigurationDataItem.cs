using Smartest.Utilities;
using System.IO;

namespace Smartest.Infrastructure.Models
{
    /// <summary>
    /// Data of the Items (sensors, terrain, Stands, etc...)
    /// </summary>
    public class ConfigurationDataItem
    {
        public string ItemName { get; set; }
        public DirectoryInfo ItemDirectoryInfo { get; set; }
        public string ItemIconPath {
            get
            {
                return Path.Combine(ItemDirectoryInfo.FullName, "Icon.png");
            }
        } 
        public ConfigurationDataItem(DirectoryInfo folderInfo)
        { 
            ItemName = folderInfo.Name;
            ItemDirectoryInfo = folderInfo; 
        }


        public bool IsConfigurationExist
        {
            get
            {
                return FoldersHelper.CheckIfConfigFileExists(ItemDirectoryInfo.FullName);
            }
        }
        //TODO Add Get Configuration function

       

    }
}
