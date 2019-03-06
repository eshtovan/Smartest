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

        //Path.Combine(sourcePath, dataItem.ItemName + ".conf")
        public PlacedDataItem(string itemName , string locationPath)
        {
            ItemName = itemName;
            LocationPath = locationPath;
        }
    }
}
