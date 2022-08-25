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
        public List<string> LastSelectedFiles { get; set; }
        public bool ShowFiles { get; set; }

        public string EditorOne { get; set; }
        public string EditorTwo { get; set; }

        public string GameRootFolder { get; set; }

        public static void Save(Config config, string filePath = ".eaw-balancer.json")
        {
            string jsonString = JsonSerializer.Serialize(config);
            File.WriteAllText(filePath, jsonString);
            //File.SetAttributes(filePath, FileAttributes.Hidden);
        }

        public static Config Load(string filePath = ".eaw-balancer.json")
        {
            string jsonString = File.ReadAllText(filePath);
            Config config = JsonSerializer.Deserialize<Config>(jsonString)!;

            return config;
        }
    }
}
