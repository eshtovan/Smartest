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
        private readonly IGlobalConfigService _globalSettings;
        private readonly Dictionary<string, int> _addedItemsDictionary = new Dictionary<string, int>();


        public ConfigurationDataService(IGlobalConfigService globalSettings)
        {
            _globalSettings = globalSettings;
        }
        public ObservableCollection<ConfigurationDataItem> GetItemsCollection(string collectionName)
        {
            var configurationCollection = new ObservableCollection<ConfigurationDataItem>();

            var subfolders = FoldersHelper.GetSubFolders(Path.Combine(_globalSettings.Get("ItemsPath").ToString(), collectionName));

            foreach(var folder in subfolders)
            {
                configurationCollection.Add(new ConfigurationDataItem(folder));
            }

            return configurationCollection; 
        }

        //TODO add more functionality here

        public PlacedDataItem CopyConfigurationAndAddToList(ConfigurationDataItem dataItem ,string configurationName)
        {
            var basePath = _globalSettings.Get("BasePath").ToString();
            var itemsPath = _globalSettings.Get("ItemsPath").ToString();

            var sourcePath = Path.Combine(itemsPath, configurationName, dataItem.ItemName);
            var calculatedName = CalculatedItemName(dataItem);
            string destinationPath = "";
            if (FoldersHelper.CheckIfConfigFileExists(sourcePath))
            {
                destinationPath = Path.Combine(basePath, "Projects", ProjectsData.CurrentProjectName, ProjectsData.CurrentConfigurationName, "Configurations");

                FoldersHelper.CopyFileToLocation(Path.Combine(sourcePath, dataItem.ItemName + ".conf"), destinationPath, calculatedName + ".conf");
            }

            //return Placeid
            // AddItemToSelectedCollection(calculateditemName, destinationPath); 
            return new PlacedDataItem(calculatedName, destinationPath);
        }


        private string CalculatedItemName(ConfigurationDataItem dataItem)
        {
            string calculatedName;
            if (_addedItemsDictionary.ContainsKey(dataItem.ItemName))
            {
                int numberOfShows = _addedItemsDictionary[dataItem.ItemName] + 1;
                calculatedName = dataItem.ItemName + numberOfShows;
                _addedItemsDictionary[dataItem.ItemName] = numberOfShows;
            }
            else
            {
                _addedItemsDictionary.Add(dataItem.ItemName, 0);
                calculatedName = dataItem.ItemName;
            }

            return calculatedName;
        }


        public string DeleteConfigurationAndRemoveFromList(PlacedDataItem dataItem)
        {
            // RemoveItemName(dataItem.ItemName);
            FoldersHelper.DeleteFile(dataItem.ConfigurationPath);
            return dataItem.ItemName;
        }

    }
}
