using Smartest.Infrastructure.Models;
using Smartest.Infrastructure.Interfaces;
using Smartest.Models;
using Smartest.ViewModels.BaseViewModels;
using System.Collections.ObjectModel;

namespace Smartest.ViewModels.VehicleConfigurationVM
{
    public class SensorViewModel : ConfigurationItemsBaseViewModel 
    {
        public SensorViewModel(IConfigurationDataService dataService, IGlobalConfigService globalSettings) :base(dataService,"Sensors", globalSettings)
        { 
        }
    }
}
