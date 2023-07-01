using Balancer.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using System.Windows.Shapes;

namespace Balancer.UserControls
{
    /// <summary>
    /// Interaction logic for BarChart.xaml
    /// </summary>
    public partial class BarChart : UserControl
    {
        #region Items Source

        public ICollection<ChartItem> ItemsSource
        {
            get { return (ICollection<ChartItem>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ICollection<ChartItem>), typeof(BarChart), new PropertyMetadata(new PropertyChangedCallback(OnItemsSourcePropertyChanged)));

        private static void OnItemsSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as BarChart;

            if (control != null)
            {
                control.OnItemsSourceChanged((ICollection<ChartItem>)e.OldValue, (ICollection<ChartItem>)e.NewValue);
            }
        }

        private void OnItemsSourceChanged(ICollection<ChartItem> oldValue, ICollection<ChartItem> newValue)
        {
            // Remove handler for oldValue.CollectionChanged
            var oldValueINotifyCollectionChanged = oldValue as INotifyCollectionChanged;

            if (null != oldValueINotifyCollectionChanged)
            {
                oldValueINotifyCollectionChanged.CollectionChanged -= new NotifyCollectionChangedEventHandler(newValueINotifyCollectionChanged_CollectionChanged);
            }

            // Add handler for newValue.CollectionChanged (if possible)
            var newValueINotifyCollectionChanged = newValue as INotifyCollectionChanged;
            if (null != newValueINotifyCollectionChanged)
            {
                newValueINotifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(newValueINotifyCollectionChanged_CollectionChanged);
            }

            UpdateBarChart();
        }

        void newValueINotifyCollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateBarChart();
        }

        #endregion



        private void UpdateBarChart()
        {
            if (ItemsSource == null)
            {
                return;
            }

            var chartItems = (ICollection<ChartItem>)GetValue(ItemsSourceProperty);

            ChartGrid.Children.Clear();
            ChartGrid.RowDefinitions.Clear();

            var maxChartItem = chartItems.MaxBy(x => x.Value);

            int i = 0;

            foreach (var chartItem in chartItems)
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

                //context menu for label
                labelName.ContextMenu = CreateChartLabelContextMenu(chartItem);

                //chart bars

                //calculate rectangle length in percentages
                var value = (int)Math.Ceiling(chartItem.Value * 100d / maxChartItem!.Value);

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


        private ContextMenu CreateChartLabelContextMenu(ChartItem chartItem)
        {
            var contextMenu = new ContextMenu();

            /*
            var menuItemOpenFileEditorOne = new MenuItem();
            menuItemOpenFileEditorOne.Header = "Open in Editor 1";
            menuItemOpenFileEditorOne.Tag = chartItem.Name;
            menuItemOpenFileEditorOne.Click += Chart_MenuItem_Click_OpenFileInEditorOne;
            contextMenu.Items.Add(menuItemOpenFileEditorOne);

            var menuItemOpenFileEditorTwo = new MenuItem();
            menuItemOpenFileEditorTwo.Header = "Open in Editor 2";
            menuItemOpenFileEditorTwo.Tag = chartItem.Name;
            menuItemOpenFileEditorTwo.Click += Chart_MenuItem_Click_OpenFileInEditorTwo;
            contextMenu.Items.Add(menuItemOpenFileEditorTwo);

            contextMenu.Items.Add(new Separator());

            var menuItemOpenFileInExplorer = new MenuItem();
            menuItemOpenFileInExplorer.Header = "Open in Explorer";
            menuItemOpenFileInExplorer.Tag = chartItem.Name;
            menuItemOpenFileInExplorer.Click += Chart_MenuItem_Click_OpenFileInExplorer;
            contextMenu.Items.Add(menuItemOpenFileInExplorer);

            contextMenu.Items.Add(new Separator());
            */

            var menuItemDelete = new MenuItem();
            menuItemDelete.Header = "Delete from Current Chart";
            menuItemDelete.Tag = chartItem.Name;
            menuItemDelete.Click += Chart_MenuItem_Click_Delete;
            contextMenu.Items.Add(menuItemDelete);

            return contextMenu;
        }

        /*
        private void Chart_MenuItem_Click_OpenFileInEditorOne(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var unitName = (string)menuItem.Tag;

            viewModel.SelectUnitByName(unitName);
            viewModel.OpenSelectedUnitFileEditorOne();
        }

        private void Chart_MenuItem_Click_OpenFileInEditorTwo(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var unitName = (string)menuItem.Tag;

            viewModel.SelectUnitByName(unitName);
            viewModel.OpenSelectedUnitFileEditorTwo();
        }

        private void Chart_MenuItem_Click_OpenFileInExplorer(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var unitName = (string)menuItem.Tag;

            viewModel.SelectUnitByName(unitName);
            viewModel.OpenSelectedUnitFileInExplorer();
        }
        */

        private void Chart_MenuItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var name = (string)menuItem.Tag;

            var chartItem = ItemsSource.FirstOrDefault(ci => ci.Name == name);

            if (chartItem == null)
            {
                return;
            }

            ItemsSource.Remove(chartItem);
        }


        public BarChart()
        {
            InitializeComponent();
        }
    }
}
