using Smartest.Infrastructure.Interfaces;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight; 
using CommonServiceLocator;
using Smartest.ViewModels.VehicleConfigurationVM;
using Smartest.Utilities;

namespace Smartest.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        //private ObservableCollection<ITabViewModel> tabViewModels;
        //internal ObservableCollection<ITabViewModel> TabViewModels
        //{
        //    get { return tabViewModels; }
        //    set { tabViewModels = value; }
        //}

        //internal ITabViewModel SelectedTabViewModel;
        public MainWindowVM()
        {
            ProjectsData.CurrentProjectName = "Hummer";
            //TabViewModels = new ObservableCollection<ITabViewModel>();
            //TabViewModels.Add(new SensorViewModel { Header = "Tab B" });
            ////TabViewModels.Add(new ViewModelB { Header = "Tab B" });
            ////TabViewModels.Add(new ViewModelC { Header = "Tab C" });

            //SelectedTabViewModel = TabViewModels[0];
        }

        public SensorViewModel SensorVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SensorViewModel>();
            }
        }

        public ConfigurationViewModel ConfigurationVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ConfigurationViewModel>();
            }
        }

    }
}
