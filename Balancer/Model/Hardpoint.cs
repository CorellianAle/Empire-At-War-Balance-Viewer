using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balancer.Model
{
    public class Hardpoint
    {
        public HardpointFile HardpointFile { get; set; }
        public List<Unit> Units { get; set; }



        public string Name { get; set; }



        public string Type { get; set; }
        public bool? Is_Targetable { get; set; }
        public bool? Is_Destroyable { get; set; }
        public double? Health { get; set; }

        public string Damage_Type { get; set; }

        public string ProjectileName { get; set; }
        public Projectile Fire_Projectile_Type { get; set; }

        public double? Fire_Min_Recharge_Seconds { get; set; }
        public double? Fire_Max_Recharge_Seconds { get; set; }
        public double? Fire_Pulse_Count { get; set; }
        public double? Fire_Pulse_Delay_Seconds { get; set; }
        public double? Fire_Range_Distance { get; set; }



        public Hardpoint()
        {
            Units = new List<Unit>();
        }

        public Hardpoint(string name)
        {
            Units = new List<Unit>();
            Name = name;
        }

        public override string ToString()
        {
            if (Name != null)
            {
                return Name;
            }

            return base.ToString();
        }

        /// <summary>
        /// damage = Projectile_Damage * Fire_Pulse_Count
        /// </summary>
        /// <returns></returns>
        public double? CalculateInitialAttackDamage()
        {
            if (Fire_Projectile_Type == null)
            {
                return null;
            }

            if (Fire_Projectile_Type.Projectile_Damage == null)
            {
                return null;
            }

            if (Fire_Pulse_Count == null)
            {
                return null;
            }

            return Fire_Projectile_Type.Projectile_Damage * Fire_Pulse_Count;
        }

        public double? CalculateXSecondsAttackDamage(double maxSeconds)
        {
            if (Fire_Projectile_Type == null)
            {
                return null;
            }

            if (Fire_Projectile_Type.Projectile_Damage == null)
            {
                return null;
            }

            if (Fire_Pulse_Count == null)
            {
                return null;
            }

            if (Fire_Max_Recharge_Seconds == null)
            {
                return null;
            }

            if (Fire_Pulse_Delay_Seconds == null)
            {
                return null;
            }

            double seconds = 0;
            double damage = 0;
            double count = 0;

            while (seconds < maxSeconds)
            {
                ++count;
                damage += Fire_Projectile_Type.Projectile_Damage.Value;
                seconds += Fire_Pulse_Delay_Seconds.Value;

                if (count == Fire_Pulse_Count)
                {
                    seconds += Fire_Max_Recharge_Seconds.Value;
                    count = 0;
                }
            }

            return damage;
        }
    }
}
