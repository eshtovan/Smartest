using Smartest.Infrastructure.Interfaces;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight; 
using CommonServiceLocator;
using Smartest.ViewModels.VehicleConfigurationVM;
using Smartest.Utilities;

namespace Smartest.ViewModels
{
    public class MainWindowVm : ViewModelBase
    {
        //private ObservableCollection<ITabViewModel> tabViewModels;
        //internal ObservableCollection<ITabViewModel> TabViewModels
        //{
        //    get { return tabViewModels; }
        //    set { tabViewModels = value; }
        //}

        //internal ITabViewModel SelectedTabViewModel;
        public MainWindowVm()
        {
            ProjectsData.CurrentProjectName = "Hummer";

            ProjectsData.CurrentConfigurationName = "Hummer First Test";
            //TabViewModels = new ObservableCollection<ITabViewModel>();
            //TabViewModels.Add(new SensorViewModel { Header = "Tab B" });
            ////TabViewModels.Add(new ViewModelB { Header = "Tab B" });
            ////TabViewModels.Add(new ViewModelC { Header = "Tab C" });

            //SelectedTabViewModel = TabViewModels[0];
        }


        private object _currentSensorsPage;

        public object CurrentSensorsPage
        {
            get
            {
                if (_currentSensorsPage == null)
                    _currentSensorsPage = ServiceLocator.Current.GetInstance<SensorViewModel>();

                return _currentSensorsPage;
            }
            set
            {
                _currentSensorsPage = value;
                RaisePropertyChanged();
            }
        }

        public StandsViewModel StandsVm
        {
            get
            {
             //   ProjectsData.CurrentConfigurationView = "Stands";
                return ServiceLocator.Current.GetInstance<StandsViewModel>();
            }
        }
        
        public SensorViewModel SensorVm
        {
            get
            {
            //    ProjectsData.CurrentConfigurationView = "Sensors";
                return ServiceLocator.Current.GetInstance<SensorViewModel>();
            }
        }


        private object _currentControllersPage;

        public object CurrentControllersPage
        {
            get
            {
                if (_currentControllersPage == null)
                    _currentControllersPage = ServiceLocator.Current.GetInstance<ControllerViewModel>();

                return _currentControllersPage;
            }
            set
            {
                _currentControllersPage = value;
                RaisePropertyChanged();
            }
        }

        public ControllerViewModel ControllerVm
        {
            get
            {
              //  ProjectsData.CurrentConfigurationView = "Controllers";
                return ServiceLocator.Current.GetInstance<ControllerViewModel>();
            }
        }

        public ConfigurationViewModel ConfigurationVm 
        {
            get
            {
              //  ProjectsData.CurrentConfigurationView = "Configurations";
                return ServiceLocator.Current.GetInstance<ConfigurationViewModel>();
            }
        }
    
    }
}
