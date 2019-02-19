using Smartest.Infrastructure.Objects;
using Smartest.Infrastructure.Interfaces;
using System.Collections.ObjectModel;

namespace Smartest.ViewModels.BaseViewModels
{
    public class ItemsSourceBaseViewModel : BaseViewModel
    {
        private readonly IConfigurationDataService _dataService;
        private readonly IGlobalConfigService _globalSettings;
        private string _configurationName;
        private ObservableCollection<ConfigurationDataItem> _itemsCollection = new ObservableCollection<ConfigurationDataItem>();

        private ObservableCollection<PlacedDataItem> _placedItemsCollection = new ObservableCollection<PlacedDataItem>();
         
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
            ItemsCollection = dataService.GetItemsCollection(configurationName, globalSettings.Get("BasePath").ToString());
        //    AddSelectedDataItem = new RelayCommand<ConfigurationDataItem>(OnAddItemCommandClicked);
        }
        
        //private void OnAddItemCommandClicked(ConfigurationDataItem dataitem)
        //{
        //    ////TODO
          
        //    //Add Selected item to SelectedItems List
        //    SelectedItemsCollection.Add(dataitem);
        //    //Copy Item Folder to the correct Project Folder (so that we can make changes to the configuration path
        //    //TODO 
        //    //Send Message to Unity - To spone item in to Scene
             

        //}

        public void AddItemToSelectedCollection(string itemName,string itemPath)
        {
            //Add Selected item to SelectedItems List
            _placedItemsCollection.Add(new PlacedDataItem(itemName,itemPath));
        }

        public void SendUnityCommand() //Unity Communication Object
        {
            //TODO
        }

        //    public ICommand AddSelectedDataItem { get; private set; }

    }
}
