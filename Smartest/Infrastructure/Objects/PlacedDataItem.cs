using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartest.ViewModels;

namespace Smartest.Infrastructure.Objects
{
   

    public class PlacedDataItem : BaseViewModel
    {
        private string _itemName;

        public string ItemName
        {
            get => _itemName;
            set
            {
                _itemName = value;
                RaisePropertyChanged();
            }
        }

        public string LocationPath { get; set; }

        public string ConfigurationPath { get; set; }

        public bool IsConfigurationExists { get; set; } = false;

        //
        public PlacedDataItem(string itemName , string locationPath)
        {
            ItemName = itemName;
            LocationPath = locationPath;
            var configFile = Path.Combine(locationPath, itemName + ".conf");
            if (File.Exists(configFile))
            {
                IsConfigurationExists = true;
                ConfigurationPath = configFile;
            }
        }

        public PlacedDataItem(string itemName)
        {
            ItemName = itemName;
            LocationPath = null;
            IsConfigurationExists = false;

        }

        public void UpdateConfigFile()
        {
            if (IsConfigurationExists)
            {
                var newConfig = Path.Combine(LocationPath, ItemName + ".conf");
                if (!File.Exists(newConfig))
                {
                    File.Move(ConfigurationPath, newConfig);
                }

                ConfigurationPath = newConfig;
            }
         
        }
    }
}
