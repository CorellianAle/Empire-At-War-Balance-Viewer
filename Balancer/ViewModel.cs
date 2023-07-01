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
    public class ViewModel : BaseNotifyProp
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

        private const string ChartLaserDamageInitialAttack = "Laser Damage Initial Attack ";
        private const string ChartMissleDamageInitialAttack = "Missle Damage Initial Attack ";
        private const string ChartTorpedoDamageInitialAttack = "Torpedo Damage Initial Attack ";
        private const string ChartIonDamageInitialAttack = "Ion Damage Initial Attack ";

        private const string ChartLaserDamageOver10Sec = "Laser Damage over 10 sec";
        private const string ChartMissleDamageOver10Sec = "Missle Damage over 10 sec";
        private const string ChartTorpedoDamageOver10Sec = "Torpedo Damage over 10 sec";
        private const string ChartIonDamageOver10Sec = "Ion Damage over 10 sec";

        private const string ChartLaserDamageOver30Sec = "Laser Damage over 30 sec";
        private const string ChartMissleDamageOver30Sec = "Missle Damage over 30 sec";
        private const string ChartTorpedoDamageOver30Sec = "Torpedo Damage over 30 sec";
        private const string ChartIonDamageOver30Sec = "Ion Damage over 30 sec";

        private const string ChartLaserDamageOver60Sec = "Laser Damage over 60 sec";
        private const string ChartMissleDamageOver60Sec = "Missle Damage over 60 sec";
        private const string ChartTorpedoDamageOver60Sec = "Torpedo Damage over 60 sec";
        private const string ChartIonDamageOver60Sec = "Ion Damage over 60 sec";



        public List<string> LastSelectedProjectileFiles
        {
            get => Config.getInstance().LastSelectedProjectileFiles;
        }

        public List<string> LastSelectedHardpointFiles
        {
            get => Config.getInstance().LastSelectedHardpointFiles;
        }

        public List<string> LastSelectedUnitFiles
        {
            get => Config.getInstance().LastSelectedUnitFiles;
        }

        private List<string> projectileFilePaths;
        public List<string> ProjectileFilePaths
        {
            get => projectileFilePaths;
            private set
            {
                projectileFilePaths = value;
                OnPropertyChanged();
            }
        }

        private List<string> hardpointFilePaths;
        public List<string> HardpointFilePaths
        {
            get => hardpointFilePaths;
            private set
            {
                hardpointFilePaths = value;
                OnPropertyChanged();
            }
        }

        private List<string> unitFilePaths;
        public List<string> UnitFilePaths
        {
            get => unitFilePaths;
            private set
            {
                unitFilePaths = value;
                OnPropertyChanged();
            }
        }





        private ObservableCollection<Projectile> comparedProjectiles;
        public ObservableCollection<Projectile> ComparedProjectiles
        {
            get => comparedProjectiles;
            set
            {
                comparedProjectiles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Hardpoint> comparedHardpoints;
        public ObservableCollection<Hardpoint> ComparedHardpoints
        {
            get => comparedHardpoints;
            set
            {
                comparedHardpoints = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Unit> comparedUnits;
        public ObservableCollection<Unit> ComparedUnits
        {
            get => comparedUnits;
            set
            {
                comparedUnits = value;
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

                RefreshUnitsChartItems();
                //OnChartRefresh();
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
                Config.getInstance().ShowFiles = value;
                SaveConfig();
            }
        }




        #region projectiles

        private ObservableCollection<ProjectileFile> projectileFiles;
        public ObservableCollection<ProjectileFile> ProjectileFiles
        {
            get => projectileFiles;
            private set
            {
                projectileFiles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Projectile> projectiles;
        public ObservableCollection<Projectile> Projectiles
        {
            get => projectiles;
            private set
            {
                projectiles = value;
                OnPropertyChanged();
            }
        }

        private Projectile selectedProjectile;
        public Projectile SelectedProjectile
        {
            get => selectedProjectile;
            set
            {
                selectedProjectile = value;
                OnPropertyChanged();
            }
        }

        #endregion




        #region hardpoints

        private ObservableCollection<HardpointFile> hardpointFiles;
        public ObservableCollection<HardpointFile> HardpointFiles
        {
            get => hardpointFiles;
            private set
            {
                hardpointFiles = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Hardpoint> hardpoints;
        public ObservableCollection<Hardpoint> Hardpoints
        {
            get => hardpoints;
            private set
            {
                hardpoints = value;
                OnPropertyChanged();
            }
        }

        private Hardpoint selectedHardpoint;
        public Hardpoint SelectedHardpoint
        {
            get => selectedHardpoint;
            set
            {
                selectedHardpoint = value;
                OnPropertyChanged();
            }
        }

        #endregion




        #region units

        private ObservableCollection<UnitFile> unitFiles;
        public ObservableCollection<UnitFile> UnitFiles
        {
            get => unitFiles;
            private set
            {
                unitFiles = value;
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

        #endregion




        public ViewModel()
        {
            InitializeChartTypes();

            LoadConfig();
            ShowFiles = Config.getInstance().ShowFiles;
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
                ChartTacticalBuildTimeSeconds,
                ChartLaserDamageInitialAttack,
                ChartMissleDamageInitialAttack,
                ChartTorpedoDamageInitialAttack,
                ChartIonDamageInitialAttack,
                ChartLaserDamageOver10Sec,
                ChartMissleDamageOver10Sec,
                ChartTorpedoDamageOver10Sec,
                ChartIonDamageOver10Sec,
                ChartLaserDamageOver30Sec,
                ChartMissleDamageOver30Sec,
                ChartTorpedoDamageOver30Sec,
                ChartIonDamageOver30Sec,
                ChartLaserDamageOver60Sec,
                ChartMissleDamageOver60Sec,
                ChartTorpedoDamageOver60Sec,
                ChartIonDamageOver60Sec
            };

            ChartTypes = new ObservableCollection<string>(charts);
        }

        /// <summary>
        /// Load contents of provided unit XML files.
        /// </summary>
        /// <param name="files">List of XML file paths</param>
        public void UpdateFiles(List<string> projectileFiles, List<string> hardpointFiles, List<string> unitFiles)
        {
            //update config
            var config = Config.getInstance();

            config.LastSelectedProjectileFiles = projectileFiles;
            config.LastSelectedHardpointFiles = hardpointFiles;
            config.LastSelectedUnitFiles = unitFiles;

            SaveConfig();

            ProjectileFilePaths = projectileFiles;
            HardpointFilePaths = hardpointFiles;
            UnitFilePaths = unitFiles;

            //parse
            var result = Parser.parseXmlFiles(ProjectileFilePaths, HardpointFilePaths, UnitFilePaths, config.FilterDeathClones);

            ProjectileFiles = new ObservableCollection<ProjectileFile>(result.ProjectileFiles);
            Projectiles = new ObservableCollection<Projectile>(result.Projectiles);

            HardpointFiles = new ObservableCollection<HardpointFile>(result.HardpointFiles);
            Hardpoints = new ObservableCollection<Hardpoint>(result.Hardpoints);

            UnitFiles = new ObservableCollection<UnitFile>(result.UnitFiles);
            Units = new ObservableCollection<Unit>(result.Units);
        }

        /// <summary>
        /// Reload previously selected files.
        /// </summary>
        public void RefreshData()
        {
            UpdateFiles(Config.getInstance().LastSelectedProjectileFiles, Config.getInstance().LastSelectedHardpointFiles, Config.getInstance().LastSelectedUnitFiles);
        }





        /// <summary>
        /// Add new units XML files to previously selected and refreshes everything.
        /// </summary>
        /// <param name="unitsFiles"></param>
        public void AddUnitsXmlFiles(List<string> newUnitsFiles)
        {
            if (newUnitsFiles == null)
            {
                return;
            }

            var config = Config.getInstance();

            if (config.LastSelectedUnitFiles != null && config.LastSelectedUnitFiles.Count > 0)
            {
                UnitFilePaths = config.LastSelectedUnitFiles.Union(newUnitsFiles).ToList();
            }
            else
            {
                UnitFilePaths = newUnitsFiles;
            }

            //update config
            config.LastSelectedUnitFiles = UnitFilePaths;

            SaveConfig();

            RefreshData();
        }

        public void RemoveUnitsXmlFiles(List<UnitFile> unitFiles)
        {
            if (unitFiles == null || unitFiles.Count == 0)
            {
                MessageBox.Show("No unit XML files to delete.", "Information");
                return;
            }

            foreach (UnitFile unitFile in unitFiles)
            {
                UnitFiles.Remove(unitFile);
                UnitFilePaths.Remove(unitFile.Path);

                Config.getInstance().LastSelectedUnitFiles.Remove(unitFile.Path);

                if (unitFile.Units != null)
                {
                    var unitsBackup = new List<Unit>(unitFile.Units);

                    foreach (var unit in unitsBackup)
                    {
                        if (unit == null)
                        {
                            continue;
                        }

                        RemoveUnit(unit);
                    }
                }
            }

            RefreshData();
        }





        /// <summary>
        /// Add new hardpoint XML files to previously selected and refreshes everything.
        /// </summary>
        /// <param name="newHardpointFiles"></param>
        public void AddHardpointsXmlFiles(List<string> newHardpointFiles)
        {
            var config = Config.getInstance();

            if (config.LastSelectedHardpointFiles != null && config.LastSelectedHardpointFiles.Count > 0)
            {
                HardpointFilePaths = config.LastSelectedHardpointFiles.Union(newHardpointFiles).ToList();
            }
            else
            {
                HardpointFilePaths = newHardpointFiles;
            }

            //update config
            config.LastSelectedHardpointFiles = HardpointFilePaths;

            SaveConfig();

            RefreshData();
        }

        /// <summary>
        /// Removes hardpoints XML files from corresponding collections/
        /// Removes hardpoints, associated with this files, from corresponding collections. 
        /// In the end, refreshes everything. 
        /// </summary>
        /// <param name="hardpointFiles">List of parsed hardpoint files entities.</param>
        public void RemoveHardpointsXmlFiles(List<HardpointFile> hardpointFiles)
        {
            if (hardpointFiles == null || hardpointFiles.Count == 0)
            {
                MessageBox.Show("No hardpoint XML files to delete.", "Information");
                return;
            }

            foreach (HardpointFile hardpointFile in hardpointFiles)
            {
                HardpointFiles.Remove(hardpointFile);
                HardpointFilePaths.Remove(hardpointFile.Path);

                Config.getInstance().LastSelectedHardpointFiles.Remove(hardpointFile.Path);

                if (hardpointFile.Hardpoints != null)
                {
                    var hardpointsBackup = new List<Hardpoint>(hardpointFile.Hardpoints);

                    foreach (var hardpoint in hardpointsBackup)
                    {
                        if (hardpoint == null)
                        {
                            continue;
                        }

                        RemoveHardpoint(hardpoint);
                    }
                }
            }

            RefreshData();
        }





        /// <summary>
        /// Add new hardpoint XML files to previously selected and refreshes everything.
        /// </summary>
        /// <param name="newProjectilesFiles"></param>
        public void AddProjectilesXmlFiles(List<string> newProjectilesFiles)
        {
            var config = Config.getInstance();

            if (config.LastSelectedProjectileFiles != null && config.LastSelectedProjectileFiles.Count > 0)
            {
                ProjectileFilePaths = config.LastSelectedProjectileFiles.Union(newProjectilesFiles).ToList();
            }
            else
            {
                ProjectileFilePaths = newProjectilesFiles;
            }

            //update config
            config.LastSelectedProjectileFiles = ProjectileFilePaths;

            SaveConfig();

            RefreshData();
        }

        /// <summary>
        /// Removes hardpoints XML files from corresponding collections/
        /// Removes hardpoints, associated with this files, from corresponding collections. 
        /// In the end, refreshes everything. 
        /// </summary>
        /// <param name="projectilesFiles">List of parsed hardpoint files entities.</param>
        public void RemoveProjectilesXmlFiles(List<ProjectileFile> projectilesFiles)
        {
            if (projectilesFiles == null || projectilesFiles.Count == 0)
            {
                MessageBox.Show("No hardpoint XML files to delete.", "Information");
                return;
            }

            foreach (ProjectileFile projectilesFile in projectilesFiles)
            {
                ProjectileFiles.Remove(projectilesFile);
                ProjectileFilePaths.Remove(projectilesFile.Path);

                Config.getInstance().LastSelectedHardpointFiles.Remove(projectilesFile.Path);

                if (projectilesFile.Projectiles != null)
                {
                    var projectilesBackup = new List<Projectile>(projectilesFile.Projectiles);

                    foreach (var projectile in projectilesBackup)
                    {
                        if (projectile == null)
                        {
                            continue;
                        }

                        RemoveProjectile(projectile);
                    }
                }
            }

            RefreshData();
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
            Config.Save();
        }

        /// <summary>
        /// Load app configuration from file.
        /// </summary>
        public void LoadConfig()
        {
            try
            {
                Config.Load();
            }
            catch
            {
                //create new and try to save it
                Config.Initialize();
                
                try
                {
                    Config.Save();
                }
                catch
                {
                    MessageBox.Show("Unable to save configuration file.");
                }
            }
            
            ShowFiles = Config.getInstance().ShowFiles;
        }

        /// <summary>
        /// On close.
        /// </summary>
        public void Close()
        {
            Config.Save();
        }

        /// <summary>
        /// Asks user to open previous selected files?
        /// </summary>
        public void AskRestoreFiles()
        {
            if (Config.getInstance().LastSelectedUnitFiles != null && Config.getInstance().LastSelectedUnitFiles.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Open previously selected files?", "Restore Previous?", MessageBoxButton.YesNo);

                if (result.Equals(MessageBoxResult.Yes))
                {
                    RefreshData();
                }
            }
        }

        public void RefreshUnitsChartItems()
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





                case ChartLaserDamageInitialAttack:
                    ChartTitle = ChartLaserDamageInitialAttack;

                    foreach (var unit in units)
                    {
                        var value = unit.CalculateInitialAttackDamageLaser();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;

                case ChartMissleDamageInitialAttack:
                    ChartTitle = ChartMissleDamageInitialAttack;

                    foreach (var unit in units)
                    {
                        var value = unit.CalculateInitialAttackDamageMissle();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;

                case ChartTorpedoDamageInitialAttack:
                    ChartTitle = ChartTorpedoDamageInitialAttack;

                    foreach (var unit in units)
                    {
                        var value = unit.CalculateInitialAttackDamageTorpedo();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;

                case ChartIonDamageInitialAttack:
                    ChartTitle = ChartIonDamageInitialAttack;

                    foreach (var unit in units)
                    {
                        var value = unit.CalculateInitialAttackDamageIon();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;





                case ChartLaserDamageOver10Sec:
                    ChartTitle = ChartLaserDamageOver10Sec;

                    foreach (var unit in units)
                    {
                        var value = unit.Calculate10SecAttackDamageLaser();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;

                case ChartMissleDamageOver10Sec:
                    ChartTitle = ChartMissleDamageOver10Sec;

                    foreach (var unit in units)
                    {
                        var value = unit.Calculate10SecAttackDamageMissle();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;

                case ChartTorpedoDamageOver10Sec:
                    ChartTitle = ChartTorpedoDamageOver10Sec;

                    foreach (var unit in units)
                    {
                        var value = unit.Calculate10SecAttackDamageTorpedo();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;

                case ChartIonDamageOver10Sec:
                    ChartTitle = ChartIonDamageOver10Sec;

                    foreach (var unit in units)
                    {
                        var value = unit.Calculate10SecAttackDamageIon();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;





                case ChartLaserDamageOver30Sec:
                    ChartTitle = ChartLaserDamageOver30Sec;

                    foreach (var unit in units)
                    {
                        var value = unit.Calculate30SecAttackDamageLaser();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;

                case ChartMissleDamageOver30Sec:
                    ChartTitle = ChartMissleDamageOver30Sec;

                    foreach (var unit in units)
                    {
                        var value = unit.Calculate30SecAttackDamageMissle();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;

                case ChartTorpedoDamageOver30Sec:
                    ChartTitle = ChartTorpedoDamageOver30Sec;

                    foreach (var unit in units)
                    {
                        var value = unit.Calculate30SecAttackDamageTorpedo();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;

                case ChartIonDamageOver30Sec:
                    ChartTitle = ChartIonDamageOver30Sec;

                    foreach (var unit in units)
                    {
                        var value = unit.Calculate30SecAttackDamageIon();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;





                case ChartLaserDamageOver60Sec:
                    ChartTitle = ChartLaserDamageOver60Sec;

                    foreach (var unit in units)
                    {
                        var value = unit.Calculate60SecAttackDamageLaser();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;

                case ChartMissleDamageOver60Sec:
                    ChartTitle = ChartMissleDamageOver60Sec;

                    foreach (var unit in units)
                    {
                        var value = unit.Calculate60SecAttackDamageMissle();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;

                case ChartTorpedoDamageOver60Sec:
                    ChartTitle = ChartTorpedoDamageOver60Sec;

                    foreach (var unit in units)
                    {
                        var value = unit.Calculate60SecAttackDamageTorpedo();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;

                case ChartIonDamageOver60Sec:
                    ChartTitle = ChartIonDamageOver60Sec;

                    foreach (var unit in units)
                    {
                        var value = unit.Calculate60SecAttackDamageIon();
                        var chartItem = new ChartItem(unit.Name, value.HasValue ? value.Value : 0);
                        chartItems.Add(chartItem);
                    }

                    break;
            }

            ChartItems = new ObservableCollection<ChartItem>(chartItems);
            ChartItemsAmount = chartItems.Count;
        }





        /// <summary>
        /// Opens projectile's corresponding XML file in editor one.
        /// </summary>
        public void OpenSelectedProjectileFileEditorOne()
        {
            if (selectedProjectile == null)
            {
                return;
            }

            OpenFileEditorOne(selectedProjectile.ProjectileFile.Path);
        }

        /// <summary>
        /// Opens hardpoint's corresponding XML file in editor two.
        /// </summary>
        public void OpenSelectedProjectileFileEditorTwo()
        {
            if (selectedProjectile == null)
            {
                return;
            }

            OpenFileEditorTwo(selectedProjectile.ProjectileFile.Path);
        }

        /// <summary>
        /// Opens hardpoint's corresponding XML file in Windows Explorer and selects it.
        /// </summary>
        public void OpenSelectedProjectileFileInExplorer()
        {
            if (selectedProjectile == null)
            {
                return;
            }

            OpenFileInExplorer(selectedProjectile.ProjectileFile.Path);
        }





        /// <summary>
        /// Opens hardpoint's corresponding XML file in editor one.
        /// </summary>
        public void OpenSelectedHardpointFileEditorOne()
        {
            if (selectedHardpoint == null)
            {
                return;
            }

            OpenFileEditorOne(selectedHardpoint.HardpointFile.Path);
        }

        /// <summary>
        /// Opens hardpoint's corresponding XML file in editor two.
        /// </summary>
        public void OpenSelectedHardpointFileEditorTwo()
        {
            if (selectedHardpoint == null)
            {
                return;
            }

            OpenFileEditorTwo(selectedHardpoint.HardpointFile.Path);
        }

        /// <summary>
        /// Opens hardpoint's corresponding XML file in Windows Explorer and selects it.
        /// </summary>
        public void OpenSelectedHardpointFileInExplorer()
        {
            if (selectedHardpoint == null)
            {
                return;
            }

            OpenFileInExplorer(selectedHardpoint.HardpointFile.Path);
        }





        /// <summary>
        /// Opens unit's corresponding XML file in editor one.
        /// </summary>
        public void OpenSelectedUnitFileEditorOne()
        {
            if (selectedHardpoint == null)
            {
                return;
            }

            OpenFileEditorOne(selectedUnit.UnitFile.Path);
        }

        /// <summary>
        /// Opens unit's corresponding XML file in editor two.
        /// </summary>
        public void OpenSelectedUnitFileEditorTwo()
        {
            if (selectedHardpoint == null)
            {
                return;
            }

            OpenFileEditorTwo(selectedUnit.UnitFile.Path);
        }

        /// <summary>
        /// Opens unit's corresponding XML file in Windows Explorer and selects it.
        /// </summary>
        public void OpenSelectedUnitFileInExplorer()
        {
            if (selectedHardpoint == null)
            {
                return;
            }

            OpenFileInExplorer(selectedUnit.UnitFile.Path);
        }

        



        /// <summary>
        /// Opens file in editor one.
        /// </summary>
        public void OpenFileEditorOne(string filePath)
        {
            if (selectedHardpoint == null)
            {
                return;
            }

            if (Config.getInstance() == null || string.IsNullOrWhiteSpace(Config.getInstance().EditorOne))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            try
            {
                Process.Start(Config.getInstance().EditorOne, filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        /// <summary>
        /// Opens file in editor two.
        /// </summary>
        public void OpenFileEditorTwo(string filePath)
        {
            if (selectedHardpoint == null)
            {
                return;
            }

            if (Config.getInstance() == null || string.IsNullOrWhiteSpace(Config.getInstance().EditorTwo))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            try
            {
                Process.Start(Config.getInstance().EditorTwo, filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        /// <summary>
        /// Opens file in Windows Explorer.
        /// </summary>
        public void OpenFileInExplorer(string filePath)
        {
            if (selectedHardpoint == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            try
            {
                Process.Start(Config.getInstance().EditorTwo, filePath);
            }
            catch (Exception ex)
            {
                Process.Start("explorer.exe", filePath);
            }
        }





        /// <summary>
        /// Removes specified projectile and it's links to other entities.
        /// </summary>
        /// <param name="projectile">Projectile to delete.</param>
        public void RemoveProjectile(Projectile projectile)
        {
            if (projectile == null)
            {
                MessageBox.Show("No projectiles to delete.", "Information");
                return;
            }

            //remove projectile from hardpoints
            if (projectile.Hardpoints != null)
            {
                foreach (var hardpoint in projectile.Hardpoints)
                {
                    if (hardpoint != null)
                    {
                        hardpoint.Fire_Projectile_Type = null;
                    }
                }

                //remove hardpoints
                projectile.Hardpoints.Clear();
            }

            //remove projectile from units
            if (projectile.Units != null)
            {
                foreach (var unit in projectile.Units)
                {
                    if (unit != null)
                    {
                        unit.Projectile_Types = null;
                    }
                }

                //remove units
                projectile.Units.Clear();
            }

            //remove from parent file
            if (projectile.ProjectileFile != null)
            {
                if (projectile.ProjectileFile.Projectiles != null)
                {
                    projectile.ProjectileFile.Projectiles.Remove(SelectedProjectile);
                }

                //
                projectile.ProjectileFile = null;
            }

            //remove from current projectiles
            if (Projectiles != null && Projectiles.Count > 0)
            {
                Projectiles.Remove(projectile);
            }
        }

        /// <summary>
        /// Removes selected projectile from units collection.
        /// </summary>
        public void RemoveSelectedProjectile()
        {
            RemoveProjectile(SelectedProjectile);
        }

        /// <summary>
        /// Removes specified projectiles from units collection.
        /// </summary>
        public void RemoveSelectedProjectiles(ICollection<Projectile> projectiles)
        {
            if (projectiles == null || projectiles.Count == 0)
            {
                MessageBox.Show("No projectiles to delete.", "Information");
                return;
            }

            foreach (var projectile in projectiles)
            {
                RemoveProjectile(projectile);
            }
        }

        /// <summary>
        /// Searches for projectile with specified name and removes this unit from units collection.
        /// </summary>
        public void RemoveProjectileByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var projectile = Projectiles.FirstOrDefault(u => name.Equals(u.Name));

            if (projectile == null)
            {
                return;
            }

            RemoveProjectile(projectile);
        }




        /// <summary>
        /// Removes specified hardpoint and it's hardpoints to other entities.
        /// </summary>
        /// <param name="hardpoint">Hardpoint to delete.</param>
        public void RemoveHardpoint(Hardpoint hardpoint)
        {
            if (hardpoint == null)
            {
                MessageBox.Show("No hardpoint to delete.", "Information");
                return;
            }

            //remove hardpoint from units' hardpoints
            if (hardpoint.Units != null)
            {
                foreach (var unit in hardpoint.Units)
                {
                    if (unit != null && unit.Hardpoints != null)
                    {
                        unit.Hardpoints.Remove(hardpoint);
                    }
                }

                hardpoint.Units.Clear();
            }

            //remove hardpoint from projectiles
            if (hardpoint.Fire_Projectile_Type != null)
            {
                if (hardpoint.Fire_Projectile_Type.Hardpoints != null)
                {
                    hardpoint.Fire_Projectile_Type.Hardpoints.Remove(hardpoint);
                }

                hardpoint.Fire_Projectile_Type = null;
            }

            //remove hardpoint from hardpoint file
            if (hardpoint.HardpointFile != null)
            {
                if (hardpoint.HardpointFile.Hardpoints != null)
                {
                    hardpoint.HardpointFile.Hardpoints.Remove(hardpoint);
                }

                //
                hardpoint.HardpointFile = null;
            }

            //remove hardpoint from hardpoints
            if (Hardpoints != null && Hardpoints.Count > 0)
            {
                Hardpoints.Remove(hardpoint);
            }
        }

        /// <summary>
        /// Removes selected hardpoint from hardpoints collection.
        /// </summary>
        public void RemoveSelectedHardpoint()
        {
            if (SelectedHardpoint == null)
            {
                MessageBox.Show("No hardpoints to delete.", "Information");
                return;
            }

            RemoveHardpoint(SelectedHardpoint);
        }

        /// <summary>
        /// Removes specified hardpoints from hardpoints collection.
        /// </summary>
        public void RemoveSelectedHardpoints(ICollection<Hardpoint> hardpoints)
        {
            if (hardpoints == null || hardpoints.Count == 0)
            {
                MessageBox.Show("No hardpoints to delete.", "Information");
                return;
            }

            foreach (var unit in units)
            {
                RemoveUnit(unit);
            }
        }

        /// <summary>
        /// Searches for hardpoint with specified name and removes it.
        /// </summary>
        public void RemoveHardpointByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var hardpoint = Hardpoints.FirstOrDefault(u => name.Equals(u.Name));

            if (hardpoint == null)
            {
                return;
            }

            RemoveHardpoint(hardpoint);
        }

        /// <summary>
        /// Finds hardpoint by it's name and marks it as selected.
        /// </summary>
        public void SelectHardpointByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var hardpoint = Hardpoints.FirstOrDefault(u => u.Name == name);

            if (hardpoint == null)
            {
                return;
            }

            SelectedHardpoint = hardpoint;
        }





        /// <summary>
        /// Removes specified unit and it's links to other entities.
        /// </summary>
        /// <param name="unit">Unit to delete.</param>
        public void RemoveUnit(Unit unit)
        {
            //remove from parent file
            unit.UnitFile.Units.Remove(SelectedUnit);

            //remove unit from hardpoints
            if (unit.Hardpoints != null)
            {
                foreach (var hardpoint in unit.Hardpoints)
                {
                    if (hardpoint != null && hardpoint.Units != null)
                    {
                        hardpoint.Units.Remove(unit);
                    }
                }

                unit.Hardpoints.Clear();
            }

            //remove unit from projectiles
            if (unit.Projectile_Types != null)
            {
                if (unit.Projectile_Types.Units != null)
                {
                    unit.Projectile_Types.Units.Remove(unit);
                }

                unit.Projectile_Types = null;
            }

            //remove from current units
            if (Units != null)
            {
                Units.Remove(unit);
            }
        }

        /// <summary>
        /// Removes selected unit from units collection.
        /// </summary>
        public void RemoveSelectedUnit()
        {
            if (SelectedUnit == null)
            {
                MessageBox.Show("No units to delete.", "Information");
                return;
            }

            RemoveUnit(SelectedUnit);
        }

        /// <summary>
        /// Removes specified units from units collection.
        /// </summary>
        public void RemoveSelectedUnits(ICollection<Unit> units)
        {
            if (units == null || units.Count == 0)
            {
                MessageBox.Show("No units to delete.", "Information");
                return;
            }

            foreach (var unit in units)
            {
                RemoveUnit(unit);
            }
        }

        /// <summary>
        /// Searches for unit with specified name and removes this unit from units collection.
        /// </summary>
        public void RemoveUnitByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var unit = Units.FirstOrDefault(u => name.Equals(u.Name));

            if (unit == null)
            {
                return;
            }

            RemoveUnit(unit);
        }

        /// <summary>
        /// Finds unit by it's name and marks it as selected.
        /// </summary>
        public void SelectUnitByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var unit = Units.FirstOrDefault(u => u.Name == name);

            if (unit == null)
            {
                return;
            }

            SelectedUnit = unit;
        }

        /// <summary>
        /// Clears list of units selected for comparison.
        /// </summary>
        public void ResetComparedUnits()
        {
            if (ComparedUnits == null)
            {
                return;
            }

            ComparedUnits.Clear();
        }





        /// <summary>
        /// Compares selected projectiles - calculates differences in parameters.
        /// </summary>
        /// <param name="units">List of projectiles for comparison.</param>
        public void SelectProjectilesForComparison(ICollection<Projectile> projectiles)
        {
            ComparedProjectiles = new ObservableCollection<Projectile>(projectiles);
        }

        /// <summary>
        /// Compares selected hardpoints - calculates differences in parameters.
        /// </summary>
        /// <param name="units">List of hardpoints for comparison.</param>
        public void SelectHardpointsForComparison(ICollection<Hardpoint> hardpoints)
        {
            ComparedHardpoints = new ObservableCollection<Hardpoint>(hardpoints);
        }

        /// <summary>
        /// Compares selected units - calculates differences in parameters.
        /// </summary>
        /// <param name="units">List of units for comparison.</param>
        public void SelectUnitsForComparison(ICollection<Unit> units)
        {
            ComparedUnits = new ObservableCollection<Unit>(units);
        }
    }
}
