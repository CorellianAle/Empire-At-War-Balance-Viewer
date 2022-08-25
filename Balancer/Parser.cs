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
        /// <summary>
        /// Parses single XML file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static GameObjectFile parseGameObjectFile(string filePath)
        {
            var gameObjectFile = new GameObjectFile();

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
                if (name != null)
                {
                    if (name.ToLower().Contains("death_clone"))
                    {
                        isDeathClone = true;
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
                    if (childElement.Name == "xxxSpace_Model_Name")
                    {
                        ++excludedUnitsCount;
                        isDeathClone = true;
                        break;
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
                }

                //add to units and link to file (two-way)
                if (!isDeathClone)
                {
                    unit.GameObjectFile = gameObjectFile;
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
        private static List<GameObjectFile> parseGameObjectFiles(List<string> filePaths)
        {
            var gameObjectFiles = new List<GameObjectFile>();

            foreach (var filePath in filePaths)
            {
                var gameObjectfile = parseGameObjectFile(filePath);

                gameObjectFiles.Add(gameObjectfile);
            }

            return gameObjectFiles;
        }

        /// <summary>
        /// Parses multiple files. Groups units from all files to single collection.
        /// </summary>
        /// <param name="filePaths"></param>
        /// <returns></returns>
        public static ParseResult parseXmlFiles(List<string> filePaths)
        {
            //parse files
            var gameObjectFiles = parseGameObjectFiles(filePaths);

            //group units
            var units = new List<Unit>();

            foreach (var gameObjectFile in gameObjectFiles)
            {
                foreach (var unit in gameObjectFile.Units)
                {
                    units.Add(unit);
                }
            }

            //handle "Variant_Of_Existing_Type"
            foreach (var unit in units)
            {
                if (unit.Variant_Of_Existing_Type != null)
                {
                    var parent = units.First(a => a.Name == unit.Variant_Of_Existing_Type);

                    FillChildValuesByParent(parent, unit);
                }
            }

            return new ParseResult(gameObjectFiles, units);
        }

        /// <summary>
        /// Fills missing values of a child by it's parent's corresponding values
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
        }
    }
}
