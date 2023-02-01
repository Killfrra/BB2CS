#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MordekaiserCreepingDeathCast : BBSpellScript
    {
        int[] effect0 = {26, 32, 38, 44, 50};
        int[] effect1 = {24, 38, 52, 66, 80};
        int[] effect2 = {10, 15, 20, 25, 30};
        public override void SelfExecute()
        {
            float healthCost;
            float temp1;
            healthCost = this.effect0[level];
            temp1 = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if(healthCost >= temp1)
            {
                healthCost = temp1 - 1;
            }
            healthCost *= -1;
            IncHealth(owner, healthCost, owner);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nextBuffVars_DamagePerTick;
            float nextBuffVars_DefenseStats;
            AddBuff((ObjAIBase)owner, target, new Buffs.MordekaiserCreepingDeathCheck(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(target == owner)
            {
                nextBuffVars_DamagePerTick = this.effect1[level];
                nextBuffVars_DefenseStats = this.effect2[level];
                AddBuff((ObjAIBase)owner, target, new Buffs.MordekaiserCreepingDeath(nextBuffVars_DamagePerTick, nextBuffVars_DefenseStats), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            else
            {
                SpellCast((ObjAIBase)owner, target, default, default, 1, SpellSlotType.ExtraSlots, level, true, false, false, true, false, false);
            }
        }
    }
}