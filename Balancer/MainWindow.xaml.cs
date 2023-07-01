using Balancer.Model;
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

            this.DataContext = viewModel;
        }





        #region menu bar

        private void MenuItem_Click_OpenFiles(object sender, RoutedEventArgs e)
        {
            var projectileFiles = OpenXmlFiles("Select projectile XML files", viewModel.LastSelectedProjectileFiles);
            var hardpointFiles = OpenXmlFiles("Select hardpoint XML files", viewModel.LastSelectedHardpointFiles);
            var unitFiles = OpenXmlFiles("Select unit XML files", viewModel.LastSelectedUnitFiles);

            //trigger update
            viewModel.UpdateFiles(projectileFiles, hardpointFiles, unitFiles);
        }

        private List<string> OpenXmlFiles(string title, List<string> lastSelectedFiles)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = title;
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";

            //select initial
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
                return null;
            }

            //create list
            var files = new List<string>();

            foreach (var file in openFileDialog.FileNames)
            {
                files.Add(file);
            }

            return files;
        }



        private void MenuItem_Click_RefreshFiles(object sender, RoutedEventArgs e)
        {
            //trigger refresh
            viewModel.RefreshData();
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



        private void MenuItem_Click_ShowSettings(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(viewModel);
            settingsWindow.Show();
        }



        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Copyright © 2022 Alexey Novikov. All Right Reserved.", "About");
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.AskRestoreFiles();
        }

#endregion





        #region xml files tables buttons 

        private void Button_Click_AddUnitXmlFile(object sender, RoutedEventArgs e)
        {
            var files = OpenXmlFiles("Add units XML files", viewModel.LastSelectedUnitFiles);

            viewModel.AddUnitsXmlFiles(files);
        }



        private void Button_Click_RemoveUnitXmlFile(object sender, RoutedEventArgs e)
        {
            //check
            if (UnitsXmlFiles.SelectedItems == null || UnitsXmlFiles.SelectedItems.Count == 0)
            {
                MessageBox.Show("No units XML files to remove.", "Information");
                return;
            }

            //cast
            var selectedXmlFiles = new List<UnitFile>();

            foreach (var x in UnitsXmlFiles.SelectedItems)
            {
                selectedXmlFiles.Add((UnitFile)x);
            }

            viewModel.RemoveUnitsXmlFiles(selectedXmlFiles);
        }



        private void Button_Click_AddHardpointXmlFile(object sender, RoutedEventArgs e)
        {
            var files = OpenXmlFiles("Add hardpoints XML files", viewModel.LastSelectedHardpointFiles);

            viewModel.AddHardpointsXmlFiles(files);
        }



        private void Button_Click_RemoveHardpointXmlFile(object sender, RoutedEventArgs e)
        {
            //check
            if (HardpointsXmlFiles.SelectedItems == null || HardpointsXmlFiles.SelectedItems.Count == 0)
            {
                MessageBox.Show("No hardpoints XML files to remove.", "Information");
                return;
            }

            //cast
            var selectedXmlFiles = new List<HardpointFile>();

            foreach (var x in HardpointsXmlFiles.SelectedItems)
            {
                selectedXmlFiles.Add((HardpointFile)x);
            }

            viewModel.RemoveHardpointsXmlFiles(selectedXmlFiles);
        }



        private void Button_Click_AddProjectileXmlFile(object sender, RoutedEventArgs e)
        {
            var files = OpenXmlFiles("Add projectiles XML files", viewModel.LastSelectedProjectileFiles);

            viewModel.AddProjectilesXmlFiles(files);
        }



        private void Button_Click_RemoveProjectileXmlFile(object sender, RoutedEventArgs e)
        {
            //check
            if (ProjectilesXmlFiles.SelectedItems == null || ProjectilesXmlFiles.SelectedItems.Count == 0)
            {
                MessageBox.Show("No projectiles XML files to remove.", "Information");
                return;
            }

            //cast
            var selectedXmlFiles = new List<ProjectileFile>();

            foreach (var x in ProjectilesXmlFiles.SelectedItems)
            {
                selectedXmlFiles.Add((ProjectileFile)x);
            }

            viewModel.RemoveProjectilesXmlFiles(selectedXmlFiles);
        }



        #endregion





        #region units tab

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

        

        private void DataGrid_MenuItem_Click_RemoveUnit(object sender, RoutedEventArgs e)
        {
            viewModel.RemoveSelectedUnit();
        }



        private void Button_Click_RefreshChart(object sender, RoutedEventArgs e)
        {
            viewModel.RefreshUnitsChartItems();
        }



        private void Button_Click_CompareSelectedUnits(object sender, RoutedEventArgs e)
        {
            //check
            if (AllUnitsDataGrid.SelectedItems == null || AllUnitsDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("No units to compare.", "Information");
                return;
            }

            //cast
            var selectedUnits = new List<Unit>();

            foreach (var x in AllUnitsDataGrid.SelectedItems)
            {
                selectedUnits.Add((Unit)x);
            }

            //invoke
            viewModel.SelectUnitsForComparison(selectedUnits);

            //Select "TabItem_CompareUnits"
            Dispatcher.BeginInvoke((Action)(() => TabControl.SelectedIndex = 4)); //5th tab (0-4)
        }



        private void Button_Click_ResetSelectedUnits(object sender, RoutedEventArgs e)
        {
            AllUnitsDataGrid.SelectedItems.Clear();
        }



        private void Button_Click_RemoveUnits(object sender, RoutedEventArgs e)
        {
            //check
            if (AllUnitsDataGrid.SelectedItems == null || AllUnitsDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("No units to compare.", "Information");
                return;
            }

            //cast
            var selectedUnits = new List<Unit>();

            foreach (var x in AllUnitsDataGrid.SelectedItems)
            {
                selectedUnits.Add((Unit)x);
            }

            viewModel.RemoveSelectedUnits(selectedUnits);
        }

        #endregion





        #region hardpoints tab

        private void DataGrid_MenuItem_Click_OpenHardpointFileEditorOne(object sender, RoutedEventArgs e)
        {
            viewModel.OpenSelectedHardpointFileEditorOne();
        }



        private void DataGrid_MenuItem_Click_OpenHardpointFileEditorTwo(object sender, RoutedEventArgs e)
        {
            viewModel.OpenSelectedHardpointFileEditorTwo();
        }



        private void DataGrid_MenuItem_Click_OpenHardpointFileInExplorer(object sender, RoutedEventArgs e)
        {
            viewModel.OpenSelectedHardpointFileInExplorer();
        }



        private void DataGrid_MenuItem_Click_RemoveHardpoint(object sender, RoutedEventArgs e)
        {
            viewModel.RemoveSelectedHardpoint();
        }



        private void Button_Click_CompareSelectedHardpoints(object sender, RoutedEventArgs e)
        {
            //check
            if (AllHardpointsDataGrid.SelectedItems == null || AllHardpointsDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("No hardpoint to compare.", "Information");
                return;
            }

            //cast
            var selectedHardpoints = new List<Hardpoint>();

            foreach (var x in AllHardpointsDataGrid.SelectedItems)
            {
                selectedHardpoints.Add((Hardpoint)x);
            }

            //invoke
            viewModel.SelectHardpointsForComparison(selectedHardpoints);

            //Select "TabItem_CompareUnits"
            Dispatcher.BeginInvoke((Action)(() => TabControl.SelectedIndex = 2));
        }



        private void Button_Click_ResetSelectedHardpoints(object sender, RoutedEventArgs e)
        {
            AllHardpointsDataGrid.SelectedItems.Clear();
        }



        private void Button_Click_RemoveHardpoints(object sender, RoutedEventArgs e)
        {
            //check
            if (AllHardpointsDataGrid.SelectedItems == null || AllHardpointsDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("No hardpoints to compare.", "Information");
                return;
            }

            //cast
            var selectedHardpoints = new List<Hardpoint>();

            foreach (var x in AllHardpointsDataGrid.SelectedItems)
            {
                selectedHardpoints.Add((Hardpoint)x);
            }

            viewModel.RemoveSelectedHardpoints(selectedHardpoints);
        }

        #endregion





        #region projectiles tab

        private void DataGrid_MenuItem_Click_OpenProjectileFileEditorOne(object sender, RoutedEventArgs e)
        {
            viewModel.OpenSelectedProjectileFileEditorOne();
        }



        private void DataGrid_MenuItem_Click_OpenProjectileFileEditorTwo(object sender, RoutedEventArgs e)
        {
            viewModel.OpenSelectedProjectileFileEditorTwo();
        }



        private void DataGrid_MenuItem_Click_OpenProjectileFileInExplorer(object sender, RoutedEventArgs e)
        {
            viewModel.OpenSelectedProjectileFileInExplorer();
        }



        private void DataGrid_MenuItem_Click_RemoveProjectile(object sender, RoutedEventArgs e)
        {
            viewModel.RemoveSelectedProjectile();
        }



        private void Button_Click_CompareSelectedProjectiles(object sender, RoutedEventArgs e)
        {
            //check
            if (AllProjectilesDataGrid.SelectedItems == null || AllProjectilesDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("No projectiles to compare.", "Information");
                return;
            }

            //cast
            var selectedProjectiles = new List<Projectile>();

            foreach (var x in AllProjectilesDataGrid.SelectedItems)
            {
                selectedProjectiles.Add((Projectile)x);
            }

            //invoke
            viewModel.SelectProjectilesForComparison(selectedProjectiles);

            //Select "TabItem_CompareUnits"
            Dispatcher.BeginInvoke((Action)(() => TabControl.SelectedIndex = 2));
        }



        private void Button_Click_ResetSelectedProjectiles(object sender, RoutedEventArgs e)
        {
            AllProjectilesDataGrid.SelectedItems.Clear();
        }



        private void Button_Click_RemoveProjectiles(object sender, RoutedEventArgs e)
        {
            //check
            if (AllProjectilesDataGrid.SelectedItems == null || AllProjectilesDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("No projectiles to compare.", "Information");
                return;
            }

            //cast
            var selectedProjectiles = new List<Projectile>();

            foreach (var x in AllProjectilesDataGrid.SelectedItems)
            {
                selectedProjectiles.Add((Projectile)x);
            }

            viewModel.RemoveSelectedProjectiles(selectedProjectiles);
        }

        #endregion





        #region compares units tab

        private void Button_Click_ResetComparedUnits(object sender, RoutedEventArgs e)
        {
            viewModel.ResetComparedUnits();
        }


        #endregion
    }
}
