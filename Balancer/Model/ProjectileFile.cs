using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balancer.Model
{
    public class ProjectileFile : BaseNotifyProp
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public int ProjectilesCount { get; set; }
        public int ExcludedProjectilesCount { get; set; }
        public List<Projectile> Projectiles { get; set; }
        public string RootElement { get; set; }

        public ProjectileFile()
        {
            Projectiles = new List<Projectile>();
        }
    }
}
