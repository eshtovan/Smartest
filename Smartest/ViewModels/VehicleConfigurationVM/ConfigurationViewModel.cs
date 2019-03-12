using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Smartest.Infrastructure.Interfaces;
using Smartest.Infrastructure.Objects;
using Smartest.Utilities;

namespace Smartest.ViewModels.VehicleConfigurationVM
{
    public class ConfigurationViewModel : BaseViewModel
    {
     //   private readonly Grid _myGrid;
        private static INavigation _navigation;


        public ICommand GoBackToItemsList { get; }

        private ObservableCollection<ConfigurationItem> _mysampleGrid;

        public ObservableCollection<ConfigurationItem> MySampleGrid
        {
            get
            {
                if (_mysampleGrid == null)
                    _mysampleGrid = new ObservableCollection<ConfigurationItem>();

                return _mysampleGrid;
            }
            set
            {
                _mysampleGrid = value;
                RaisePropertyChanged();
            }
        }


        public string ItemName => ProjectsData.CurrentDataItem.ItemName;

        //[PreferredConstructor]
        public ConfigurationViewModel(INavigation navigation)//Grid myGrid)
        {

         
            _navigation = navigation;

            GoBackToItemsList = new RelayCommand(GoBackToItemsListCommand);
             
            //var rowDef = new RowDefinition { Height = new GridLength(25) };
            //// _myGrid.RowDefinitions.Add(rowDef);

            //var lblHeader = new Label { Content = "Configuration", FontWeight = FontWeights.Bold };

            //rowDef.DataContext = lblHeader;

            //var item = new ConfigurationItem("This is a test Name","123");
            //MySampleGrid.Add(item);

        }

        //public ConfigurationViewModel(Grid myGrid) // TODO get INavigation here // Move path to constructor
        //{
        //     _myGrid = myGrid;
        //     //_navigation = navigation;

        //     // check if need to show icon go back (for Controller window)
        //     GoBackToItemsList = new RelayCommand(GoBackToItemsListCommand);


        //     BuildPage(ProjectsData.CurrentDataItem.ConfigurationPath);
             

        //}


        public bool IsGoBackToItemsVisibly => ProjectsData.LastPage != null;


        private void GoBackToItemsListCommand()//PlacedDataItem item)
        {
            // TODO also get first configuration path in order to restore defaults
            // BuildPage(configInputPath);
            //TODO
            _navigation.GoBack();

            //((ViewModelLocator) Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.CurrentPage = ProjectsData.LastPage;//((ViewModelLocator)Application.Current.Resources["ViewModelLocator"]).VehicleConfigVm.SensorVm;
        }

        //TODO Move to Model
        //private void BuildPage(string configFilePath)
        //{
       
        //    var keysAndValues = LoadConfigFile(configFilePath);
        //    if (keysAndValues.Count > 0)
        //    {
        //        AddNewGridRow();
        //        var lblHeader = new Label { Content = "Configuration", FontWeight = FontWeights.Bold };

        //        _myGrid.Children.Add(lblHeader);
        //        Grid.SetRow(lblHeader, _myGrid.RowDefinitions.Count - 1);

        //        foreach (var keysAndValue in keysAndValues)
        //        {
        //            AddNewGridRow();

        //            var lbl = new Label { Content = keysAndValue.Key.Replace("_", " ") };

        //            _myGrid.Children.Add(lbl);

        //            Grid.SetRow(lbl, _myGrid.RowDefinitions.Count - 1);
        //            Grid.SetColumn(lbl, 0);//TestModel.MyGrid.ColumnDefinitions.Count - 2

        //            if (bool.TryParse(keysAndValue.Value, out bool res))
        //            {
        //                var chkBox = new CheckBox() { IsChecked = res };

        //                _myGrid.Children.Add(chkBox);

        //                Grid.SetRow(chkBox, _myGrid.RowDefinitions.Count - 1);
        //                Grid.SetColumn(chkBox, 1);//TestModel.MyGrid.ColumnDefinitions.Count

        //            }
        //            //else if (int.TryParse(keysAndValue.Value, out int intres))
        //            //{
        //            //    var lstBox = new ListBox();

        //            //    lstBox.Items.Add(keysAndValue.Value);
        //            //    lstBox.ScrollIntoView(keysAndValue.Value);


        //            //    _myGrid.Children.Add(lstBox);

        //            //    Grid.SetRow(lstBox, _myGrid.RowDefinitions.Count - 1);
        //            //    Grid.SetColumn(lstBox, 1);//TestModel.MyGrid.ColumnDefinitions.Count

        //            //}
        //            else
        //            {
        //                var txtBox = new TextBox { Text = keysAndValue.Value };

        //                _myGrid.Children.Add(txtBox);

        //                Grid.SetRow(txtBox, _myGrid.RowDefinitions.Count - 1);
        //                Grid.SetColumn(txtBox, 1);//TestModel.MyGrid.ColumnDefinitions.Count

        //            }

        //        }

        //        AddNewGridRow();

        //        var btn = new Button { Content = "Save" };
        //        btn.Click += Btn_Click;
        //        _myGrid.Children.Add(btn);
        //        Grid.SetRow(btn, _myGrid.RowDefinitions.Count - 1);


        //    }

        //}

        //private void AddNewGridRow()
        //{
        //    var rowDef = new RowDefinition { Height = new GridLength(25) };
        //    _myGrid.RowDefinitions.Add(rowDef);
        //}

        ////Save Button
        //private void Btn_Click(object sender, RoutedEventArgs e)
        //{

        //    //TODO Save Backup File to restore last state
        //    var sb = new StringBuilder();
        //    bool first = true;
        //    foreach (var myGridChild in _myGrid.Children)
        //    {
        //        if (first)
        //        {
        //            first = false;
        //            continue;
        //        }

        //        if (myGridChild.GetType() == typeof(Label))
        //        {
        //            //                  sb = new StringBuilder();
        //            string lblText = ((ContentControl)myGridChild).Content.ToString();
        //            sb.Append(lblText.Replace(" ", "_"));
        //        }
        //        else
        //        {
        //            if (myGridChild.GetType() == typeof(TextBox))
        //            {
        //                sb.Append("=");
        //                sb.Append(((TextBox)myGridChild).Text);

        //            }
        //            if (myGridChild.GetType() == typeof(CheckBox))
        //            {
        //                sb.Append("=");
        //                sb.Append(((CheckBox)myGridChild).IsChecked.ToString());
        //            }
        //            sb.AppendLine();
        //        }
        //        //  var name = ((System.RuntimeType) ((System.Windows.FrameworkElement) myGridChild).DefaultStyleKey).FullName;
        //    }

        //    //TODO Save from model
        //    File.WriteAllText(ProjectsData.CurrentDataItem.ConfigurationPath, sb.ToString());

        //}

        private Dictionary<string, string> LoadConfigFile(string configFilePath)
        {
            string[] lines = System.IO.File.ReadAllLines(configFilePath);
            var dictionary = new Dictionary<string, string>();
            foreach (var line in lines)
            {
                if (line.Trim().StartsWith("/") || !line.Trim().Contains("="))
                    continue;

                var keyVal = line.Split('=');

                dictionary.Add(keyVal[0].Trim(), keyVal[1].Trim());
            }
            return dictionary;
        }


    }
}
