using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balancer.Model
{
    public class ParseResult
    {
        public List<GameObjectFile> GameObjectFiles { get; set; }
        public List<Unit> Units;

        public ParseResult(List<GameObjectFile> gameObjectFiles, List<Unit> units)
        {
            GameObjectFiles = gameObjectFiles;
            Units = units;
        }

        public ParseResult()
        {
            GameObjectFiles = new List<GameObjectFile>();
            Units = new List<Unit>();
        }
    }
}
