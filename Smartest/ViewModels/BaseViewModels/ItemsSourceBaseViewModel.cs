using System.Collections.Generic;
using Smartest.Infrastructure.Objects;
using Smartest.Infrastructure.Interfaces;
using System.Collections.ObjectModel;
using Smartest.Utilities;

namespace Smartest.ViewModels.BaseViewModels
{
    public class ItemsSourceBaseViewModel : BaseViewModel
    {
        // private string _configurationName;
        private ObservableCollection<ConfigurationDataItem> _itemsCollection = new ObservableCollection<ConfigurationDataItem>();

        private ObservableCollection<PlacedDataItem> _placedItemsCollection = new ObservableCollection<PlacedDataItem>();
         
        private readonly Dictionary<string, PlacedDataItem> _placedItemsDictionary = new Dictionary<string, PlacedDataItem>();
        public ObservableCollection<ConfigurationDataItem> ItemsCollection
        {
            get => _itemsCollection;
            set
            {
                _itemsCollection = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<PlacedDataItem> PlacedItemsCollection
        {
            get => _placedItemsCollection;
            set
            {
                _placedItemsCollection = value;
                RaisePropertyChanged();
            }
        }
        public ItemsSourceBaseViewModel(IConfigurationDataService dataService,string configurationName)
        {
           // _configurationName = configurationName;
           
            //Get according to the configurationName the wanted items
            ItemsCollection = dataService.GetItemsCollection(configurationName);
        }
         
        public void AddItemToSelectedCollection(PlacedDataItem placedItem)
        { 
            _placedItemsDictionary.Add(placedItem.ItemName, placedItem);
            //Add Selected item to SelectedItems List
            _placedItemsCollection.Add(placedItem);

            ProjectsData.CurrentDataItem = placedItem;
        }

        public void RemoveItemToSelectedCollection(string itemName)
        {
            if (_placedItemsDictionary.ContainsKey(itemName))
            {
                var placedItem = _placedItemsDictionary[itemName]; 
                //Remove Selected item to SelectedItems List
                _placedItemsCollection.Remove(placedItem);
                _placedItemsDictionary.Remove(itemName);
            } 
        }

        public void UpdateItemConfigurationFile(string oldItemName,PlacedDataItem dataItem)
        {
            dataItem.UpdateConfigFile();
            _placedItemsDictionary.Remove(oldItemName);
            _placedItemsDictionary.Add(dataItem.ItemName, dataItem);
        }

        public void SendUnityCommand() //Unity Communication Object
        {
            //TODO
        }

      
    }
} 