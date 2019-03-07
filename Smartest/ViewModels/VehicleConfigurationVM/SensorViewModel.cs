using Smartest.Infrastructure.Objects;
using Smartest.Infrastructure.Interfaces;
using Smartest.ViewModels.BaseViewModels;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.IO;
using Smartest.Utilities;
using System.Collections.Generic;
using System.Windows;
using CommonServiceLocator;

namespace Smartest.ViewModels.VehicleConfigurationVM
{
    public class SensorViewModel : ItemsSourceBaseViewModel 
    {
        private readonly IConfigurationDataService _dataService; 
        private readonly INavigation _navigationManager;
        private readonly string _configurationName; 
        public ICommand AddSelectedDataItem { get; }

        public ICommand DeleteSelectedDataItem { get; }

        public ICommand ItemDoubleClicked { get; }

        public SensorViewModel(IConfigurationDataService dataService, IGlobalConfigService globalSettings,
            INavigation navigationManager) : base(dataService, "Sensors", globalSettings)
        {
            _dataService = dataService;
            _navigationManager = navigationManager;
            _configurationName = "Sensors";
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
                //  ProjectsData.LastPage = ((ViewModelLocator) Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentPage;
                 
                _navigationManager.GoToPage(Enums.Pages.ConfigurationVm);

                //((ViewModelLocator) Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentPage = ((ViewModelLocator) Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm
                //    .ConfigurationVm;
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
                //ProjectsData.LastPage = ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentPage;

                //((ViewModelLocator) Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentPage = ((ViewModelLocator) Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm
                //    .ConfigurationVm;
                 
                _navigationManager.GoToPage(Enums.Pages.ConfigurationVm);
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

        //TODO Move part to service
        //private void DeleteConfigurationAndRemoveFromList(PlacedDataItem dataItem)
        //{
        //    // RemoveItemName(dataItem.ItemName);
        //    FoldersHelper.DeleteFile(Path.Combine(dataItem.LocationPath, dataItem.ItemName + ".conf"));
        //    RemoveItemToSelectedCollection(dataItem.ItemName);
        //}


        //private void RemoveItemName(string dataItemName)
        //{
        //    if (_addedItemsDictionary.ContainsKey(dataItemName))
        //    {
        //        _addedItemsDictionary[dataItemName] = _addedItemsDictionary[dataItemName] - 1;
        //    }
        //}

        #endregion

    }
}
