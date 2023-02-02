#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VayneTumbleUltAttack : BBSpellScript
    {
        float[] effect0 = {0.4f, 0.45f, 0.5f, 0.55f, 0.6f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float critChance;
            float scalingDamage;
            float baseAttackDamage;
            float damageVar;
            critChance = GetFlatCritChanceMod(attacker);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            scalingDamage = this.effect0[level];
            baseAttackDamage = GetTotalAttackDamage(owner);
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                    hitResult = HitResult.HIT_Normal;
                    damageVar = baseAttackDamage;
                }
                else
                {
                    if(RandomChance() < critChance)
                    {
                        float critDamage;
                        hitResult = HitResult.HIT_Critical;
                        critDamage = GetFlatCritDamageMod(attacker);
                        critDamage += 2;
                        scalingDamage /= critDamage;
                    }
                    else
                    {
                        hitResult = HitResult.HIT_Normal;
                    }
                    scalingDamage++;
                    damageVar = baseAttackDamage * scalingDamage;
                }
            }
            else
            {
                hitResult = HitResult.HIT_Normal;
                damageVar = baseAttackDamage;
            }
            ApplyDamage(attacker, target, damageVar, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    Particle hi; // UNUSED
                    SpellEffectCreate(out hi, out _, "vayne_Q_tar.troy", default, TeamId.TEAM_NEUTRAL, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, owner.Position, target, default, default, true, default, default, false, false);
                    SpellBuffRemove(owner, nameof(Buffs.VayneTumbleBonus), (ObjAIBase)owner, 0);
                    SpellBuffRemove(owner, nameof(Buffs.VayneTumbleFade), (ObjAIBase)owner, 0);
                }
            }
        }
    }
}