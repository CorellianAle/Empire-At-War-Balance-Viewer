using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balancer.Model
{
    public class Unit
    {
        public UnitFile UnitFile { get; set; }
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



        public string ArmorTypeName { get; set; }
        public ArmorType Armor_Type { get; set; }
        public bool Armor_Type_fromParent { get; set; }

        public string ShieldArmorTypeName { get; set; }
        public ShieldArmorType Shield_Armor_Type { get; set; }
        public bool Shield_Armor_Type_fromParent { get; set; }

        public string DamageTypeName { get; set; }
        public DamageType Damage_Type { get; set; }
        public bool Damage_Type_fromParent { get; set; }

        public string ProjectileTypesName { get; set; }
        public Projectile Projectile_Types { get; set; }
        public bool Projectile_Types_fromParent { get; set; }

        public double? Projectile_Fire_Pulse_Count { get; set; }
        public bool Projectile_Fire_Pulse_Count_fromParent { get; set; }

        public double? Projectile_Fire_Pulse_Delay_Seconds { get; set; }
        public bool Projectile_Fire_Pulse_Delay_Seconds_fromParent { get; set; }

        public double? Projectile_Fire_Recharge_Seconds { get; set; }
        public bool Projectile_Fire_Recharge_Seconds_fromParent { get; set; }


        public List<string> HardpointNames { get; set; }
        public List<Hardpoint> Hardpoints { get; set; }
        public bool Hardpoints_fromParent { get; set; }



        public Unit()
        {
            Hardpoints = new List<Hardpoint>();
        }

        public Unit(string name)
        {
            this.Name = name;
            Hardpoints = new List<Hardpoint>();
        }

        private Unit(Unit unit)
        {
            UnitFile = unit.UnitFile;
            Parent = unit.Parent;

            Name = unit.Name;

            Variant_Of_Existing_Type = unit.Variant_Of_Existing_Type;

            AI_Combat_Power = unit.AI_Combat_Power;
            AI_Combat_Power_fromParent = unit.AI_Combat_Power_fromParent;

            Damage = unit.Damage;
            Damage_fromParent = unit.Damage_fromParent;

            Autoresolve_Health = unit.Autoresolve_Health;
            Autoresolve_Health_fromParent = unit.Autoresolve_Health_fromParent;

            Shield_Points = unit.Shield_Points;
            Shield_Points_fromParent = unit.Shield_Points_fromParent;

            Tactical_Health = unit.Tactical_Health;
            Tactical_Health_fromParent = unit.Tactical_Health_fromParent;

            Shield_Refresh_Rate = unit.Shield_Refresh_Rate;
            Shield_Refresh_Rate_fromParent = unit.Shield_Refresh_Rate_fromParent;

            Energy_Capacity = unit.Energy_Capacity;
            Energy_Capacity_fromParent = unit.Energy_Capacity_fromParent;

            Energy_Refresh_Rate = unit.Energy_Refresh_Rate;
            Energy_Refresh_Rate_fromParent = unit.Energy_Refresh_Rate_fromParent;

            Build_Cost_Credits = unit.Build_Cost_Credits;
            Build_Cost_Credits_fromParent = unit.Build_Cost_Credits_fromParent;

            Piracy_Value_Credits = unit.Piracy_Value_Credits;
            Piracy_Value_Credits_fromParent = unit.Piracy_Value_Credits_fromParent;

            Build_Time_Seconds = unit.Build_Time_Seconds;
            Build_Time_Seconds_fromParent = unit.Build_Time_Seconds_fromParent;

            Space_FOW_Reveal_Range = unit.Space_FOW_Reveal_Range;
            Space_FOW_Reveal_Range_fromParent = unit.Space_FOW_Reveal_Range_fromParent;

            Targeting_Max_Attack_Distance = unit.Targeting_Max_Attack_Distance;
            Targeting_Max_Attack_Distance_fromParent = unit.Targeting_Max_Attack_Distance_fromParent;

            Score_Cost_Credits = unit.Score_Cost_Credits;
            Score_Cost_Credits_fromParent = unit.Score_Cost_Credits_fromParent;

            Tactical_Build_Cost_Multiplayer = unit.Tactical_Build_Cost_Multiplayer;
            Tactical_Build_Cost_Multiplayer_fromParent = unit.Tactical_Build_Cost_Multiplayer_fromParent;

            Tactical_Build_Time_Seconds = unit.Tactical_Build_Time_Seconds;
            Tactical_Build_Time_Seconds_fromParent = unit.Tactical_Build_Time_Seconds_fromParent;

            //
            HardpointNames = new List<string>();

            foreach (var hardpointName in unit.HardpointNames)
            {
                HardpointNames.Add(hardpointName);
            }

            //
            Hardpoints = new List<Hardpoint>();

            foreach (var hardpoint in unit.Hardpoints)
            {
                Hardpoints.Add(hardpoint);
            }

            Hardpoints_fromParent = unit.Hardpoints_fromParent;

            ArmorTypeName = unit.ArmorTypeName;
            Armor_Type = unit.Armor_Type;
            Armor_Type_fromParent = unit.Armor_Type_fromParent;

            ShieldArmorTypeName = unit.ShieldArmorTypeName;
            Shield_Armor_Type = unit.Shield_Armor_Type;
            Shield_Armor_Type_fromParent = unit.Shield_Armor_Type_fromParent;

            DamageTypeName = unit.DamageTypeName;
            Damage_Type = unit.Damage_Type;
            Damage_Type_fromParent = unit.Damage_Type_fromParent;

            ProjectileTypesName = unit.ProjectileTypesName;
            Projectile_Types = unit.Projectile_Types;
            Projectile_Types_fromParent = unit.Projectile_Types_fromParent;

            Projectile_Fire_Pulse_Count = unit.Projectile_Fire_Pulse_Count;
            Projectile_Fire_Pulse_Count_fromParent = unit.Projectile_Fire_Pulse_Count_fromParent;

            Projectile_Fire_Pulse_Delay_Seconds = unit.Projectile_Fire_Pulse_Delay_Seconds;
            Projectile_Fire_Pulse_Delay_Seconds_fromParent = unit.Projectile_Fire_Pulse_Delay_Seconds_fromParent;

            Projectile_Fire_Recharge_Seconds = unit.Projectile_Fire_Recharge_Seconds;
            Projectile_Fire_Recharge_Seconds_fromParent = unit.Projectile_Fire_Recharge_Seconds_fromParent;
        }


        public override string ToString()
        {
            if (Name != null)
            {
                return Name;
            }

            return base.ToString();
        }

        public double? CalculateTotalHardpointsHealth()
        {
            if (Hardpoints == null || Hardpoints.Count == 0)
            {
                return null;
            }

            double total = 0;

            foreach (var hardpoint in Hardpoints)
            {
                if (hardpoint == null)
                {
                    continue;
                }

                if (!hardpoint.Is_Destroyable.HasValue || !hardpoint.Is_Destroyable.Value)
                {
                    continue;
                }

                if (!hardpoint.Health.HasValue)
                {
                    continue;
                }

                total += hardpoint.Health.Value;
            }

            return total;
        }

        /// <summary>
        /// Total initial damage of all hardpoints of type HARD_POINT_WEAPON_LASER.
        /// </summary>
        /// <returns></returns>
        public double? CalculateInitialAttackDamageLaser()
        {
            return CalculateInitialAttackDamage("HARD_POINT_WEAPON_LASER");
        }

        /// <summary>
        /// Total initial damage of all hardpoints of type HARD_POINT_WEAPON_MISSILE.
        /// </summary>
        /// <returns></returns>
        public double? CalculateInitialAttackDamageMissle()
        {
            return CalculateInitialAttackDamage("HARD_POINT_WEAPON_MISSILE");
        }

        /// <summary>
        /// Total initial damage of all hardpoints of type HARD_POINT_WEAPON_TORPEDO.
        /// </summary>
        /// <returns></returns>
        public double? CalculateInitialAttackDamageTorpedo()
        {
            return CalculateInitialAttackDamage("HARD_POINT_WEAPON_TORPEDO");
        }

        /// <summary>
        /// Total initial damage of all hardpoints of type HARD_POINT_WEAPON_ION_CANNON.
        /// </summary>
        /// <returns></returns>
        public double? CalculateInitialAttackDamageIon()
        {
            return CalculateInitialAttackDamage("HARD_POINT_WEAPON_ION_CANNON");
        }

        /// <summary>
        /// Projectile damage multiplied by pulse count. Counts only HARD_POINT_WEAPON_LASER.
        /// </summary>
        /// <returns></returns>
        private double? CalculateInitialAttackDamage(string hardpointType)
        {
            if (Hardpoints == null || Hardpoints.Count == 0)
            {
                return null;
            }

            double damage = 0;

            foreach (var hardpoint in Hardpoints)
            {
                if (!hardpointType.Equals(hardpoint.Type))
                {
                    continue;
                }

                var initialAttackDamage = hardpoint.CalculateInitialAttackDamage();

                if (initialAttackDamage != null)
                {
                    damage += initialAttackDamage.Value;
                }
            }

            return Math.Round(damage, 2);
        }




        /// <summary>
        /// Total damage over 10 seconds of all hardpoints of type HARD_POINT_WEAPON_LASER.
        /// </summary>
        /// <returns></returns>
        public double? Calculate10SecAttackDamageLaser()
        {
            return CalculateXSecAttackDamage("HARD_POINT_WEAPON_LASER", 10);
        }

        /// <summary>
        /// Total damage over 10 seconds of all hardpoints of type HARD_POINT_WEAPON_MISSILE.
        /// </summary>
        /// <returns></returns>
        public double? Calculate10SecAttackDamageMissle()
        {
            return CalculateXSecAttackDamage("HARD_POINT_WEAPON_MISSILE", 10);
        }

        /// <summary>
        /// Total damage over 10 seconds of all hardpoints of type HARD_POINT_WEAPON_TORPEDO.
        /// </summary>
        /// <returns></returns>
        public double? Calculate10SecAttackDamageTorpedo()
        {
            return CalculateXSecAttackDamage("HARD_POINT_WEAPON_TORPEDO", 10);
        }

        /// <summary>
        /// Total damage over 10 seconds of all hardpoints of type HARD_POINT_WEAPON_ION_CANNON.
        /// </summary>
        /// <returns></returns>
        public double? Calculate10SecAttackDamageIon()
        {
            return CalculateXSecAttackDamage("HARD_POINT_WEAPON_ION_CANNON", 10);
        }





        /// <summary>
        /// Total damage over 30 seconds of all hardpoints of type HARD_POINT_WEAPON_LASER.
        /// </summary>
        /// <returns></returns>
        public double? Calculate30SecAttackDamageLaser()
        {
            return CalculateXSecAttackDamage("HARD_POINT_WEAPON_LASER", 30);
        }

        /// <summary>
        /// Total damage over 30 seconds of all hardpoints of type HARD_POINT_WEAPON_MISSILE.
        /// </summary>
        /// <returns></returns>
        public double? Calculate30SecAttackDamageMissle()
        {
            return CalculateXSecAttackDamage("HARD_POINT_WEAPON_MISSILE", 30);
        }

        /// <summary>
        /// Total damage over 30 seconds of all hardpoints of type HARD_POINT_WEAPON_TORPEDO.
        /// </summary>
        /// <returns></returns>
        public double? Calculate30SecAttackDamageTorpedo()
        {
            return CalculateXSecAttackDamage("HARD_POINT_WEAPON_TORPEDO", 30);
        }

        /// <summary>
        /// Total damage over 30 seconds of all hardpoints of type HARD_POINT_WEAPON_ION_CANNON.
        /// </summary>
        /// <returns></returns>
        public double? Calculate30SecAttackDamageIon()
        {
            return CalculateXSecAttackDamage("HARD_POINT_WEAPON_ION_CANNON", 30);
        }





        /// <summary>
        /// Total damage over 60 seconds of all hardpoints of type HARD_POINT_WEAPON_LASER.
        /// </summary>
        /// <returns></returns>
        public double? Calculate60SecAttackDamageLaser()
        {
            return CalculateXSecAttackDamage("HARD_POINT_WEAPON_LASER", 60);
        }

        /// <summary>
        /// Total damage over 60 seconds of all hardpoints of type HARD_POINT_WEAPON_MISSILE.
        /// </summary>
        /// <returns></returns>
        public double? Calculate60SecAttackDamageMissle()
        {
            return CalculateXSecAttackDamage("HARD_POINT_WEAPON_MISSILE", 60);
        }

        /// <summary>
        /// Total damage over 60 seconds of all hardpoints of type HARD_POINT_WEAPON_TORPEDO.
        /// </summary>
        /// <returns></returns>
        public double? Calculate60SecAttackDamageTorpedo()
        {
            return CalculateXSecAttackDamage("HARD_POINT_WEAPON_TORPEDO", 60);
        }

        /// <summary>
        /// Total damage over 60 seconds of all hardpoints of type HARD_POINT_WEAPON_ION_CANNON.
        /// </summary>
        /// <returns></returns>
        public double? Calculate60SecAttackDamageIon()
        {
            return CalculateXSecAttackDamage("HARD_POINT_WEAPON_ION_CANNON", 60);
        }

        /// <summary>
        /// Total damage over X seconds (including fire delays and recharge time) from a hardpoint of specified type.
        /// </summary>
        /// <returns></returns>
        private double? CalculateXSecAttackDamage(string hardpointType, int seconds)
        {
            if (Hardpoints == null || Hardpoints.Count == 0)
            {
                return null;
            }

            double damage = 0;

            foreach (var hardpoint in Hardpoints)
            {
                if (!hardpointType.Equals(hardpoint.Type))
                {
                    continue;
                }

                var tenSecAttackDamage = hardpoint.CalculateXSecondsAttackDamage(seconds);

                if (tenSecAttackDamage != null)
                {
                    damage += tenSecAttackDamage.Value;
                }
            }

            return Math.Round(damage, 2);
        }
    }
}
