using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balancer.Model
{
    public class Projectile
    {
        public ProjectileFile ProjectileFile { get; set; }
        public Projectile Parent { get; set; }
        public List<Hardpoint> Hardpoints { get; set; }
        public List<Unit> Units { get; set; }



        public string Name { get; set; }



        public string Variant_Of_Existing_Type { get; set; }



        public string Damage_Type { get; set; }
        public bool Damage_Type_fromParent { get; set; }

        public double? Projectile_Max_Flight_Distance { get; set; }
        public bool Projectile_Max_Flight_Distance_fromParent { get; set; }

        public double? Projectile_Damage { get; set; }
        public bool Projectile_Damage_fromParent { get; set; }

        public bool? Projectile_Does_Shield_Damage { get; set; }
        public bool Projectile_Does_Shield_Damage_fromParent { get; set; }

        public bool? Projectile_Does_Energy_Damage { get; set; }
        public bool Projectile_Does_Energy_Damage_fromParent { get; set; }

        public bool? Projectile_Does_Hitpoint_Damage { get; set; }
        public bool Projectile_Does_Hitpoint_Damage_fromParent { get; set; }

        public int? Projectile_Blast_Area_Damage { get; set; }
        public bool Projectile_Blast_Area_Damage_fromParent { get; set; }

        public int? Projectile_Blast_Area_Range { get; set; }
        public bool Projectile_Blast_Area_Range_fromParent { get; set; }

        public int? AI_Combat_Power { get; set; }
        public bool AI_Combat_Power_fromParent { get; set; }



        public Projectile()
        {
            Hardpoints = new List<Hardpoint>();
            Units = new List<Unit>();
        }

        public Projectile(string name)
        {
            this.Name = name;
            Hardpoints = new List<Hardpoint>();
            Units = new List<Unit>();
        }

        public override string ToString()
        {
            if (Name != null)
            {
                return Name;
            }
            
            return base.ToString();
        }
    }
}
