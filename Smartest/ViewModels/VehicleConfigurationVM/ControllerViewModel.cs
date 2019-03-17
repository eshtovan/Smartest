using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Smartest.Infrastructure.Interfaces;
using Smartest.Infrastructure.Objects;
using Smartest.Utilities;
using Smartest.ViewModels.BaseViewModels;

namespace Smartest.ViewModels.VehicleConfigurationVM
{
    public class ControllerViewModel : ItemsSourceBaseViewModel
    {
        private readonly IConfigurationDataService _dataService;
        private readonly INavigation _navigationManager;
        public string ConfigurationName { get; }

        public ICommand AddSelectedDataItem { get; }

        public ICommand DeleteSelectedDataItem { get; }

        public ICommand EditSelectedDataItem { get; }

        public ICommand ItemDoubleClicked { get; }

        public ControllerViewModel(IConfigurationDataService dataService, IGlobalConfigService globalSettings,
            INavigation navigationManager) : base(dataService,"Controllers", globalSettings)
        {
            _dataService = dataService;
            _navigationManager = navigationManager;
            ConfigurationName = "Controllers";
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
                _navigationManager.GoToPage("Controllers",Enums.Pages.ConfigurationVm);
            }

        }


        #region Add Item To List

        private void OnAddItemCommandClicked(ConfigurationDataItem dataItem)
        {
            var placedItem = _dataService.CopyConfigurationAndAddToList(dataItem, ConfigurationName);

            AddItemToSelectedCollection(placedItem);
            //TODO 
            //Send Message to Unity - To spone item in to Scene
            SendUnityCommand();

            //Switch View to configuration if exists 
            if (dataItem.IsConfigurationExist)
            {
                _navigationManager.GoToPage("Controllers", Enums.Pages.ConfigurationVm);
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
