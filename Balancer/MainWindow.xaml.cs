using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Balancer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel;



        public MainWindow()
        {
            InitializeComponent();

            viewModel = new ViewModel();
            viewModel.OnChartRefresh = UpdateBarChart;

            this.DataContext = viewModel;
        }



        private void MenuItem_Click_SelectFiles(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";

            //select initial
            var lastSelectedFiles = viewModel.LastSelectedFiles;

            if (lastSelectedFiles != null && lastSelectedFiles.Count > 0)
            {
                var lastSelectedFile = lastSelectedFiles[0];
                var directory = Path.GetDirectoryName(lastSelectedFile);
                openFileDialog.InitialDirectory = directory;
                var filename = Path.GetFileName(lastSelectedFile);
                openFileDialog.FileName = filename;
            }

            //show dialog
            var result = openFileDialog.ShowDialog();

            if (!result.HasValue || !result.Value)
            {
                return;
            }

            //create list
            var files = new List<string>();

            foreach (var file in openFileDialog.FileNames)
            {
                files.Add(file);
            }

            //trigger update
            viewModel.UpdateFiles(files);
        }



        private void MenuItem_Click_RefreshFiles(object sender, RoutedEventArgs e)
        {
            //trigger refresh
            viewModel.RefreshFiles();
        }



        private void MenuItem_Click_Close(object sender, RoutedEventArgs e)
        {
            viewModel.Close();
            this.Close();
        }



        private void MenuItem_Click_ToggleFilesListViewDisplay(object sender, RoutedEventArgs e)
        {
            viewModel.ToggleFilesDisplay();
        }



        private void MenuItem_Click_ShowOptions(object sender, RoutedEventArgs e)
        {

        }



        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Copyright © 2022 Alexey Novikov. All Right Reserved.", "About");
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.AskRestoreFiles();
        }



        private void DataGrid_MenuItem_Click_OpenFileEditorOne(object sender, RoutedEventArgs e)
        {
            viewModel.OpenSelectedUnitFileEditorOne();
        }

        private void DataGrid_MenuItem_Click_OpenFileEditorTwo(object sender, RoutedEventArgs e)
        {
            viewModel.OpenSelectedUnitFileEditorTwo();
        }



        private void DataGrid_MenuItem_Click_OpenFileInExplorer(object sender, RoutedEventArgs e)
        {
            viewModel.OpenSelectedUnitFileInExplorer();
        }



        private void UpdateBarChart()
        {
            if (viewModel.ChartItems == null || viewModel.ChartItems.Count == 0)
            {
                return;
            }

            ChartGrid.Children.Clear();
            ChartGrid.RowDefinitions.Clear();

            var maxChartItem = viewModel.ChartItems.MaxBy(x => x.Value);

            int i = 0;

            foreach (var chartItem in viewModel.ChartItems)
            {
                //add row
                var row = new RowDefinition();
                row.MinHeight = 30;
                ChartGrid.RowDefinitions.Add(row);

                //add name label
                var labelName = new Label();
                labelName.Content = chartItem.Name;
                ChartGrid.Children.Add(labelName);
                Grid.SetRow(labelName, i);
                Grid.SetColumn(labelName, 0);

                //calculate rectangle length in percentages
                var value = (int)Math.Ceiling(chartItem.Value * 100d / maxChartItem!.Value);

                Trace.WriteLine(value);

                if (value != 0)
                {
                    var border = new Border();
                    border.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    border.Height = 25;
                    border.Margin = new Thickness(0, 0, 0, 5);

                    ChartGrid.Children.Add(border);
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, 1);

                    if (value > 0)
                    {
                        Grid.SetColumnSpan(border, value);
                    }
                }

                //add value + percentage
                var labelValue = new Label();
                labelValue.Content = $"{chartItem.Value} ({value}%)";

                ChartGrid.Children.Add(labelValue);
                Grid.SetRow(labelValue, i);
                Grid.SetColumn(labelValue, value + 1);
                Grid.SetColumnSpan(labelValue, 102 - value - 1);

                ++i;
            }
        }

        private void Button_Click_RefreshChart(object sender, RoutedEventArgs e)
        {
            UpdateBarChart();
        }
    }
}
