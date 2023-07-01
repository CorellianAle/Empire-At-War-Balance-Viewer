using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balancer.Model
{
    public class ParseResult
    {
        public List<ProjectileFile> ProjectileFiles { get; set; }
        public List<Projectile> Projectiles { get; set; }
        public Dictionary<string, Projectile> ProjectilesMap { get; set; }

        public List<HardpointFile> HardpointFiles { get; set; }
        public List<Hardpoint> Hardpoints { get; set; }
        public Dictionary<string, Hardpoint> HardpointsMap { get; set; }

        public List<UnitFile> UnitFiles { get; set; }
        public List<Unit> Units { get; set; }
        public Dictionary<string, Unit> UnitsMap { get; set; }



        public ParseResult(List<ProjectileFile> projectileFiles, List<Projectile> projectiles, Dictionary<string, Projectile> projectilesMap,
            List<HardpointFile> hardpointFiles, List<Hardpoint> hardpoints, Dictionary<string, Hardpoint> hardpointsMap,
            List<UnitFile> unitFiles, List<Unit> units, Dictionary<string, Unit> unitsMap)
        {
            ProjectileFiles = projectileFiles;
            Projectiles = projectiles;
            ProjectilesMap = projectilesMap;

            HardpointFiles = hardpointFiles;
            Hardpoints = hardpoints;
            HardpointsMap = hardpointsMap;

            UnitFiles = unitFiles;
            Units = units;
            UnitsMap = unitsMap;
        }
    }
}