using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balancer.Model
{
    internal class ChartItem
    {
        public string Name { get; set; }
        public double Value { get; set; }

        public ChartItem(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}
