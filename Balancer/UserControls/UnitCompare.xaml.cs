using Balancer.Model;
using System;
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
    /// Interaction logic for UnitCompare.xaml
    /// </summary>
    public partial class UnitCompare : UserControl
    {
        #region Items Source

        public IList<Unit> ItemsSource
        {
            get { return (IList<Unit>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList<Unit>), typeof(UnitCompare), new PropertyMetadata(new PropertyChangedCallback(OnItemsSourcePropertyChanged)));

        private static void OnItemsSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as UnitCompare;

            if (control != null)
            {
                control.OnItemsSourceChanged((IList<Unit>)e.OldValue, (IList<Unit>)e.NewValue);
            }
        }

        private void OnItemsSourceChanged(IList<Unit> oldValue, IList<Unit> newValue)
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

            Refresh();
        }

        void newValueINotifyCollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Refresh();
        }

        #endregion

        private List<ContentControl> generatedControls;

        private List<ColumnDefinition> generatedColumns;


        /*
        < Label Grid.Row = "0" Grid.Column = "0" />
        < Label Grid.Row = "1" Grid.Column = "0" Content = "Name:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "2" Grid.Column = "0" Content = "AI Combat Power:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "3" Grid.Column = "0" Content = "Damage:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "4" Grid.Column = "0" Content = "Autoresolve Health:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "5" Grid.Column = "0" Content = "Shield Points:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "6" Grid.Column = "0" Content = "Tactical Health:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "7" Grid.Column = "0" Content = "Shield Refresh Rate:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "8" Grid.Column = "0" Content = "Energy Capacity:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "9" Grid.Column = "0" Content = "Energy Refresh Rate:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "10" Grid.Column = "0" Content = "Build Cost Credits:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "11" Grid.Column = "0" Content = "Piracy Value Credits:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "12" Grid.Column = "0" Content = "Build Time Seconds:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "13" Grid.Column = "0" Content = "Space FOW Reveal Range:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "14" Grid.Column = "0" Content = "Targeting Max Attack Distance:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "15" Grid.Column = "0" Content = "Score Cost Credits:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "16" Grid.Column = "0" Content = "Tactical Build Cost Multiplayer:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        < Label Grid.Row = "17" Grid.Column = "0" Content = "Tactical Build Time Seconds:" FontWeight = "Bold" HorizontalAlignment = "Right" />
        */

        private Unit mainUnit;

        private readonly SolidColorBrush brushGreen = new SolidColorBrush(Color.FromRgb(28, 207, 48));
        private readonly SolidColorBrush brushRed = new SolidColorBrush(Color.FromRgb(207, 53, 53));


        public UnitCompare()
        {
            InitializeComponent();

            generatedControls = new List<ContentControl>();
            generatedColumns = new List<ColumnDefinition>();
        }

        private void Refresh()
        {

            foreach (var control in generatedControls)
            {
                MainGrid.Children.Remove(control);
            }

            foreach(var column in generatedColumns)
            {
                MainGrid.ColumnDefinitions.Remove(column);
            }

            //set main
            if (ItemsSource != null && ItemsSource.Count > 0)
            {
                mainUnit = ItemsSource[0];
            }

            //fill with new data
            int columnIndex = 1;
            int tempInt;
            double? percentage;
            

            foreach (Unit unit in ItemsSource)
            {
                int rowIndex = 0;

                //create column
                var columnDefinition = new ColumnDefinition();
                columnDefinition.MinWidth = 200;
                columnDefinition.MaxWidth = 400;
                MainGrid.ColumnDefinitions.Add(columnDefinition);
                generatedColumns.Add(columnDefinition);

                //
                var labelName = new Label();
                labelName.Content = unit.Name;

                MainGrid.Children.Add(labelName);
                Grid.SetColumn(labelName, columnIndex);
                Grid.SetRow(labelName, ++rowIndex);
                generatedControls.Add(labelName);

                //
                var labelAiCombatPower = new Label();

                FillLabel(labelAiCombatPower, unit.AI_Combat_Power, mainUnit.AI_Combat_Power);

                MainGrid.Children.Add(labelAiCombatPower);
                Grid.SetColumn(labelAiCombatPower, columnIndex);
                Grid.SetRow(labelAiCombatPower, ++rowIndex);
                generatedControls.Add(labelAiCombatPower);

                //
                var labelDamage = new Label();

                FillLabel(labelDamage, unit.Damage, mainUnit.Damage);
                
                MainGrid.Children.Add(labelDamage);
                Grid.SetColumn(labelDamage, columnIndex);
                Grid.SetRow(labelDamage, ++rowIndex);
                generatedControls.Add(labelDamage);

                //
                var labelAutoresolveHealth = new Label();

                FillLabel(labelAutoresolveHealth, unit.Autoresolve_Health, mainUnit.Autoresolve_Health);

                MainGrid.Children.Add(labelAutoresolveHealth);
                Grid.SetColumn(labelAutoresolveHealth, columnIndex);
                Grid.SetRow(labelAutoresolveHealth, ++rowIndex);
                generatedControls.Add(labelAutoresolveHealth);

                //
                var labelShieldPoints = new Label();

                FillLabel(labelShieldPoints, unit.Shield_Points, mainUnit.Shield_Points);

                MainGrid.Children.Add(labelShieldPoints);
                Grid.SetColumn(labelShieldPoints, columnIndex);
                Grid.SetRow(labelShieldPoints, ++rowIndex);
                generatedControls.Add(labelShieldPoints);

                //
                var labelTacticalHealth = new Label();

                FillLabel(labelTacticalHealth, unit.Tactical_Health, mainUnit.Tactical_Health);

                MainGrid.Children.Add(labelTacticalHealth);
                Grid.SetColumn(labelTacticalHealth, columnIndex);
                Grid.SetRow(labelTacticalHealth, ++rowIndex);
                generatedControls.Add(labelTacticalHealth);

                //
                var labelShieldRefreshRate = new Label();

                FillLabel(labelShieldRefreshRate, unit.Shield_Refresh_Rate, mainUnit.Shield_Refresh_Rate);

                MainGrid.Children.Add(labelShieldRefreshRate);
                Grid.SetColumn(labelShieldRefreshRate, columnIndex);
                Grid.SetRow(labelShieldRefreshRate, ++rowIndex);
                generatedControls.Add(labelShieldRefreshRate);

                //
                var labelEnergyCapacity = new Label();

                FillLabel(labelEnergyCapacity, unit.Energy_Capacity, mainUnit.Energy_Capacity);

                MainGrid.Children.Add(labelEnergyCapacity);
                Grid.SetColumn(labelEnergyCapacity, columnIndex);
                Grid.SetRow(labelEnergyCapacity, ++rowIndex);
                generatedControls.Add(labelEnergyCapacity);

                //
                var labelEnergyRefreshRate = new Label();

                FillLabel(labelEnergyRefreshRate, unit.Energy_Refresh_Rate, mainUnit.Energy_Refresh_Rate);

                MainGrid.Children.Add(labelEnergyRefreshRate);
                Grid.SetColumn(labelEnergyRefreshRate, columnIndex);
                Grid.SetRow(labelEnergyRefreshRate, ++rowIndex);
                generatedControls.Add(labelEnergyRefreshRate);

                //
                var labelBuildCostCredits = new Label();

                FillLabel(labelBuildCostCredits, unit.Build_Cost_Credits, mainUnit.Build_Cost_Credits);

                MainGrid.Children.Add(labelBuildCostCredits);
                Grid.SetColumn(labelBuildCostCredits, columnIndex);
                Grid.SetRow(labelBuildCostCredits, ++rowIndex);
                generatedControls.Add(labelBuildCostCredits);

                //
                var labelPiracyValueCredits = new Label();

                FillLabel(labelPiracyValueCredits, unit.Piracy_Value_Credits, mainUnit.Piracy_Value_Credits);

                MainGrid.Children.Add(labelPiracyValueCredits);
                Grid.SetColumn(labelPiracyValueCredits, columnIndex);
                Grid.SetRow(labelPiracyValueCredits, ++rowIndex);
                generatedControls.Add(labelPiracyValueCredits);

                //
                var labelBuildTimeSeconds = new Label();

                FillLabel(labelBuildTimeSeconds, unit.Build_Time_Seconds, mainUnit.Build_Time_Seconds);

                MainGrid.Children.Add(labelBuildTimeSeconds);
                Grid.SetColumn(labelBuildTimeSeconds, columnIndex);
                Grid.SetRow(labelBuildTimeSeconds, ++rowIndex);
                generatedControls.Add(labelBuildTimeSeconds);

                //
                var labelSpaceFowRevealRange = new Label();

                FillLabel(labelSpaceFowRevealRange, unit.Space_FOW_Reveal_Range, mainUnit.Space_FOW_Reveal_Range);

                MainGrid.Children.Add(labelSpaceFowRevealRange);
                Grid.SetColumn(labelSpaceFowRevealRange, columnIndex);
                Grid.SetRow(labelSpaceFowRevealRange, ++rowIndex);
                generatedControls.Add(labelSpaceFowRevealRange);

                //
                var labelTargetingMaxAttackDistance = new Label();

                FillLabel(labelTargetingMaxAttackDistance, unit.Targeting_Max_Attack_Distance, mainUnit.Targeting_Max_Attack_Distance);

                MainGrid.Children.Add(labelTargetingMaxAttackDistance);
                Grid.SetColumn(labelTargetingMaxAttackDistance, columnIndex);
                Grid.SetRow(labelTargetingMaxAttackDistance, ++rowIndex);
                generatedControls.Add(labelTargetingMaxAttackDistance);

                //
                var labelScoreCostCredits = new Label();

                FillLabel(labelScoreCostCredits, unit.Score_Cost_Credits, mainUnit.Score_Cost_Credits);

                MainGrid.Children.Add(labelScoreCostCredits);
                Grid.SetColumn(labelScoreCostCredits, columnIndex);
                Grid.SetRow(labelScoreCostCredits, ++rowIndex);
                generatedControls.Add(labelScoreCostCredits);

                //
                var labelTacticalBuildCostMultiplayer = new Label();

                FillLabel(labelTacticalBuildCostMultiplayer, unit.Tactical_Build_Cost_Multiplayer, mainUnit.Tactical_Build_Cost_Multiplayer);

                MainGrid.Children.Add(labelTacticalBuildCostMultiplayer);
                Grid.SetColumn(labelTacticalBuildCostMultiplayer, columnIndex);
                Grid.SetRow(labelTacticalBuildCostMultiplayer, ++rowIndex);
                generatedControls.Add(labelTacticalBuildCostMultiplayer);

                //
                var labelTacticalBuildTimeSeconds = new Label();

                FillLabel(labelTacticalBuildTimeSeconds, unit.Tactical_Build_Time_Seconds, mainUnit.Tactical_Build_Time_Seconds);

                MainGrid.Children.Add(labelTacticalBuildTimeSeconds);
                Grid.SetColumn(labelTacticalBuildTimeSeconds, columnIndex);
                Grid.SetRow(labelTacticalBuildTimeSeconds, ++rowIndex);
                generatedControls.Add(labelTacticalBuildTimeSeconds);

                //total health (via hardpoints)
                var totalHardpointHealth = new Label();

                var value = unit.CalculateTotalHardpointsHealth();

                FillLabel(totalHardpointHealth, value, mainUnit.CalculateTotalHardpointsHealth());

                MainGrid.Children.Add(totalHardpointHealth);
                Grid.SetColumn(totalHardpointHealth, columnIndex);
                Grid.SetRow(totalHardpointHealth, ++rowIndex);
                generatedControls.Add(totalHardpointHealth);

                //

                //

                //

                //hardpoints total laser damage on first attack
                var hardpointsInitialAttackDamageLaser = new Label();

                FillLabel(hardpointsInitialAttackDamageLaser, unit.CalculateInitialAttackDamageLaser(), mainUnit.CalculateInitialAttackDamageLaser());

                MainGrid.Children.Add(hardpointsInitialAttackDamageLaser);
                Grid.SetColumn(hardpointsInitialAttackDamageLaser, columnIndex);
                ++rowIndex;
                Grid.SetRow(hardpointsInitialAttackDamageLaser, ++rowIndex);
                generatedControls.Add(hardpointsInitialAttackDamageLaser);

                //hardpoints total missle damage on first attack
                var hardpointsInitialAttackDamageMissle = new Label();

                FillLabel(hardpointsInitialAttackDamageMissle, unit.CalculateInitialAttackDamageMissle(), mainUnit.CalculateInitialAttackDamageMissle());

                MainGrid.Children.Add(hardpointsInitialAttackDamageMissle);
                Grid.SetColumn(hardpointsInitialAttackDamageMissle, columnIndex);
                Grid.SetRow(hardpointsInitialAttackDamageMissle, ++rowIndex);
                generatedControls.Add(hardpointsInitialAttackDamageMissle);

                //hardpoints total torpedo damage on first attack
                var hardpointsInitialAttackDamageTorpedo = new Label();

                FillLabel(hardpointsInitialAttackDamageTorpedo, unit.CalculateInitialAttackDamageTorpedo(), mainUnit.CalculateInitialAttackDamageTorpedo());

                MainGrid.Children.Add(hardpointsInitialAttackDamageTorpedo);
                Grid.SetColumn(hardpointsInitialAttackDamageTorpedo, columnIndex);
                Grid.SetRow(hardpointsInitialAttackDamageTorpedo, ++rowIndex);
                generatedControls.Add(hardpointsInitialAttackDamageTorpedo);

                //hardpoints total ion damage on first attack
                var hardpointsInitialAttackDamageIon = new Label();

                FillLabel(hardpointsInitialAttackDamageIon, unit.CalculateInitialAttackDamageIon(), mainUnit.CalculateInitialAttackDamageIon());

                MainGrid.Children.Add(hardpointsInitialAttackDamageIon);
                Grid.SetColumn(hardpointsInitialAttackDamageIon, columnIndex);
                Grid.SetRow(hardpointsInitialAttackDamageIon, ++rowIndex);
                generatedControls.Add(hardpointsInitialAttackDamageIon);

                //

                //

                //

                //hardpoints total laser damage over 10 sec
                var hardpoints10SecAttackDamageLaser = new Label();

                FillLabel(hardpoints10SecAttackDamageLaser, unit.Calculate10SecAttackDamageLaser(), mainUnit.Calculate10SecAttackDamageLaser());

                MainGrid.Children.Add(hardpoints10SecAttackDamageLaser);
                Grid.SetColumn(hardpoints10SecAttackDamageLaser, columnIndex);
                ++rowIndex;
                Grid.SetRow(hardpoints10SecAttackDamageLaser, ++rowIndex);
                generatedControls.Add(hardpoints10SecAttackDamageLaser);

                //hardpoints total missle damage over 10 sec
                var hardpoints10SecAttackDamageMissle = new Label();

                FillLabel(hardpoints10SecAttackDamageMissle, unit.Calculate10SecAttackDamageMissle(), mainUnit.Calculate10SecAttackDamageMissle());

                MainGrid.Children.Add(hardpoints10SecAttackDamageMissle);
                Grid.SetColumn(hardpoints10SecAttackDamageMissle, columnIndex);
                Grid.SetRow(hardpoints10SecAttackDamageMissle, ++rowIndex);
                generatedControls.Add(hardpoints10SecAttackDamageMissle);

                //hardpoints total torpedo damage over 10 sec
                var hardpoints10SecAttackDamageTorpedo = new Label();

                FillLabel(hardpoints10SecAttackDamageTorpedo, unit.Calculate10SecAttackDamageTorpedo(), mainUnit.Calculate10SecAttackDamageTorpedo());

                MainGrid.Children.Add(hardpoints10SecAttackDamageTorpedo);
                Grid.SetColumn(hardpoints10SecAttackDamageTorpedo, columnIndex);
                Grid.SetRow(hardpoints10SecAttackDamageTorpedo, ++rowIndex);
                generatedControls.Add(hardpoints10SecAttackDamageTorpedo);

                //hardpoints total ion damage over 10 sec
                var hardpoints10SecAttackDamageIon = new Label();

                FillLabel(hardpoints10SecAttackDamageIon, unit.Calculate10SecAttackDamageIon(), mainUnit.Calculate10SecAttackDamageIon());

                MainGrid.Children.Add(hardpoints10SecAttackDamageIon);
                Grid.SetColumn(hardpoints10SecAttackDamageIon, columnIndex);
                Grid.SetRow(hardpoints10SecAttackDamageIon, ++rowIndex);
                generatedControls.Add(hardpoints10SecAttackDamageIon);

                //

                //

                //

                //hardpoints total laser damage over 30 sec
                var hardpoints30SecAttackDamageLaser = new Label();

                FillLabel(hardpoints30SecAttackDamageLaser, unit.Calculate30SecAttackDamageLaser(), mainUnit.Calculate30SecAttackDamageLaser());

                MainGrid.Children.Add(hardpoints30SecAttackDamageLaser);
                Grid.SetColumn(hardpoints30SecAttackDamageLaser, columnIndex);
                ++rowIndex;
                Grid.SetRow(hardpoints30SecAttackDamageLaser, ++rowIndex);
                generatedControls.Add(hardpoints30SecAttackDamageLaser);

                //hardpoints total missle damage over 30 sec
                var hardpoints30SecAttackDamageMissle = new Label();

                FillLabel(hardpoints30SecAttackDamageMissle, unit.Calculate30SecAttackDamageMissle(), mainUnit.Calculate30SecAttackDamageMissle());

                MainGrid.Children.Add(hardpoints30SecAttackDamageMissle);
                Grid.SetColumn(hardpoints30SecAttackDamageMissle, columnIndex);
                Grid.SetRow(hardpoints30SecAttackDamageMissle, ++rowIndex);
                generatedControls.Add(hardpoints30SecAttackDamageMissle);

                //hardpoints total torpedo damage over 30 sec
                var hardpoints30SecAttackDamageTorpedo = new Label();

                FillLabel(hardpoints30SecAttackDamageTorpedo, unit.Calculate30SecAttackDamageTorpedo(), mainUnit.Calculate30SecAttackDamageTorpedo());

                MainGrid.Children.Add(hardpoints30SecAttackDamageTorpedo);
                Grid.SetColumn(hardpoints30SecAttackDamageTorpedo, columnIndex);
                Grid.SetRow(hardpoints30SecAttackDamageTorpedo, ++rowIndex);
                generatedControls.Add(hardpoints30SecAttackDamageTorpedo);

                //hardpoints total ion damage over 30 sec
                var hardpoints30SecAttackDamageIon = new Label();

                FillLabel(hardpoints30SecAttackDamageIon, unit.Calculate30SecAttackDamageIon(), mainUnit.Calculate30SecAttackDamageIon());

                MainGrid.Children.Add(hardpoints30SecAttackDamageIon);
                Grid.SetColumn(hardpoints30SecAttackDamageIon, columnIndex);
                Grid.SetRow(hardpoints30SecAttackDamageIon, ++rowIndex);
                generatedControls.Add(hardpoints30SecAttackDamageIon);

                //

                //

                //

                //hardpoints total laser damage over 60 sec
                var hardpoints60SecAttackDamageLaser = new Label();

                FillLabel(hardpoints60SecAttackDamageLaser, unit.Calculate60SecAttackDamageLaser(), mainUnit.Calculate60SecAttackDamageLaser());

                MainGrid.Children.Add(hardpoints60SecAttackDamageLaser);
                Grid.SetColumn(hardpoints60SecAttackDamageLaser, columnIndex);
                ++rowIndex;
                Grid.SetRow(hardpoints60SecAttackDamageLaser, ++rowIndex);
                generatedControls.Add(hardpoints60SecAttackDamageLaser);

                //hardpoints total missle damage over 60 sec
                var hardpoints60SecAttackDamageMissle = new Label();

                FillLabel(hardpoints60SecAttackDamageMissle, unit.Calculate60SecAttackDamageMissle(), mainUnit.Calculate60SecAttackDamageMissle());

                MainGrid.Children.Add(hardpoints60SecAttackDamageMissle);
                Grid.SetColumn(hardpoints60SecAttackDamageMissle, columnIndex);
                Grid.SetRow(hardpoints60SecAttackDamageMissle, ++rowIndex);
                generatedControls.Add(hardpoints60SecAttackDamageMissle);

                //hardpoints total torpedo damage over 60 sec
                var hardpoints60SecAttackDamageTorpedo = new Label();

                FillLabel(hardpoints60SecAttackDamageTorpedo, unit.Calculate60SecAttackDamageTorpedo(), mainUnit.Calculate60SecAttackDamageTorpedo());

                MainGrid.Children.Add(hardpoints60SecAttackDamageTorpedo);
                Grid.SetColumn(hardpoints60SecAttackDamageTorpedo, columnIndex);
                Grid.SetRow(hardpoints60SecAttackDamageTorpedo, ++rowIndex);
                generatedControls.Add(hardpoints60SecAttackDamageTorpedo);

                //hardpoints total ion damage over 60 sec
                var hardpoints60SecAttackDamageIon = new Label();

                FillLabel(hardpoints60SecAttackDamageIon, unit.Calculate60SecAttackDamageIon(), mainUnit.Calculate60SecAttackDamageIon());

                MainGrid.Children.Add(hardpoints60SecAttackDamageIon);
                Grid.SetColumn(hardpoints60SecAttackDamageIon, columnIndex);
                Grid.SetRow(hardpoints60SecAttackDamageIon, ++rowIndex);
                generatedControls.Add(hardpoints60SecAttackDamageIon);

                ++columnIndex;
            }
        }

        private void FillLabel(Label label, double? value, double? mainValue)
        {
            //
            if (value.HasValue)
            {
                if (mainValue.HasValue)
                {
                    var percentage = value * 100.0d / mainValue;

                    if (percentage > 100)
                    {
                        label.Foreground = brushGreen; //green

                        percentage = Math.Round((double)(percentage - 100), 2);
                        label.Content = $"{value} (+{percentage}%)";
                    }
                    else if (percentage < 100)
                    {
                        label.Foreground = brushRed; //red

                        percentage = Math.Round((double)(100 - percentage), 2);
                        label.Content = $"{value} (-{percentage}%)";
                    }
                    else
                    {
                        label.Content = value;
                    }
                }
                else
                {
                    label.Content = value;
                }
            }
        }

    }
}
