#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class PantheonBasicAttack2 : BBSpellScript
    {
        float[] effect0 = {0.15f, 0.15f, 0.15f, 0.15f, 0.15f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_CertainDeath)) > 0)
                    {
                        if(target is ObjAIBase)
                        {
                            if(target is not BaseTurret)
                            {
                                float tarHP;
                                float hpThreshold;
                                tarHP = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                                hpThreshold = this.effect0[level];
                                if(tarHP <= hpThreshold)
                                {
                                    hitResult = HitResult.HIT_Critical;
                                }
                            }
                        }
                    }
                }
            }
            baseDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 1, 1, false, false, attacker);
        }
    }
}