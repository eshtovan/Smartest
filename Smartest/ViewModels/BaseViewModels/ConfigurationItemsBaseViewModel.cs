using GalaSoft.MvvmLight.Command;
using Smartest.Infrastructure.Models;
using Smartest.Infrastructure.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Smartest.ViewModels.BaseViewModels
{
    public class ConfigurationItemsBaseViewModel : BaseViewModel
    {
        private readonly IConfigurationDataService _dataService;
        private readonly IGlobalConfigService _globalSettings;

        private ObservableCollection<ConfigurationDataItem> itemsCollection = new ObservableCollection<ConfigurationDataItem>();

        private ObservableCollection<ConfigurationDataItem> selectedItemsCollection = new ObservableCollection<ConfigurationDataItem>();
         
        public ObservableCollection<ConfigurationDataItem> ItemsCollection
        {
            get
            {
                return itemsCollection;
            }
            set
            {
                itemsCollection = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ConfigurationDataItem> SelectedItemsCollection
        {
            get
            {
                return selectedItemsCollection;
            }
            set
            {
                selectedItemsCollection = value;
                RaisePropertyChanged();
            }
        }
        public ConfigurationItemsBaseViewModel(IConfigurationDataService dataService,string configurationName, IGlobalConfigService globalSettings)
        {
            _dataService = dataService;
            _globalSettings = globalSettings; 
            ItemsCollection = dataService.GetItemsCollection(configurationName, globalSettings.Get("BasePath").ToString());
            AddSelectedDataItem = new RelayCommand<ConfigurationDataItem>(OnAddItemCommandClicked);
        }
        
        private void OnAddItemCommandClicked(ConfigurationDataItem dataitem)
        {
            //Add Selected item to SelectedItems List
            SelectedItemsCollection.Add(dataitem);
            //TODO 
            //Send Message to Unity - To spone item in to Scene
        }
        public ICommand AddSelectedDataItem { get; private set; }
         
    }
}
