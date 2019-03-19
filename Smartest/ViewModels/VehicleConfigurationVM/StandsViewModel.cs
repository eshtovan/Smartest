using Smartest.Infrastructure.Objects;
using Smartest.Infrastructure.Interfaces;
using Smartest.ViewModels.BaseViewModels;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Smartest.Utilities;

namespace Smartest.ViewModels.VehicleConfigurationVM
{
    public class StandsViewModel : ItemsSourceBaseViewModel 
    {
        private readonly IConfigurationDataService _dataService; 
        private readonly INavigation _navigationManager;
        private readonly string _configurationName; 
        public ICommand AddSelectedDataItem { get; }

        public ICommand DeleteSelectedDataItem { get; }

        public ICommand ItemDoubleClicked { get; }

        public StandsViewModel(IConfigurationDataService dataService, 
            INavigation navigationManager) : base(dataService, "Stands")
        {
            _dataService = dataService;
            _navigationManager = navigationManager;
            _configurationName = "Stands";
            AddSelectedDataItem = new RelayCommand<ConfigurationDataItem>(OnAddItemCommandClicked);
            DeleteSelectedDataItem = new RelayCommand<PlacedDataItem>(OnDeleteItemCommandClicked);

            ItemDoubleClicked = new RelayCommand<PlacedDataItem>(OnItemDoubleClickCommandClicked); 
            // TODO Load  _addedItemsDictionary on startup
        } 
        private void OnItemDoubleClickCommandClicked(PlacedDataItem placedItem)
        {
            ProjectsData.CurrentDataItem = placedItem;

            //TODO Check if config file exists
            if (placedItem.IsConfigurationExists)
            {
                _navigationManager.GoToPage("Stands", Enums.Pages.ConfigurationVm);
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
              
                _navigationManager.GoToPage("Stands", Enums.Pages.ConfigurationVm);
            }

        }

         

        #endregion


        #region Remove Item From List
        private void OnDeleteItemCommandClicked(PlacedDataItem dataItem)
        {
           var itemNameTodelete = _dataService.DeleteConfigurationAndRemoveFromList(dataItem);

           RemoveItemToSelectedCollection(itemNameTodelete);
            //TODO 
            //Send Message to Unity - To spone item in to Scene
            SendUnityCommand();
        }
         
        #endregion

    }
}
