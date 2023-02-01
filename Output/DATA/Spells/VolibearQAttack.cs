#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VolibearQAttack : BBSpellScript
    {
        int[] effect0 = {30, 60, 90, 120, 150};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float critChance;
            float bonusDamage;
            float baseAttackDamage;
            float critDamage;
            float damageVar;
            TeamId teamID;
            Particle kennenss; // UNUSED
            Vector3 nextBuffVars_BouncePos;
            critChance = GetFlatCritChanceMod(attacker);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            bonusDamage = this.effect0[level];
            baseAttackDamage = GetTotalAttackDamage(owner);
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(RandomChance() < critChance)
                    {
                        hitResult = HitResult.HIT_Critical;
                        critDamage = GetFlatCritDamageMod(attacker);
                        critDamage += 2;
                        bonusDamage /= critDamage;
                    }
                    else
                    {
                        hitResult = HitResult.HIT_Normal;
                    }
                }
                else
                {
                    hitResult = HitResult.HIT_Normal;
                }
            }
            else
            {
                hitResult = HitResult.HIT_Normal;
            }
            damageVar = baseAttackDamage + bonusDamage;
            ApplyDamage(attacker, target, damageVar, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            SpellBuffRemove(owner, nameof(Buffs.VolibearQ), (ObjAIBase)owner, 0);
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    BreakSpellShields(target);
                    teamID = GetTeamID(attacker);
                    SpellEffectCreate(out kennenss, out _, "Volibear_Q_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, target, "C_BUFFBONE_GLB_CENTER_LOC", default, target, default, default, true, false, false, false, false);
                    nextBuffVars_BouncePos = charVars.BouncePos;
                    AddBuff(attacker, target, new Buffs.VolibearQExtra(nextBuffVars_BouncePos), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, true);
                }
            }
        }
    }
}