using Smartest.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartest.Infrastructure.Interfaces
{
   public interface IConfigurationDataService
    {  
        ObservableCollection<ConfigurationDataItem> GetItemsCollection(string collectionName,string path);
         

    }
}
