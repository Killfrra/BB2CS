#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class HeimerTBlueBasicAttack : BBSpellScript
    {
        float[] effect0 = {-0.2f, -0.25f, -0.3f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float dmg;
            teamID = GetTeamID(owner);
            attacker = GetChampionBySkinName("Heimerdinger", teamID ?? TeamId.TEAM_UNKNOWN);
            dmg = GetTotalAttackDamage(owner);
            if(target is BaseTurret)
            {
                dmg /= 2;
            }
            ApplyDamage(attacker, target, dmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, (ObjAIBase)owner);
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    float nextBuffVars_MovementSpeedMod;
                    float nextBuffVars_AttackSpeedMod;
                    level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    nextBuffVars_MovementSpeedMod = this.effect0[level];
                    nextBuffVars_AttackSpeedMod = 0;
                    AddBuff(attacker, target, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
        }
    }
}