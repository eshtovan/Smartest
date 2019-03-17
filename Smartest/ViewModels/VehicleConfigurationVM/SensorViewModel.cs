using Smartest.Infrastructure.Objects;
using Smartest.Infrastructure.Interfaces;
using Smartest.ViewModels.BaseViewModels;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Smartest.Utilities;

namespace Smartest.ViewModels.VehicleConfigurationVM
{
    public class SensorViewModel : ItemsSourceBaseViewModel 
    {
        private readonly IConfigurationDataService _dataService; 
        private readonly INavigation _navigationManager;
        private readonly string _configurationName; 
        public ICommand AddSelectedDataItem { get; }

        public ICommand DeleteSelectedDataItem { get; }

        public ICommand EditSelectedDataItem { get; }

        public ICommand ItemDoubleClicked { get; }

        public SensorViewModel(IConfigurationDataService dataService, IGlobalConfigService globalSettings,
            INavigation navigationManager) : base(dataService, "Sensors", globalSettings)
        {
            _dataService = dataService;
            _navigationManager = navigationManager;
            _configurationName = "Sensors";
            AddSelectedDataItem = new RelayCommand<ConfigurationDataItem>(OnAddItemCommandClicked);
            DeleteSelectedDataItem = new RelayCommand<PlacedDataItem>(OnDeleteItemCommandClicked);

            EditSelectedDataItem = new RelayCommand<PlacedDataItem>(OnEditItemCommandClicked);

            ItemDoubleClicked = new RelayCommand<PlacedDataItem>(OnItemDoubleClickCommandClicked);
             
            // TODO Load  _addedItemsDictionary on startup from unity
        }

        private void OnItemDoubleClickCommandClicked(PlacedDataItem placedItem)
        {
            ProjectsData.CurrentDataItem = placedItem;

            //TODO Check if config file exists
            if (placedItem.IsConfigurationExists)
            { 
                _navigationManager.GoToPage("Sensors", Enums.Pages.ConfigurationVm);
            }

        }


        #region Add Item To List

        private void OnAddItemCommandClicked(ConfigurationDataItem dataItem)
        {
            var placedItem =  _dataService.CopyConfigurationAndAddToList(dataItem, _configurationName);

            AddItemToSelectedCollection(placedItem);
           //TODO 
           //Send Message to Unity - To spone item in to Scene
           SendUnityCommand();

            //Switch View to configuration if exists 
            if (dataItem.IsConfigurationExist)
            { 
                _navigationManager.GoToPage("Sensors", Enums.Pages.ConfigurationVm);
            }

        }

         

        #endregion


        #region Remove Item From List
        private void OnDeleteItemCommandClicked(PlacedDataItem dataItem)
        {
           var itemNameDelete = _dataService.DeleteConfigurationAndRemoveFromList(dataItem);

           RemoveItemToSelectedCollection(itemNameDelete);
            //TODO 
            //Send Message to Unity - To spone item in to Scene
            SendUnityCommand();
        }

        #endregion


        private void OnEditItemCommandClicked(PlacedDataItem dataItem)
        {
            //var itemNameTodelete = _dataService.DeleteConfigurationAndRemoveFromList(dataItem);

            //RemoveItemToSelectedCollection(itemNameTodelete);
            ////TODO 
            ////Send Message to Unity - To spone item in to Scene
            //SendUnityCommand();
        }
    }
}
