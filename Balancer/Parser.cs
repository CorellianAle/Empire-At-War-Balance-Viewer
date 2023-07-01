using Balancer.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Balancer
{
    internal class Parser
    {
        #region projectiles

        /// <summary>
        /// Parses single XML file.
        /// 
        /// Locates Projectile tags and, if successful, parses individual parameters.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static ProjectileFile parseProjectileFile(string filePath)
        {
            var projectileFile = new ProjectileFile();

            projectileFile.Path = filePath;
            projectileFile.FileName = Path.GetFileName(filePath);

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlElement? rootNode = doc.DocumentElement;

            //handle empty
            if (rootNode == null)
            {
                return projectileFile;
            }

            //parse root
            projectileFile.RootElement = rootNode.Name;

            //handle units
            int excludedProjectilesCount = 0; //non-space-units and deathclones

            foreach (XmlNode node in rootNode)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                var element = (XmlElement)node;

                if (!(element.Name == "Projectile"))
                {
                    ++excludedProjectilesCount;
                    continue;
                }

                int tempInt;
                double tempDouble;
                bool tempBool;

                //handle unit name
                var name = element.GetAttribute("Name");

                //create unit
                var projectile = new Projectile(name);

                //handle unit parameters
                foreach (XmlNode childNode in element)
                {
                    if (childNode.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }

                    var childElement = (XmlElement)childNode;


                    //get specific parameters
                    if (childElement.Name == "Variant_Of_Existing_Type")
                    {
                        projectile.Variant_Of_Existing_Type = childElement.InnerText;
                    }

                    if (childElement.Name == "Damage_Type")
                    {
                        projectile.Damage_Type = childElement.InnerText;
                    }

                    if (childElement.Name == "Projectile_Max_Flight_Distance")
                    {
                        if (double.TryParse(childElement.InnerText, out tempDouble))
                        {
                            projectile.Projectile_Max_Flight_Distance = tempDouble;
                        }
                    }

                    if (childElement.Name == "Projectile_Damage")
                    {
                        if (double.TryParse(childElement.InnerText, out tempDouble))
                        {
                            projectile.Projectile_Damage = tempDouble;
                        }
                    }

                    if (childElement.Name == "Projectile_Does_Shield_Damage")
                    {
                        //if (bool.TryParse(childElement.InnerText, out tempBool))
                        //{
                        //    projectile.Projectile_Does_Shield_Damage = tempBool;
                        //}

                        projectile.Projectile_Does_Shield_Damage = parseYesNo(childElement.InnerText);
                    }

                    if (childElement.Name == "Projectile_Does_Energy_Damage")
                    {
                        //if (bool.TryParse(childElement.InnerText, out tempBool))
                        //{
                        //    projectile.Projectile_Does_Energy_Damage = tempBool;
                        //}

                        projectile.Projectile_Does_Energy_Damage = parseYesNo(childElement.InnerText);
                    }

                    if (childElement.Name == "Projectile_Does_Hitpoint_Damage")
                    {
                        //if (bool.TryParse(childElement.InnerText, out tempBool))
                        //{
                        //    projectile.Projectile_Does_Hitpoint_Damage = tempBool;
                        //}

                        projectile.Projectile_Does_Hitpoint_Damage = parseYesNo(childElement.InnerText);
                    }

                    if (childElement.Name == "Projectile_Blast_Area_Damage")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            projectile.Projectile_Blast_Area_Damage = tempInt;
                        }
                    }

                    if (childElement.Name == "Projectile_Blast_Area_Range")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            projectile.Projectile_Blast_Area_Range = tempInt;
                        }
                    }

                    if (childElement.Name == "AI_Combat_Power")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            projectile.AI_Combat_Power = tempInt;
                        }
                    }

                }

                projectile.ProjectileFile = projectileFile;
                projectileFile.Projectiles.Add(projectile);
            }

            projectileFile.ProjectilesCount = projectileFile.Projectiles.Count;
            projectileFile.ExcludedProjectilesCount = excludedProjectilesCount;

            return projectileFile;
        }

        /// <summary>
        /// Parses multiple XML files.
        /// </summary>
        /// <param name="filePaths">List of file paths.</param>
        /// <returns></returns>
        private static List<ProjectileFile> parseProjectileFiles(List<string> filePaths)
        {
            var projectileFiles = new List<ProjectileFile>();

            foreach (var filePath in filePaths)
            {
                var projectilefile = parseProjectileFile(filePath);

                projectileFiles.Add(projectilefile);
            }

            return projectileFiles;
        }

        #endregion





        #region hardpoints files

        /// <summary>
        /// Parses single XML file.
        /// 
        /// Locates Projectile tags and, if successful, parses individual parameters.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static HardpointFile parseHardpointFile(string filePath)
        {
            var hardpointFile = new HardpointFile();

            hardpointFile.Path = filePath;
            hardpointFile.FileName = Path.GetFileName(filePath);

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlElement? rootNode = doc.DocumentElement;

            //handle empty
            if (rootNode == null)
            {
                return hardpointFile;
            }

            //parse root
            hardpointFile.RootElement = rootNode.Name;

            //handle units
            int excludedProjectilesCount = 0; //non-space-units and deathclones

            foreach (XmlNode node in rootNode)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                var element = (XmlElement)node;

                if (!(element.Name == "HardPoint"))
                {
                    ++excludedProjectilesCount;
                    continue;
                }

                int tempInt;
                double tempDouble;
                bool tempBool;

                //handle unit name
                var name = element.GetAttribute("Name");

                //create unit
                var hardpoint = new Hardpoint(name);

                //handle unit parameters
                foreach (XmlNode childNode in element)
                {
                    if (childNode.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }

                    var childElement = (XmlElement)childNode;


                    //get specific parameters
                    if (childElement.Name == "Type")
                    {
                        if (!string.IsNullOrEmpty(childElement.InnerText))
                        {
                            hardpoint.Type = childElement.InnerText.Trim();
                        }
                    }

                    if (childElement.Name == "Is_Targetable")
                    {
                        //if (bool.TryParse(childElement.InnerText, out tempBool))
                        //{
                        //    hardpoint.Is_Targetable = tempBool;
                        //}

                        hardpoint.Is_Targetable = parseYesNo(childElement.InnerText);
                    }

                    if (childElement.Name == "Is_Destroyable")
                    {
                        //if (bool.TryParse(childElement.InnerText, out tempBool))
                        //{
                        //    hardpoint.Is_Destroyable = tempBool;
                        //}

                        hardpoint.Is_Destroyable = parseYesNo(childElement.InnerText);
                    }

                    if (childElement.Name == "Health")
                    {
                        if (double.TryParse(childElement.InnerText, out tempDouble))
                        {
                            hardpoint.Health = tempDouble;
                        }
                    }

                    if (childElement.Name == "Damage_Type")
                    {
                        if (!string.IsNullOrEmpty(childElement.InnerText))
                        {
                            hardpoint.Damage_Type = childElement.InnerText.Trim();
                        }
                    }


                    if (childElement.Name == "Fire_Projectile_Type")
                    {
                        if (!string.IsNullOrEmpty(childElement.InnerText))
                        {
                            hardpoint.ProjectileName = childElement.InnerText.Trim();
                        }
                    }


                    if (childElement.Name == "Fire_Min_Recharge_Seconds")
                    {
                        if (double.TryParse(childElement.InnerText, out tempDouble))
                        {
                            hardpoint.Fire_Min_Recharge_Seconds = tempDouble;
                        }
                    }

                    if (childElement.Name == "Fire_Max_Recharge_Seconds")
                    {
                        if (double.TryParse(childElement.InnerText, out tempDouble))
                        {
                            hardpoint.Fire_Max_Recharge_Seconds = tempDouble;
                        }
                    }

                    if (childElement.Name == "Fire_Pulse_Count")
                    {
                        if (double.TryParse(childElement.InnerText, out tempDouble))
                        {
                            hardpoint.Fire_Pulse_Count = tempDouble;
                        }
                    }

                    if (childElement.Name == "Fire_Pulse_Delay_Seconds")
                    {
                        if (double.TryParse(childElement.InnerText, out tempDouble))
                        {
                            hardpoint.Fire_Pulse_Delay_Seconds = tempDouble;
                        }
                    }

                    if (childElement.Name == "Fire_Range_Distance")
                    {
                        if (double.TryParse(childElement.InnerText, out tempDouble))
                        {
                            hardpoint.Fire_Range_Distance = tempDouble;
                        }
                    }

                }

                hardpoint.HardpointFile = hardpointFile;
                hardpointFile.Hardpoints.Add(hardpoint);
            }

            hardpointFile.HardpointsCount = hardpointFile.Hardpoints.Count;
            hardpointFile.ExcludedHardpointsCount = excludedProjectilesCount;

            return hardpointFile;
        }


        /// <summary>
        /// Parses multiple XML files.
        /// </summary>
        /// <param name="filePaths">List of file paths.</param>
        /// <returns></returns>
        private static List<HardpointFile> parseHardpointFiles(List<string> filePaths)
        {
            var hardpointfiles = new List<HardpointFile>();

            foreach (var filePath in filePaths)
            {
                var hardpointFile = parseHardpointFile(filePath);

                hardpointfiles.Add(hardpointFile);
            }

            return hardpointfiles;
        }

        #endregion





        #region units files

        /// <summary>
        /// Parses single XML file.
        /// 
        /// Locates SpaceUnit or UniqueUnit tags and, if successful, parses individual parameters.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isFilterDeathClone">Parser would ignore death clones</param>
        /// <returns></returns>
        private static UnitFile parseGameObjectFile(string filePath, bool isFilterDeathClone)
        {
            var gameObjectFile = new UnitFile();

            gameObjectFile.Path = filePath;
            gameObjectFile.FileName = Path.GetFileName(filePath);

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlElement? rootNode = doc.DocumentElement;

            //handle empty
            if (rootNode == null)
            {
                return gameObjectFile;
            }

            //parse root
            gameObjectFile.RootElement = rootNode.Name;

            //handle units
            int excludedUnitsCount = 0; //non-space-units and deathclones

            foreach (XmlNode node in rootNode)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                var element = (XmlElement) node;

                if (!(element.Name == "SpaceUnit" || element.Name == "UniqueUnit"))
                {
                    ++excludedUnitsCount;
                    continue;
                }

                int tempInt;
                double tempDouble;
                bool isDeathClone = false;

                //handle unit name
                var name = element.GetAttribute("Name");

                //filter death clone by name
                if (isFilterDeathClone)
                {
                    if (name != null)
                    {
                        if (name.ToLower().Contains("death_clone"))
                        {
                            isDeathClone = true;
                        }
                    }
                }


                //create unit
                var unit = new Unit(name);

                //handle unit parameters
                foreach (XmlNode childNode in element)
                {
                    if (childNode.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }

                    var childElement = (XmlElement) childNode;

                    //filter death clone by xxxModel tag
                    if (isFilterDeathClone)
                    {
                        if (childElement.Name == "xxxSpace_Model_Name")
                        {
                            ++excludedUnitsCount;
                            isDeathClone = true;
                            break;
                        }
                    }


                    //get specific parameters
                    if (childElement.Name == "Variant_Of_Existing_Type")
                    {
                        unit.Variant_Of_Existing_Type = childElement.InnerText;
                    }

                    if (childElement.Name == "AI_Combat_Power")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            unit.AI_Combat_Power = tempInt;
                        }
                    }

                    if (childElement.Name == "Damage")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            unit.Damage = tempInt;
                        }
                    }

                    if (childElement.Name == "Autoresolve_Health")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            unit.Autoresolve_Health = tempInt;
                        }
                    }

                    if (childElement.Name == "Shield_Points")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            unit.Shield_Points = tempInt;
                        }
                    }

                    if (childElement.Name == "Tactical_Health")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            unit.Tactical_Health = tempInt;
                        }
                    }

                    if (childElement.Name == "Shield_Refresh_Rate")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            unit.Shield_Refresh_Rate = tempInt;
                        }
                    }

                    if (childElement.Name == "Energy_Capacity")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            unit.Energy_Capacity = tempInt;
                        }
                    }

                    if (childElement.Name == "Energy_Refresh_Rate")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            unit.Energy_Refresh_Rate = tempInt;
                        }
                    }

                    if (childElement.Name == "Build_Cost_Credits")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            unit.Build_Cost_Credits = tempInt;
                        }
                    }

                    if (childElement.Name == "Piracy_Value_Credits")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            unit.Piracy_Value_Credits = tempInt;
                        }
                    }

                    if (childElement.Name == "Build_Time_Seconds")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            unit.Build_Time_Seconds = tempInt;
                        }
                    }

                    if (childElement.Name == "Space_FOW_Reveal_Range")
                    {
                        if (double.TryParse(childElement.InnerText, out tempDouble))
                        {
                            unit.Space_FOW_Reveal_Range = tempDouble;
                        }
                    }

                    if (childElement.Name == "Targeting_Max_Attack_Distance")
                    {
                        if (double.TryParse(childElement.InnerText, out tempDouble))
                        {
                            unit.Targeting_Max_Attack_Distance = tempDouble;
                        }
                    }

                    if (childElement.Name == "Score_Cost_Credits")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            unit.Score_Cost_Credits = tempInt;
                        }
                    }

                    if (childElement.Name == "Tactical_Build_Cost_Multiplayer")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            unit.Tactical_Build_Cost_Multiplayer = tempInt;
                        }
                    }

                    if (childElement.Name == "Tactical_Build_Time_Seconds")
                    {
                        if (int.TryParse(childElement.InnerText, out tempInt))
                        {
                            unit.Tactical_Build_Time_Seconds = tempInt;
                        }
                    }

                    if (childElement.Name == "HardPoints")
                    {
                        if (!string.IsNullOrWhiteSpace(childElement.InnerText))
                        {
                            var strs = childElement.InnerText.Split(',').ToList();
                            var hardpointNames = new List<string>();

                            foreach (var str in strs)
                            {
                                hardpointNames.Add(str.Trim());
                            }

                            unit.HardpointNames = hardpointNames;
                        }
                    }
                }

                //add to units and link to file (two-way)
                if (!isDeathClone)
                {
                    unit.UnitFile = gameObjectFile;
                    gameObjectFile.Units.Add(unit);
                }
            }

            gameObjectFile.UnitsCount = gameObjectFile.Units.Count;
            gameObjectFile.ExcludedUnitsCount = excludedUnitsCount;

            return gameObjectFile;
        }

        /// <summary>
        /// Parses multiple XML files.
        /// </summary>
        /// <param name="filePaths">List of file paths.</param>
        /// <returns></returns>
        private static List<UnitFile> parseGameObjectFiles(List<string> filePaths, bool isFilterDeathClones)
        {
            var gameObjectFiles = new List<UnitFile>();

            foreach (var filePath in filePaths)
            {
                var gameObjectfile = parseGameObjectFile(filePath, isFilterDeathClones);

                gameObjectFiles.Add(gameObjectfile);
            }

            return gameObjectFiles;
        }

        #endregion



        

        /// <summary>
        /// Parses multiple files. Groups units from all files to single collection.
        /// </summary>
        /// <param name="unitFilesPath"></param>
        /// <returns></returns>
        public static ParseResult parseXmlFiles(List<string> projectilesFilesPaths, List<string> hardpointsFilesPaths, List<string> unitsFilesPaths, bool isFilterDeathClones)
        {
            //parse projectiles
            List<ProjectileFile> projectileFiles;

            if (projectilesFilesPaths != null)
            {
                projectileFiles = parseProjectileFiles(projectilesFilesPaths);             
            }
            else
            {
                projectileFiles = new List<ProjectileFile>();
            }

            //parse hardpoints
            List<HardpointFile> hardpointFiles;

            if (hardpointsFilesPaths != null)
            {
                hardpointFiles = parseHardpointFiles(hardpointsFilesPaths);
            }
            else
            {
                hardpointFiles= new List<HardpointFile>();
            }

            //parse unit files
            List<UnitFile> unitFiles;

            if (unitsFilesPaths != null)
            {
                unitFiles = parseGameObjectFiles(unitsFilesPaths, isFilterDeathClones);
            }
            else
            {
                unitFiles = new List<UnitFile>();
            }
            




            //group particles
            var projectilesMap = new Dictionary<string, Projectile>();
            var projectiles = new List<Projectile>();

            foreach (var projectileFile in projectileFiles)
            {
                foreach (var projectile in projectileFile.Projectiles)
                {
                    if (!projectilesMap.ContainsKey(projectile.Name))
                    {
                        projectilesMap.Add(projectile.Name, projectile); 
                    }

                    projectiles.Add(projectile);
                }
            }

            //group hardpoints
            var hardpointsMap = new Dictionary<string, Hardpoint>();
            var hardpoints = new List<Hardpoint>();

            foreach (var hardpointFile in hardpointFiles)
            {
                foreach (var hardpoint in hardpointFile.Hardpoints)
                {
                    if (!hardpointsMap.ContainsKey(hardpoint.Name))
                    {
                        hardpointsMap.Add(hardpoint.Name, hardpoint);
                    }

                    hardpoints.Add(hardpoint);
                }
            }

            //group units
            var unitsMap = new Dictionary<string, Unit>();
            var units = new List<Unit>();

            foreach (var unitFile in unitFiles)
            {
                foreach (var unit in unitFile.Units)
                {
                    if (!unitsMap.ContainsKey(unit.Name))
                    {
                        unitsMap.Add(unit.Name, unit);
                    }    

                    units.Add(unit);
                    
                }
            }

            //handle "Variant_Of_Existing_Type"
            foreach (var projectile in projectiles)
            {
                if (projectile.Variant_Of_Existing_Type != null)
                {
                    var parent = projectilesMap.GetValueOrDefault(projectile.Variant_Of_Existing_Type);


                    if (parent != null)
                    {
                        FillChildValuesByParent(parent, projectile);
                    }
                }
            }

            foreach (var unit in units)
            {
                if (unit.Variant_Of_Existing_Type != null)
                {
                    var parent = unitsMap.GetValueOrDefault(unit.Variant_Of_Existing_Type);

                    if (parent != null)
                    {
                        FillChildValuesByParent(parent, unit);
                    }
                }
            }

            //projectiles to hardpoints
            if (projectiles.Count > 0 && hardpoints.Count > 0)
            {
                foreach (var hardpoint in hardpoints)
                {
                    if (!string.IsNullOrWhiteSpace(hardpoint.ProjectileName))
                    {
                        var projectile = projectilesMap.GetValueOrDefault(hardpoint.ProjectileName);

                        if (projectile != null)
                        {
                            hardpoint.Fire_Projectile_Type = projectile;
                            projectile.Hardpoints.Add(hardpoint);
                        }
                    }
                }
            }

            //hardpoints to units
            if (hardpoints.Count > 0 && units.Count > 0)
            {
                foreach (var unit in units)
                {
                    if (unit.HardpointNames != null)
                    {
                        foreach (var hardpointName in unit.HardpointNames)
                        {
                            var hardpoint = hardpointsMap.GetValueOrDefault(hardpointName);

                            if (hardpoint != null)
                            {
                                unit.Hardpoints.Add(hardpoint);
                                hardpoint.Units.Add(unit);
                            }
                        }
                    }
                }
            }

            //projectiles to units
            if (projectiles.Count > 0 && units.Count > 0)
            {
                foreach (var unit in units)
                {
                    if (unit.ProjectileTypesName != null)
                    {
                        var projectile = projectilesMap.GetValueOrDefault(unit.ProjectileTypesName);

                        if (projectile != null)
                        {
                            unit.Projectile_Types = projectile;
                            projectile.Units.Add(unit);
                        }
                    }
                }
            }

            


            return new ParseResult(projectileFiles, projectiles, projectilesMap, hardpointFiles, hardpoints, hardpointsMap, unitFiles, units, unitsMap);
        }

        /// <summary>
        /// Fills missing values of a child by it's parent's corresponding values.
        /// </summary>
        /// <param name="parent">Parent unit</param>
        /// <param name="child">Child unit (Variant_Of_Existing_Type)</param>
        private static void FillChildValuesByParent(Unit parent, Unit child)
        {
            child.Parent = parent;

            //update fields
            if (child.AI_Combat_Power == null)
            {
                child.AI_Combat_Power = parent.AI_Combat_Power;
                child.AI_Combat_Power_fromParent = true;
            }

            if (child.Damage == null)
            {
                child.Damage = parent.Damage;
                child.Damage_fromParent = true;
            }

            if (child.Autoresolve_Health == null)
            {
                child.Autoresolve_Health = parent.Autoresolve_Health;
                child.Autoresolve_Health_fromParent = true;
            }

            if (child.Shield_Points == null)
            {
                child.Shield_Points = parent.Shield_Points;
                child.Shield_Points_fromParent = true;
            }

            if (child.Tactical_Health == null)
            {
                child.Tactical_Health = parent.Tactical_Health;
                child.Tactical_Health_fromParent = true;
            }

            if (child.Shield_Refresh_Rate == null)
            {
                child.Shield_Refresh_Rate = parent.Shield_Refresh_Rate;
                child.Shield_Refresh_Rate_fromParent = true;
            }

            if (child.Energy_Capacity == null)
            {
                child.Energy_Capacity = parent.Energy_Capacity;
                child.Energy_Capacity_fromParent = true;
            }

            if (child.Energy_Refresh_Rate == null)
            {
                child.Energy_Refresh_Rate = parent.Energy_Refresh_Rate;
                child.Energy_Refresh_Rate_fromParent = true;
            }

            if (child.Build_Cost_Credits == null)
            {
                child.Build_Cost_Credits = parent.Build_Cost_Credits;
                child.Build_Cost_Credits_fromParent = true;
            }

            if (child.Piracy_Value_Credits == null)
            {
                child.Piracy_Value_Credits = parent.Piracy_Value_Credits;
                child.Piracy_Value_Credits_fromParent = true;
            }

            if (child.Build_Time_Seconds == null)
            {
                child.Build_Time_Seconds = parent.Build_Time_Seconds;
                child.Build_Time_Seconds_fromParent = true;
            }

            if (child.Space_FOW_Reveal_Range == null)
            {
                child.Space_FOW_Reveal_Range = parent.Space_FOW_Reveal_Range;
                child.Space_FOW_Reveal_Range_fromParent = true;
            }

            if (child.Targeting_Max_Attack_Distance == null)
            {
                child.Targeting_Max_Attack_Distance = parent.Targeting_Max_Attack_Distance;
                child.Targeting_Max_Attack_Distance_fromParent = true;
            }

            if (child.Score_Cost_Credits == null)
            {
                child.Score_Cost_Credits = parent.Score_Cost_Credits;
                child.Score_Cost_Credits_fromParent = true;
            }

            if (child.Tactical_Build_Cost_Multiplayer == null)
            {
                child.Tactical_Build_Cost_Multiplayer = parent.Tactical_Build_Cost_Multiplayer;
                child.Tactical_Build_Cost_Multiplayer_fromParent = true;
            }

            if (child.Tactical_Build_Time_Seconds == null)
            {
                child.Tactical_Build_Time_Seconds = parent.Tactical_Build_Time_Seconds;
                child.Tactical_Build_Time_Seconds_fromParent = true;
            }

           



            if (child.ArmorTypeName == null)
            {
                child.ArmorTypeName = parent.ArmorTypeName;
                child.Armor_Type_fromParent = true;
            }

            if (child.Armor_Type == null)
            {
                child.Armor_Type = parent.Armor_Type;
                child.Armor_Type_fromParent = true;
            }



            if (child.ShieldArmorTypeName == null)
            {
                child.ShieldArmorTypeName = parent.ShieldArmorTypeName;
                child.Shield_Armor_Type_fromParent = true;
            }

            if (child.Shield_Armor_Type == null)
            {
                child.Shield_Armor_Type = parent.Shield_Armor_Type;
                child.Shield_Armor_Type_fromParent = true;
            }



            if (child.DamageTypeName == null)
            {
                child.DamageTypeName = parent.DamageTypeName;
                child.Damage_fromParent = true;
            }

            if (child.Damage_Type == null)
            {
                child.Damage_Type = parent.Damage_Type;
                child.Damage_fromParent = true;
            }



            if (child.ProjectileTypesName == null)
            {
                child.ProjectileTypesName = parent.ProjectileTypesName;
                child.Projectile_Types_fromParent = true;
            }

            if (child.Projectile_Types == null)
            {
                child.Projectile_Types = parent.Projectile_Types;
                child.Projectile_Types_fromParent = true;
            }



            if (child.Projectile_Fire_Pulse_Count == null)
            {
                child.Projectile_Fire_Pulse_Count = parent.Projectile_Fire_Pulse_Count;
                child.Projectile_Fire_Pulse_Count_fromParent = true;
            }

            if (child.Projectile_Fire_Pulse_Count == null)
            {
                child.Projectile_Fire_Pulse_Count = parent.Projectile_Fire_Pulse_Count;
                child.Projectile_Fire_Pulse_Count_fromParent = true;
            }

            if (child.Projectile_Fire_Pulse_Delay_Seconds == null)
            {
                child.Projectile_Fire_Pulse_Delay_Seconds = parent.Projectile_Fire_Pulse_Delay_Seconds;
                child.Projectile_Fire_Pulse_Delay_Seconds_fromParent = true;
            }

            if (child.Projectile_Fire_Recharge_Seconds == null)
            {
                child.Projectile_Fire_Recharge_Seconds = parent.Projectile_Fire_Recharge_Seconds;
                child.Projectile_Fire_Recharge_Seconds_fromParent = true;
            }



            if (child.HardpointNames == null)
            {
                child.HardpointNames = parent.HardpointNames;
                child.Hardpoints_fromParent = true;
            }

            if (child.Hardpoints == null)
            {
                child.Hardpoints = parent.Hardpoints;
                child.Hardpoints_fromParent = true;
            }
        }

        /// <summary>
        /// Fills missing values of a child by it's parent's corresponding values.
        /// </summary>
        /// <param name="parent">Parent unit</param>
        /// <param name="child">Child unit (Variant_Of_Existing_Type)</param>
        private static void FillChildValuesByParent(Projectile parent, Projectile child)
        {
            child.Parent = parent;

            //update fields
            if (child.Damage_Type == null)
            {
                child.Damage_Type = parent.Damage_Type;
                child.Damage_Type_fromParent = true;
            }

            if (child.Projectile_Max_Flight_Distance == null)
            {
                child.Projectile_Max_Flight_Distance = parent.Projectile_Max_Flight_Distance;
                child.Projectile_Max_Flight_Distance_fromParent = true;
            }

            if (child.Projectile_Max_Flight_Distance == null)
            {
                child.Projectile_Max_Flight_Distance = parent.Projectile_Max_Flight_Distance;
                child.Projectile_Max_Flight_Distance_fromParent = true;
            }

            if (child.Projectile_Damage == null)
            {
                child.Projectile_Damage = parent.Projectile_Damage;
                child.Projectile_Damage_fromParent = true;
            }

            if (child.Projectile_Does_Shield_Damage == null)
            {
                child.Projectile_Does_Shield_Damage = parent.Projectile_Does_Shield_Damage;
                child.Projectile_Does_Shield_Damage_fromParent = true;
            }

            if (child.Projectile_Does_Energy_Damage == null)
            {
                child.Projectile_Does_Energy_Damage = parent.Projectile_Does_Energy_Damage;
                child.Projectile_Does_Energy_Damage_fromParent = true;
            }

            if (child.Projectile_Does_Hitpoint_Damage == null)
            {
                child.Projectile_Does_Hitpoint_Damage = parent.Projectile_Does_Hitpoint_Damage;
                child.Projectile_Does_Hitpoint_Damage_fromParent = true;
            }

            if (child.Projectile_Blast_Area_Damage == null)
            {
                child.Projectile_Blast_Area_Damage = parent.Projectile_Blast_Area_Damage;
                child.Projectile_Blast_Area_Damage_fromParent = true;
            }

            if (child.Projectile_Blast_Area_Range == null)
            {
                child.Projectile_Blast_Area_Range = parent.Projectile_Blast_Area_Range;
                child.Projectile_Blast_Area_Range_fromParent = true;
            }

            if (child.AI_Combat_Power == null)
            {
                child.AI_Combat_Power = parent.AI_Combat_Power;
                child.AI_Combat_Power_fromParent = true;
            }
        }

        private static bool? parseYesNo(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }

            var tr = str.Trim().ToLower();

            if (tr == "yes")
            {
                return true;
            }

            if (tr == "no")
            {
                return false;
            }

            throw new ArgumentException("Provided value is not \"yes\", \"no\" or null");
        }
    }
}
