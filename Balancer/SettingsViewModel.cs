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
    public class SettingsViewModel : BaseNotifyProp
    {

        private string editorOnePath;
        public string EditorOnePath
        {
            get => editorOnePath;
            set
            {
                editorOnePath = value;
                OnPropertyChanged();
            }
        }

        private string editorTwoPath;
        public string EditorTwoPath
        {
            get => editorTwoPath;
            set
            {
                editorTwoPath = value;
                OnPropertyChanged();
            }
        }

        private string gameRootFolder;
        public string GameRootFolder
        {
            get => gameRootFolder;
            set
            {
                gameRootFolder = value;
                OnPropertyChanged();
            }
        }

        private bool filterDeathClones;
        public bool FilterDeathClones
        {
            get => filterDeathClones;
            set
            {
                filterDeathClones = value;
                OnPropertyChanged();
            }
        }


        public SettingsViewModel()
        {
            EditorOnePath = Config.getInstance().EditorOne;
            EditorTwoPath = Config.getInstance().EditorTwo;
            GameRootFolder = Config.getInstance().GameRootFolder;
            FilterDeathClones = Config.getInstance().FilterDeathClones;
        }

        public void Apply()
        {
            Config.getInstance().EditorOne = EditorOnePath;
            Config.getInstance().EditorTwo = EditorTwoPath;
            Config.getInstance().GameRootFolder = GameRootFolder;
            Config.getInstance().FilterDeathClones = FilterDeathClones;

            Config.Save();
        }

    }
}
