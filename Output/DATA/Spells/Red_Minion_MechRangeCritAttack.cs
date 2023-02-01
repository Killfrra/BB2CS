#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Red_Minion_MechRangeCritAttack : BBSpellScript
    {
        int[] effect0 = {40, 55, 70, 85, 100};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float dmg;
            float abilityPower;
            float abilityPowerBonus;
            float totalDmg;
            teamID = GetTeamID(owner);
            attacker = GetChampionBySkinName("Jester", teamID);
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            dmg = this.effect0[level];
            abilityPower = GetFlatMagicDamageMod(attacker);
            abilityPowerBonus = abilityPower * 0.35f;
            totalDmg = dmg + abilityPowerBonus;
            if(target is not Champion)
            {
                totalDmg *= 0.5f;
            }
            ApplyDamage(attacker, target, totalDmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0);
        }
    }
}