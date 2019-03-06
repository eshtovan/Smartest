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
      //  private readonly IConfigurationDataService _dataService;
        private readonly IGlobalConfigService _globalSettings;
        private readonly string _configurationName;
        // Also need to check if named changed
        private readonly Dictionary<string, int> _addedItemsDictionary = new Dictionary<string, int>();

        public ICommand AddSelectedDataItem { get; }

        public ICommand DeleteSelectedDataItem { get; }

        public ICommand ItemDoubleClicked { get; }

        public SensorViewModel(IConfigurationDataService dataService, IGlobalConfigService globalSettings) :base(dataService,"Sensors", globalSettings)
        {
           // _dataService = dataService;
            _globalSettings = globalSettings;
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
            if(placedItem.IsConfigurationExists)
                ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).Main.CurrentPage = ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).Main.ConfigurationVm;

        }


        #region Add Item To List

        private void OnAddItemCommandClicked(ConfigurationDataItem dataItem)
        {
            CopyConfigurationAndAddToList(dataItem);

            //TODO 
            //Send Message to Unity - To spone item in to Scene
            SendUnityCommand();

            //Switch View to configuration if exists

            if (dataItem.IsConfigurationExist)
                ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).Main.CurrentPage = ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).Main.ConfigurationVm;

        }


        //TODO Move part to service
        private void CopyConfigurationAndAddToList(ConfigurationDataItem dataItem)
        {
            var basePath = _globalSettings.Get("BasePath").ToString();
            var sourcePath = Path.Combine(basePath, _configurationName, dataItem.ItemName);
            var calculateditemName = CalculatedItemName(dataItem);
            string destinationPath = "";
            if (FoldersHelper.CheckIfConfigFileExists(sourcePath))
            {
                destinationPath = Path.Combine(basePath, "Projects", ProjectsData.CurrentProjectName, "Configurations");

                FoldersHelper.CopyFileToLocation(Path.Combine(sourcePath, dataItem.ItemName + ".conf"), destinationPath, calculateditemName + ".conf");
            }

            //return Placeid
            AddItemToSelectedCollection(calculateditemName, destinationPath);
        }

        private string CalculatedItemName(ConfigurationDataItem dataItem)
        {
            string calculatedName;
            if (_addedItemsDictionary.ContainsKey(dataItem.ItemName))
            {
                int numberOfShows = _addedItemsDictionary[dataItem.ItemName] + 1;
                calculatedName = dataItem.ItemName + numberOfShows;
                _addedItemsDictionary[dataItem.ItemName] = numberOfShows;
            }
            else
            {
                _addedItemsDictionary.Add(dataItem.ItemName, 0);
                calculatedName = dataItem.ItemName;
            }

            return calculatedName;
        }

        #endregion


        #region Remove Item From List
        private void OnDeleteItemCommandClicked(PlacedDataItem dataItem)
        {
            DeleteConfigurationAndRemoveFromList(dataItem);

            //TODO 
            //Send Message to Unity - To spone item in to Scene
            SendUnityCommand();
        }

        //TODO Move part to service
        private void DeleteConfigurationAndRemoveFromList(PlacedDataItem dataItem)
        {
            // RemoveItemName(dataItem.ItemName);
            FoldersHelper.DeleteFile(Path.Combine(dataItem.LocationPath, dataItem.ItemName + ".conf"));
            RemoveItemToSelectedCollection(dataItem.ItemName);
        }
        private void RemoveItemName(string dataItemName)
        {
            if (_addedItemsDictionary.ContainsKey(dataItemName))
            {
                _addedItemsDictionary[dataItemName] = _addedItemsDictionary[dataItemName] - 1;
            }
        }

        #endregion

    }
}
