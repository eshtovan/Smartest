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
            SimpleIoc.Default.Register<ISettings, ProjectSettings>();
            SimpleIoc.Default.Register<IGlobalConfigService, GlobalConfigService>();

            // View Models
            SimpleIoc.Default.Register<SensorViewModel>();
            SimpleIoc.Default.Register<ConfigurationViewModel>();
            
            SimpleIoc.Default.Register<MainWindowVm>();
          
            
        }

        internal static void Cleanup()
        {
            
        }
         
        public MainWindowVm Main => ServiceLocator.Current.GetInstance<MainWindowVm>();

        public SensorViewModel SensorVm => ServiceLocator.Current.GetInstance<SensorViewModel>();


        public ConfigurationViewModel ConfigurationVm => ServiceLocator.Current.GetInstance<ConfigurationViewModel>();
    }
}
