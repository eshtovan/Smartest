using System.Collections.Generic;
using Smartest.Infrastructure.Objects;
using Smartest.Infrastructure.Interfaces;
using System.Collections.ObjectModel;
using Smartest.Utilities;

namespace Smartest.ViewModels.BaseViewModels
{
    public class ItemsSourceBaseViewModel : BaseViewModel
    {
        private readonly IConfigurationDataService _dataService;
        private readonly IGlobalConfigService _globalSettings;
        private string _configurationName;
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
        public ItemsSourceBaseViewModel(IConfigurationDataService dataService,string configurationName, IGlobalConfigService globalSettings)
        {
            _dataService = dataService;
            _configurationName = configurationName;
            _globalSettings = globalSettings;
            //Get according to the configurationName the wanted items
            ItemsCollection = _dataService.GetItemsCollection(configurationName, globalSettings.Get("BasePath").ToString());
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
            var placedItem = _placedItemsDictionary[itemName];
            if (placedItem != null)
            {
                //Remove Selected item to SelectedItems List
                _placedItemsCollection.Remove(placedItem);
                _placedItemsDictionary.Remove(itemName);
            } 
        }

        public void SendUnityCommand() //Unity Communication Object
        {
            //TODO
        }

      
    }
} 