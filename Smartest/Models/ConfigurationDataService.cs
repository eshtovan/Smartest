using Smartest.Infrastructure.Interfaces;
using Smartest.Infrastructure.Objects;
using Smartest.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartest.Models
{
    public class ConfigurationDataService : IConfigurationDataService
    {
        public ObservableCollection<ConfigurationDataItem> GetItemsCollection(string collectionName,string path)
        {
            var configurationCollection = new ObservableCollection<ConfigurationDataItem>();

            var subfolders = FoldersHelper.GetSubFolders(Path.Combine(path, collectionName));

            foreach(var folder in subfolders)
            {
                configurationCollection.Add(new ConfigurationDataItem(folder));
            }

            return configurationCollection; 
        }
    }
}
