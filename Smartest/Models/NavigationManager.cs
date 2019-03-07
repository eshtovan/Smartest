using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Smartest.Infrastructure.Interfaces;
using Smartest.Infrastructure.Objects;
using Smartest.Utilities;
using Smartest.ViewModels;

namespace Smartest.Models
{
    public class NavigationManager :INavigation
    {
        public void GoBack()
        {
            if(ProjectsData.LastPage!=null)
                ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentPage = ProjectsData.LastPage;//((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.SensorVm;
        }

        public void GoToPage(Enums.Pages page)
        {
            SetLastPage();
            switch (page)
            {
                case Enums.Pages.ConfigurationVm:
                    ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentPage = ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.ConfigurationVm;
                    break;

                case Enums.Pages.SensorVm:
                    ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentPage = ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.SensorVm;
                    break;
            }
        }

        private void SetLastPage()
        {
            ProjectsData.LastPage = ((ViewModelLocator) Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentPage;
        }
    }
}
