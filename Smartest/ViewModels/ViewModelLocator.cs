using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Smartest.Infrastructure;
using Smartest.Infrastructure.Interfaces;
using Smartest.Models;
using Smartest.ViewModels.VehicleConfigurationVM;

namespace Smartest.ViewModels
{
    class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Services
            SimpleIoc.Default.Register<IConfigurationDataService, ConfigurationDataService>();
            SimpleIoc.Default.Register<ISettings, ProjectSettings>(); // Not in direct use
            SimpleIoc.Default.Register<IGlobalConfigService, GlobalConfigService>();
            SimpleIoc.Default.Register<INavigation, NavigationManager>();


            // View Models
            SimpleIoc.Default.Register<ControllerViewModel>();
            SimpleIoc.Default.Register<StandsViewModel>();
            SimpleIoc.Default.Register<SensorViewModel>();
          
            //https://stackoverflow.com/questions/9342294/simpleioc-can-it-provide-new-instance-each-time-required
            //SimpleIoc.Default.Register<BaseViewModel>(() => new ConfigurationViewModel());
         
           // SimpleIoc.Default.Register<ConfigurationViewModel>();

            SimpleIoc.Default.Register(() => new ConfigurationViewModel(SimpleIoc.Default.GetInstance<INavigation>()),true);
             
            SimpleIoc.Default.Register<MainWindowVm>();
          
            
        }

        internal static void Cleanup()
        {
            
        }
         
        public MainWindowVm VehicleConfigVm => ServiceLocator.Current.GetInstance<MainWindowVm>();

        public SensorViewModel SensorVm => ServiceLocator.Current.GetInstance<SensorViewModel>();


        public ControllerViewModel ControllerVm => ServiceLocator.Current.GetInstance<ControllerViewModel>();


        public StandsViewModel StandsVm => ServiceLocator.Current.GetInstance<StandsViewModel>();

        public ConfigurationViewModel ConfigurationVm => ServiceLocator.Current.GetInstance<ConfigurationViewModel>();
    }
}
