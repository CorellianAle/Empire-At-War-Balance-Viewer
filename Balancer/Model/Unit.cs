using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balancer.Model
{
    public class Unit
    {
        public GameObjectFile GameObjectFile { get; set; }
        public Unit Parent { get; set; }



        public string Name { get; set; }



        public string Variant_Of_Existing_Type { get; set; }



        public int? AI_Combat_Power { get; set; }
        public bool AI_Combat_Power_fromParent { get; set; }

        public int? Damage { get; set; }
        public bool Damage_fromParent { get; set; }

        public int? Autoresolve_Health { get; set; }
        public bool Autoresolve_Health_fromParent { get; set; }

        public int? Shield_Points { get; set; }
        public bool Shield_Points_fromParent { get; set; }

        public int? Tactical_Health { get; set; }
        public bool Tactical_Health_fromParent { get; set; }

        public int? Shield_Refresh_Rate { get; set; }
        public bool Shield_Refresh_Rate_fromParent { get; set; }

        public int? Energy_Capacity { get; set; }
        public bool Energy_Capacity_fromParent { get; set; }

        public int? Energy_Refresh_Rate { get; set; }
        public bool Energy_Refresh_Rate_fromParent { get; set; }



        public int? Build_Cost_Credits { get; set; }
        public bool Build_Cost_Credits_fromParent { get; set; }

        public int? Piracy_Value_Credits { get; set; }
        public bool Piracy_Value_Credits_fromParent { get; set; }

        public int? Build_Time_Seconds { get; set; }
        public bool Build_Time_Seconds_fromParent { get; set; }



        public double? Space_FOW_Reveal_Range { get; set; }
        public bool Space_FOW_Reveal_Range_fromParent { get; set; }

        public double? Targeting_Max_Attack_Distance { get; set; }
        public bool Targeting_Max_Attack_Distance_fromParent { get; set; }



        public int? Score_Cost_Credits { get; set; }
        public bool Score_Cost_Credits_fromParent { get; set; }



        public int? Tactical_Build_Cost_Multiplayer { get; set; }
        public bool Tactical_Build_Cost_Multiplayer_fromParent { get; set; }

        public int? Tactical_Build_Time_Seconds { get; set; }
        public bool Tactical_Build_Time_Seconds_fromParent { get; set; }



        public Unit()
        {

        }

        public Unit(string name)
        {
            this.Name = name;
        }
    }
}
