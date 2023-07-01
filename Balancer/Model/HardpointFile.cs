using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balancer.Model
{
    public class HardpointFile
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public int HardpointsCount { get; set; }
        public int ExcludedHardpointsCount { get; set; }
        public List<Hardpoint> Hardpoints { get; set; }
        public string RootElement { get; set; }

        public HardpointFile()
        {
            Hardpoints = new List<Hardpoint>();
        }
    }
}
