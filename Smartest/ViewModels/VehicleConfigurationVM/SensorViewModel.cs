using Smartest.Infrastructure.Objects;
using Smartest.Infrastructure.Interfaces;
using Smartest.ViewModels.BaseViewModels;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.IO;
using Smartest.Utilities;
using System.Collections.Generic;

namespace Smartest.ViewModels.VehicleConfigurationVM
{
    public class SensorViewModel : ItemsSourceBaseViewModel 
    {
        private readonly IConfigurationDataService _dataService;
        private readonly IGlobalConfigService _globalSettings;
        private readonly string _configurationName;
        // Also need to check if named changed
        private readonly Dictionary<string, int> _addedItemsDictionary = new Dictionary<string, int>();

        public ICommand AddSelectedDataItem { get; }

        private int _switchView = 0;
        public int SwitchView
        {
            get => _switchView;
            set
            {
                _switchView = value;
                RaisePropertyChanged();
            }
        }  

        public SensorViewModel(IConfigurationDataService dataService, IGlobalConfigService globalSettings) :base(dataService,"Sensors", globalSettings)
        {
            _dataService = dataService;
            _globalSettings = globalSettings;
            _configurationName = "Sensors";
            AddSelectedDataItem = new RelayCommand<ConfigurationDataItem>(OnAddItemCommandClicked);

        } 
        private void CopyConfigurationAndAddToList(ConfigurationDataItem dataitem)
        {
            var basePath = _globalSettings.Get("BasePath").ToString();
            var sourcePath = Path.Combine(basePath, _configurationName, dataitem.ItemName);
            var calculateditemName = CalculateditemName(dataitem);
            string destinationPath = "";
            if (FoldersHelper.CheckIfConfigFileExists(sourcePath))
            {
                destinationPath = Path.Combine(basePath, "Projects",ProjectsData.CurrentProjectName, "Configurations");

                FoldersHelper.CopyFileToLocation(Path.Combine(sourcePath, dataitem.ItemName + ".conf"), destinationPath,calculateditemName + ".conf");
            }

            AddItemToSelectedCollection(calculateditemName, destinationPath);
        }

        private string CalculateditemName(ConfigurationDataItem dataitem)
        {
            string calculateditemName;
            if (_addedItemsDictionary.ContainsKey(dataitem.ItemName))
            {
                int numberOfShows = _addedItemsDictionary[dataitem.ItemName] + 1;
                calculateditemName = dataitem.ItemName + numberOfShows;
                _addedItemsDictionary[dataitem.ItemName] = numberOfShows;
            }
            else
            {
                _addedItemsDictionary.Add(dataitem.ItemName, 0);
                calculateditemName = dataitem.ItemName;
            }

            return calculateditemName;
        }

        private void OnAddItemCommandClicked(ConfigurationDataItem dataitem)
        {
             CopyConfigurationAndAddToList(dataitem);
             
            //TODO 
            //Send Message to Unity - To spone item in to Scene
            SendUnityCommand();
          //  SwitchView = 1;
        }
    }
}
