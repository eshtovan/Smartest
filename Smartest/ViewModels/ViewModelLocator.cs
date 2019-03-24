using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc; 
using Smartest.Infrastructure;
using Smartest.Infrastructure.Interfaces;
using Smartest.Models;
using Smartest.ViewModels.VehicleConfigurationVM;
using Smartest.Views.VehicleConfiguration;

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
            SimpleIoc.Default.Register<IUnityCommunication, UnityCommunicationModel>();


            // View Models
            SimpleIoc.Default.Register<ControllerViewModel>(false);
            SimpleIoc.Default.Register<StandsViewModel>(false);
            SimpleIoc.Default.Register<SensorViewModel>(false);

            //https://stackoverflow.com/questions/9342294/simpleioc-can-it-provide-new-instance-each-time-required
            //SimpleIoc.Default.Register<BaseViewModel>(() => new ConfigurationViewModel());

             SimpleIoc.Default.Register<ConfigurationViewModel>(false);
            //Messenger.Default.Register<ConfigurationViewModel>(this, message =>
            //{
            //    var uniqueKey = System.Guid.NewGuid().ToString();
            //    var adventurerWindowVM = SimpleIoc.Default.GetInstance<ConfigurationViewModel>(uniqueKey);
            //    //adventurerWindowVM.Adv = message.Argument;
            //    var adventurerWindow = new ConfigurationView()
            //    {
            //        DataContext = adventurerWindowVM
            //    };
            //    adventurerWindow.Closed += (sender, args) => SimpleIoc.Default.Unregister(uniqueKey);
            //    adventurerWindow.Show();
            //});


            // SimpleIoc.Default.Register(() => new ConfigurationViewModel(SimpleIoc.Default.GetInstance<INavigation>()),true);

            SimpleIoc.Default.Register<MainWindowVm>();
          
            
        }

        internal static void Cleanup()
        {
            
        }
         
        public MainWindowVm VehicleConfigVm
        {

            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowVm>();
            }
        }
    

        public SensorViewModel SensorVm
        { 
            get
            {
                return ServiceLocator.Current.GetInstance<SensorViewModel>();
            }
        }
      


        public ControllerViewModel ControllerVm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ControllerViewModel>();
            }
        }
        


        public StandsViewModel StandsVm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StandsViewModel>();

            }
        }
      
        public ConfigurationViewModel ConfigurationVm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ConfigurationViewModel>();

            }
        }
       
    }
}
