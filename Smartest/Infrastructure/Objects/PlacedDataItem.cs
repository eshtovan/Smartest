using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartest.Infrastructure.Objects
{
   

    public class PlacedDataItem
    {
        public string ItemName { get; set; }

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
    }
}
