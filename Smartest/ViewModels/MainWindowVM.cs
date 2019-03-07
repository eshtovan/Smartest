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


        private object _currentPage;

        public object CurrentPage
        {
            get
            {
                if (_currentPage == null)
                    _currentPage = ServiceLocator.Current.GetInstance<SensorViewModel>();

                return _currentPage;
            }
            set
            {
                _currentPage = value;
                RaisePropertyChanged();
            }
        }

        public StandsViewModel StandsVm => ServiceLocator.Current.GetInstance<StandsViewModel>();


        public SensorViewModel SensorVm => ServiceLocator.Current.GetInstance<SensorViewModel>();

        public ControllerViewModel ControllerVm => ServiceLocator.Current.GetInstance<ControllerViewModel>();

        public ConfigurationViewModel ConfigurationVm => ServiceLocator.Current.GetInstance<ConfigurationViewModel>();
    }
}
