using System.Linq;
using System.Windows;
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
        private string _nameBeforeEdit = "";
        private readonly IConfigurationDataService _dataService; 
        private readonly INavigation _navigationManager;
        private readonly string _configurationName;
        private bool _showEditMode = false;
        private bool _notShowEditMode = true;
        public ICommand AddSelectedDataItem { get; }

        public ICommand DeleteSelectedDataItem { get; }

        public ICommand EditSelectedDataItem { get; }

        public ICommand UndoEditSelectedDataItem { get; }

        public ICommand ItemDoubleClicked { get; }

        public bool ShowEditMode      
        {
            get => _showEditMode;
            set
            {
                _showEditMode = value;
                RaisePropertyChanged();
            } 
        }

        public bool NotShowEditMode
        {
            get { return _notShowEditMode; }
            set
            {
                _notShowEditMode = value;
                RaisePropertyChanged();
            }
        }

        public PlacedDataItem SelectedDataItem { get; set; }


        public SensorViewModel(IConfigurationDataService dataService,
            INavigation navigationManager) : base(dataService, "Sensors")
        {
            _dataService = dataService;
            _navigationManager = navigationManager;
            _configurationName = "Sensors";
            AddSelectedDataItem = new RelayCommand<ConfigurationDataItem>(OnAddItemCommandClicked);
            DeleteSelectedDataItem = new RelayCommand<PlacedDataItem>(OnDeleteItemCommandClicked);

            EditSelectedDataItem = new RelayCommand<PlacedDataItem>(OnEditItemCommandClicked);

            UndoEditSelectedDataItem = new RelayCommand<PlacedDataItem>(OnUndoEditItemCommandClicked);

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
           
            if (ShowEditMode)
            {
                ShowEditMode = false;
                NotShowEditMode = !ShowEditMode;
                //change configuration file name
                UpdateItemConfigurationFile(_nameBeforeEdit,dataItem);
                //TODO send to unity name changed
            }
            else
            {
                _nameBeforeEdit = dataItem.ItemName;
                ShowEditMode = true;
                NotShowEditMode = !ShowEditMode;
            }
     
        }
        //https://stackoverflow.com/questions/35119480/multiple-bindings-as-parameter-for-converter-wpf-mvvm


        private void OnUndoEditItemCommandClicked(PlacedDataItem dataItem)
        { 
            dataItem.ItemName = _nameBeforeEdit;
            ShowEditMode =false;
            NotShowEditMode = !ShowEditMode;
        }

    }
}
