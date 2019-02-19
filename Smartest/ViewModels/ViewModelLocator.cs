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

            SimpleIoc.Default.Register<IConfigurationDataService, ConfigurationDataService>();

            SimpleIoc.Default.Register<ISettings, ProjectSettings>();

            SimpleIoc.Default.Register<IGlobalConfigService, GlobalConfigService>();
            SimpleIoc.Default.Register<MainWindowVM>();
            SimpleIoc.Default.Register<SensorViewModel>();
            SimpleIoc.Default.Register<ConfigurationViewModel>();

            
        }

        internal static void Cleanup()
        {
             
        }

        public MainWindowVM Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowVM>();
            }
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
