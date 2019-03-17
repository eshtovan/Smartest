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
using Smartest.ViewModels.VehicleConfigurationVM;

namespace Smartest.Models
{
    public class NavigationManager :INavigation
    {
        public void GoBack(string fromPage)
        {
            if (ProjectsData.LastPage != null)
            {
                //ProjectsData.LastPage
                switch (fromPage)
                {
                    case "Sensors":
                        ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm
                            .CurrentSensorsPage =
                            ProjectsData
                                .LastPage; //((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.SensorVm;
                        break;

                    case "Controllers":
                        ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm
                            .CurrentControllersPage =
                            ProjectsData
                                .LastPage; //((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.SensorVm;
                        break;
                } 
            }


        }

        public void GoBack()
        {
            if (ProjectsData.LastPage != null)
            {
                if (ProjectsData.LastPage is SensorViewModel)
                {
                    ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm
                        .CurrentSensorsPage =
                        ProjectsData
                            .LastPage;
                }
                if (ProjectsData.LastPage is ControllerViewModel)
                {
                    ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm
                        .CurrentControllersPage =
                        ProjectsData
                            .LastPage;
                } 
            }
        }

        public void GoToPage(string fromPage, Enums.Pages page)
        {
            SetLastPage(fromPage);

            switch (fromPage)
            {
                case "Sensors":
                    switch (page)
                    {
                        case Enums.Pages.ConfigurationVm:
                            ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentSensorsPage = ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.ConfigurationVm;
                            break;

                        case Enums.Pages.ControllerVm:
                            ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentSensorsPage = ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.ControllerVm;
                            break;

                        case Enums.Pages.SensorVm:
                            ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentSensorsPage = ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.SensorVm;
                            break;
                    }
                    break;

                case "Controllers":
                    switch (page)
                    {
                        case Enums.Pages.ConfigurationVm:
                            ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentControllersPage = ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.ConfigurationVm;
                            break;

                        case Enums.Pages.ControllerVm:
                            ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentControllersPage = ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.ControllerVm;
                            break;

                        case Enums.Pages.SensorVm:
                            ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentControllersPage = ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.SensorVm;
                            break;
                    }
                    break;
            }

        }

        private void SetLastPage(string fromPage)
        {
            switch (fromPage)
            {
                case "Sensors":
                    ProjectsData.LastPage = ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentSensorsPage;

                    break;

                case "Controllers":
                    ProjectsData.LastPage = ((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentControllersPage;

                    break;
            }


        }
    }
}
