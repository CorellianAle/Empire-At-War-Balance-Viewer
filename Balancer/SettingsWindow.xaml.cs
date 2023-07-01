using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

using Microsoft.WindowsAPICodePack.Dialogs;

namespace Balancer
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsViewModel viewModel;

        public SettingsWindow(ViewModel parentViewModel)
        {
            InitializeComponent();
            this.viewModel = new SettingsViewModel();
            this.DataContext = this.viewModel;
        }

        /// <summary>
        /// On "Apply" button push.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_ApplySettings(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_SelectEditorOne(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Executable file (*.exe)|*.exe|All files (*.*)|*.*";

            //select initial
            if (!string.IsNullOrWhiteSpace(viewModel.EditorOnePath))
            {
                var directory = Path.GetDirectoryName(viewModel.EditorOnePath);
                var filename = Path.GetFileName(viewModel.EditorOnePath);

                if (!string.IsNullOrWhiteSpace(directory))
                {
                    openFileDialog.InitialDirectory = directory;
                }

                if (!string.IsNullOrWhiteSpace(directory))
                {
                    openFileDialog.FileName = filename;
                }
            }

            //show dialog
            var result = openFileDialog.ShowDialog();

            if (!result.HasValue || !result.Value)
            {
                return;
            }

            viewModel.EditorOnePath = openFileDialog.FileName;
        }

        private void Button_Click_SelectEditorTwo(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Executable file (*.exe)|*.exe|All files (*.*)|*.*";

            //select initial
            if (!string.IsNullOrWhiteSpace(viewModel.EditorTwoPath))
            {
                var directory = Path.GetDirectoryName(viewModel.EditorTwoPath);
                var filename = Path.GetFileName(viewModel.EditorTwoPath);

                if (!string.IsNullOrWhiteSpace(directory))
                {
                    openFileDialog.InitialDirectory = directory;
                }

                if (!string.IsNullOrWhiteSpace(directory))
                {
                    openFileDialog.FileName = filename;
                }
            }

            //show dialog
            var result = openFileDialog.ShowDialog();

            if (!result.HasValue || !result.Value)
            {
                return;
            }

            viewModel.EditorTwoPath = openFileDialog.FileName;
        }

        private void Button_Click_SelectGameRootFolder(object sender, RoutedEventArgs e)
        {
            var openFolderDialog = new CommonOpenFileDialog();
            openFolderDialog.IsFolderPicker = true;


            //show dialog
            var result = openFolderDialog.ShowDialog();
            CommonFileDialogResult x;

            if (!CommonFileDialogResult.Ok.Equals(result))
            {
                return;
            }

            viewModel.GameRootFolder = openFolderDialog.FileName;
        }

        private void Button_Click_Apply(object sender, RoutedEventArgs e)
        {
            viewModel.Apply();
            this.Close();
        }
    }
}
