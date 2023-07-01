using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Balancer.Model
{
    public class Config
    {
        #region singleton

        public static Config instance;

        public static Config getInstance()
        {
            if (instance == null)
            {
                instance = new Config();
            }

            return instance;
        }

        #endregion





        public List<string> LastSelectedProjectileFiles { get; set; }
        public List<string> LastSelectedHardpointFiles { get; set; }
        public List<string> LastSelectedUnitFiles { get; set; }
        
        

        public bool ShowFiles { get; set; }

        public bool FilterDeathClones { get; set; }

        public string EditorOne { get; set; }
        public string EditorTwo { get; set; }

        public string GameRootFolder { get; set; }




        public Config()
        {
            ShowFiles = true;
            FilterDeathClones = true;
            EditorOne = "notepad.exe";
            EditorTwo = "notepad.exe";
        }

        public Config(Config conig)
        {
            LastSelectedProjectileFiles = conig.LastSelectedProjectileFiles;
            LastSelectedHardpointFiles = conig.LastSelectedHardpointFiles;
            LastSelectedUnitFiles = conig.LastSelectedUnitFiles;

            ShowFiles = conig.ShowFiles;

            FilterDeathClones = conig.FilterDeathClones;

            EditorOne = conig.EditorOne;
            EditorTwo = conig.EditorTwo;

            GameRootFolder = conig.GameRootFolder;
        }
        



        public static void Initialize()
        {
            Initialize(new Config());
        }

        public static void Initialize(Config config)
        {
            instance = config;
        }

        public static void Save(string filePath = ".eaw-balancer.json")
        {
            string jsonString = JsonSerializer.Serialize(instance);
            File.WriteAllText(filePath, jsonString);
        }

        public static void Load(string filePath = ".eaw-balancer.json")
        {
            string jsonString = File.ReadAllText(filePath);
            instance = JsonSerializer.Deserialize<Config>(jsonString)!;
        }
    }
}
