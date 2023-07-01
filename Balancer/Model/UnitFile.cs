using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balancer.Model
{
    public class UnitFile : BaseNotifyProp
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public int UnitsCount { get; set; }
        public int ExcludedUnitsCount { get; set; }
        public List<Unit> Units { get; set; }
        public string RootElement { get; set; }

        public UnitFile()
        {
            Units = new List<Unit>();
        }
    }
}
