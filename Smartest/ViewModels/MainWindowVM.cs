using System;
using Smartest.Infrastructure.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Input;
using GalaSoft.MvvmLight; 
using CommonServiceLocator;
using GalaSoft.MvvmLight.Command;
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

        public IntPtr MainWindowHandle { get; set; }
        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public ICommand SaveButton { get; }

        //internal ITabViewModel SelectedTabViewModel;
        public MainWindowVm()
        {
            ProjectsData.CurrentProjectName = "Hummer";

            ProjectsData.CurrentConfigurationName = "Hummer First Test";

            SaveButton = new RelayCommand(SaveConfigurationCommand);

            var path = @"C:\Program Files\Notepad++\notepad++.exe";
           // LoadUnityProcess(path);

            //TabViewModels = new ObservableCollection<ITabViewModel>();
            //TabViewModels.Add(new SensorViewModel { Header = "Tab B" });
            ////TabViewModels.Add(new ViewModelB { Header = "Tab B" });
            ////TabViewModels.Add(new ViewModelC { Header = "Tab C" });

            //SelectedTabViewModel = TabViewModels[0];
        }

        private object LoadUnityProcess//string path
        {
            get
            {
                //https://answers.unity.com/questions/245242/is-it-possible-to-use-unity-inside-wpf.html
                //https://social.msdn.microsoft.com/Forums/vstudio/en-US/a08de79c-f16a-4d10-ab7f-5d0a99a91ff5/external-exe-inside-wpf-window?forum=wpf


                //  WindowsFormsHost host = new WindowsFormsHost();
                //  System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
                //   host.Child = p;
                //   nPage.Header = "nouveau";
                //   nPage.Content = host;
                //   TabPage.Items.Add(nPage);
                Process proc = Process.Start(
                    new ProcessStartInfo()
                    {
                        FileName = @"C:\Program Files\Notepad++\notepad++.exe",

                        WindowStyle = ProcessWindowStyle.Normal
                    });
                Thread.Sleep(1000);
                SetParent(proc.MainWindowHandle, this.MainWindowHandle);

                return proc;

            }
        }

        private void SaveConfigurationCommand()
        {
             
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

        // https://stackoverflow.com/questions/19000721/running-an-exe-file-in-a-new-itemtab-in-wpf-application
        // 
        
    }
}
