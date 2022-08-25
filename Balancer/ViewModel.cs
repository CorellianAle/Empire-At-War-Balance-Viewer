using Balancer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Balancer
{
    internal class ViewModel : BaseNotifyProp
    {
        private const string ChartAiCombatPower = "AI Combat Power";
        private const string ChartDamage = "Damage";
        private const string ChartAutoresolveHealth = "Autoresolve Health";
        private const string ChartShieldPoints = "Shield Points";
        private const string ChartTacticalHealth = "Tactical Health";
        private const string ChartShieldRefreshRate = "Shield Refresh Rate";
        private const string ChartEnergyCapacity = "Energy Capacity";
        private const string ChartEnergyRefreshRate = "Energy Refresh Rate";
        private const string ChartBuildCostCredits = "Build Cost Credits";
        private const string ChartPiracyValueCredits = "Piracy Value Credits";
        private const string ChartBuildTimeSeconds = "Build Time Seconds";
        private const string ChartSpaceFOWRevealRange = "Space FOW Reveal Range";
        private const string ChartTargetingMaxAttackDistance = "Targeting Max Attack Distance";
        private const string ChartScoreCostCredits = "Score Cost Credits";
        private const string ChartTacticalBuildCostMultiplayer = "Tactical Build Cost Multiplayer";
        private const string ChartTacticalBuildTimeSeconds = "Tactical Build Time Seconds";

        private Config config;

        public List<string> LastSelectedFiles
        {
            get => config.LastSelectedFiles;
        }

        private List<string> files;
        public List<string> Files
        {
            get => files;
            private set
            {
                files = value;
                OnPropertyChanged();
            }
        }

        #region chart

        private ObservableCollection<string> chartTypes;
        public ObservableCollection<string> ChartTypes
        {
            get => chartTypes;
            private set
            {
                chartTypes = value;
                OnPropertyChanged();
            }
        }

        private string selectedChartType;
        public string SelectedChartType
        {
            get => selectedChartType;
            set
            {
                selectedChartType = value;
                OnPropertyChanged();

                RefreshChart();
                OnChartRefresh();
            }
        }

        private string chartTitle;
        public string ChartTitle
        {
            get => chartTitle;
            private set
            {
                chartTitle = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ChartItem> chartItems;
        public ObservableCollection<ChartItem> ChartItems
        {
            get => chartItems;
            set
            {
                chartItems = value;
                OnPropertyChanged();
            }
        }

        private int chartUnitsAmount;
        public int ChartItemsAmount
        {
            get => chartUnitsAmount;
            private set
            {
                chartUnitsAmount = value;
                OnPropertyChanged();
            }
        }

        public delegate void ChartRefreshDelegate();
        public ChartRefreshDelegate OnChartRefresh { get; set; }

        #endregion

        private bool showFiles;
        public bool ShowFiles
        {
            get => showFiles;
            set
            {
                showFiles = value;
                OnPropertyChanged();

                //save config
                config.ShowFiles = value;
                SaveConfig();
            }
        }

        private ObservableCollection<GameObjectFile> gameObjectFiles;
        public ObservableCollection<GameObjectFile> GameObjectFiles
        {
            get => gameObjectFiles;
            private set
            {
                gameObjectFiles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Unit> units;
        public ObservableCollection<Unit> Units
        {
            get => units;
            private set
            {
                units = value;
                OnPropertyChanged();
            }
        }

        private Unit selectedUnit;
        public Unit SelectedUnit
        {
            get => selectedUnit;
            set
            {
                selectedUnit = value;
                OnPropertyChanged();
            }
        }


        public ViewModel()
        {
            InitializeChartTypes();

            LoadConfig();
            ShowFiles = config.ShowFiles;
        }




        /// <summary>
        /// Initialize chartTypes
        /// </summary>
        private void InitializeChartTypes()
        {
            var charts = new List<string>
            {
                ChartAiCombatPower,
                ChartDamage,
                ChartAutoresolveHealth,
                ChartShieldPoints,
                ChartTacticalHealth,
                ChartShieldRefreshRate,
                ChartEnergyCapacity,
                ChartEnergyRefreshRate,
                ChartBuildCostCredits,
                ChartPiracyValueCredits,
                ChartBuildTimeSeconds,
                ChartSpaceFOWRevealRange,
                ChartTargetingMaxAttackDistance,
                ChartScoreCostCredits,
                ChartTacticalBuildCostMultiplayer,
                ChartTacticalBuildTimeSeconds
            };

            ChartTypes = new ObservableCollection<string>(charts);
        }

        /// <summary>
        /// Load contents of provided files.
        /// </summary>
        /// <param name="files">List of XML file paths</param>
        public void UpdateFiles(List<string> files)
        {
            Files = files;

            //parse
            var result = Parser.parseXmlFiles(Files);

            GameObjectFiles = new ObservableCollection<GameObjectFile>(result.GameObjectFiles);
            Units = new ObservableCollection<Unit>(result.Units);

            //update config
            config.LastSelectedFiles = files;
            SaveConfig();
        }

        /// <summary>
        /// Reload previously selected files.
        /// </summary>
        public void RefreshFiles()
        {
            UpdateFiles(config.LastSelectedFiles);
        }

        /// <summary>
        /// Toggle Files ListView visibility.
        /// </summary>
        public void ToggleFilesDisplay()
        {
            ShowFiles = !showFiles;
        }

        /// <summary>
        /// Save app configuration to file.
        /// </summary>
        public void SaveConfig()
        {
            Config.Save(config);
        }

        /// <summary>
        /// Load app configuration from file.
        /// </summary>
        public void LoadConfig()
        {
            try
            {
                config = Config.Load();
            }
            catch
            {
                //create new and try to save it
                config = new Config();
                
                try
                {
                    Config.Save(config);
                }
                catch
                {
                    MessageBox.Show("Unable to save configuration file.");
                }
            }
            
            ShowFiles = config.ShowFiles;
        }

        /// <summary>
        /// On close.
        /// </summary>
        public void Close()
        {
            Config.Save(config);
        }

        /// <summary>
        /// Ask user to open previous selected files?
        /// </summary>
        public void AskRestoreFiles()
        {
            if (config.LastSelectedFiles != null && config.LastSelectedFiles.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Open previously selected files?", "Restore Previous?", MessageBoxButton.YesNo);

                if (result.Equals(MessageBoxResult.Yes))
                {
                    RefreshFiles();
                }
            }
        }

        public void RefreshChart()
        {
            if (units == null || units.Count == 0)
            {
                return;
            }

            var chartItems = new List<ChartItem>();

            switch (selectedChartType)
            {
                case ChartAiCombatPower:
                    ChartTitle = ChartAiCombatPower;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.AI_Combat_Power.HasValue ? unit.AI_Combat_Power.Value : 0));
                    }

                    break;

                case ChartDamage:
                    ChartTitle = ChartDamage;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Damage.HasValue ? unit.Damage.Value : 0));
                    }

                    break;

                case ChartAutoresolveHealth:
                    ChartTitle = ChartAutoresolveHealth;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Autoresolve_Health.HasValue ? unit.Autoresolve_Health.Value : 0));
                    }

                    break;

                case ChartShieldPoints:
                    ChartTitle = ChartShieldPoints;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Shield_Points.HasValue ? unit.Shield_Points.Value : 0));
                    }

                    break;

                case ChartTacticalHealth:
                    ChartTitle = ChartTacticalHealth;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Tactical_Health.HasValue ? unit.Tactical_Health.Value : 0));
                    }

                    break;

                case ChartShieldRefreshRate:
                    ChartTitle = ChartShieldRefreshRate;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Shield_Refresh_Rate.HasValue ? unit.Shield_Refresh_Rate.Value : 0));
                    }

                    break;

                case ChartEnergyCapacity:
                    ChartTitle = ChartEnergyCapacity;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Energy_Capacity.HasValue ? unit.Energy_Capacity.Value : 0));
                    }

                    break;

                case ChartEnergyRefreshRate:
                    ChartTitle = ChartEnergyRefreshRate;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Energy_Refresh_Rate.HasValue ? unit.Energy_Refresh_Rate.Value : 0));
                    }

                    break;

                case ChartBuildCostCredits:
                    ChartTitle = ChartBuildCostCredits;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Build_Cost_Credits.HasValue ? unit.Build_Cost_Credits.Value : 0));
                    }

                    break;

                case ChartPiracyValueCredits:
                    ChartTitle = ChartPiracyValueCredits;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Piracy_Value_Credits.HasValue ? unit.Piracy_Value_Credits.Value : 0));
                    }

                    break;

                case ChartBuildTimeSeconds:
                    ChartTitle = ChartBuildTimeSeconds;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Build_Time_Seconds.HasValue ? unit.Build_Time_Seconds.Value : 0));
                    }

                    break;
                case ChartSpaceFOWRevealRange:
                    ChartTitle = ChartSpaceFOWRevealRange;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Space_FOW_Reveal_Range.HasValue ? unit.Space_FOW_Reveal_Range.Value : 0));
                    }

                    break;

                case ChartTargetingMaxAttackDistance:
                    ChartTitle = ChartTargetingMaxAttackDistance;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Targeting_Max_Attack_Distance.HasValue ? unit.Targeting_Max_Attack_Distance.Value : 0));
                    }

                    break;

                case ChartScoreCostCredits:
                    ChartTitle = ChartScoreCostCredits;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Score_Cost_Credits.HasValue ? unit.Score_Cost_Credits.Value : 0));
                    }

                    break;

                case ChartTacticalBuildCostMultiplayer:
                    ChartTitle = ChartTacticalBuildCostMultiplayer;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Tactical_Build_Cost_Multiplayer.HasValue ? unit.Tactical_Build_Cost_Multiplayer.Value : 0));
                    }

                    break;

                case ChartTacticalBuildTimeSeconds:
                    ChartTitle = ChartTacticalBuildTimeSeconds;

                    foreach (var unit in units)
                    {
                        chartItems.Add(new ChartItem(unit.Name, unit.Tactical_Build_Time_Seconds.HasValue ? unit.Tactical_Build_Time_Seconds.Value : 0));
                    }

                    break;
            }

            ChartItems = new ObservableCollection<ChartItem>(chartItems);
            ChartItemsAmount = chartItems.Count;
        }

        /// <summary>
        /// Open file in default program.
        /// </summary>
        public void OpenSelectedUnitFileEditorOne()
        {
            if (selectedUnit == null)
            {
                return;
            }

            if (config == null || config.EditorOne == null)
            {
                return;
            }

            Process.Start(config.EditorOne, selectedUnit.GameObjectFile.Path);
        }

        /// <summary>
        /// Open file in default program.
        /// </summary>
        public void OpenSelectedUnitFileEditorTwo()
        {
            if (selectedUnit == null)
            {
                return;
            }

            if (config == null || config.EditorTwo == null)
            {
                return;
            }

            Process.Start(config.EditorTwo, selectedUnit.GameObjectFile.Path);
        }

        /// <summary>
        /// Open file in Windows Explorer
        /// </summary>
        public void OpenSelectedUnitFileInExplorer()
        {
            if (selectedUnit == null)
            {
                return;
            }

            Process.Start("explorer.exe", selectedUnit.GameObjectFile.Path);
        }
    }
}
