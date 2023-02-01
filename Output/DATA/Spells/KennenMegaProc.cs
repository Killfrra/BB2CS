#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KennenMegaProc : BBSpellScript
    {
        float[] effect0 = {0.4f, 0.5f, 0.6f, 0.7f, 0.8f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float damageMod;
            float attackDamage;
            float procDamage;
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(target is ObjAIBase)
                    {
                        if(target is not BaseTurret)
                        {
                            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.KennenDoubleStrikeLive)) > 0)
                            {
                                AddBuff((ObjAIBase)owner, target, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                                damageMod = this.effect0[level];
                                attackDamage = GetTotalAttackDamage(owner);
                                procDamage = attackDamage * damageMod;
                                ApplyDamage(attacker, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                                ApplyDamage(attacker, target, procDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                SpellBuffRemove(owner, nameof(Buffs.KennenDoubleStrikeLive), (ObjAIBase)owner);
                                RemoveOverrideAutoAttack(owner, true);
                                charVars.Count = 0;
                            }
                            else
                            {
                                attackDamage = GetTotalAttackDamage(owner);
                                ApplyDamage(attacker, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                            }
                        }
                        else
                        {
                            attackDamage = GetTotalAttackDamage(owner);
                            ApplyDamage(attacker, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                        }
                    }
                    else
                    {
                        attackDamage = GetTotalAttackDamage(owner);
                        ApplyDamage(attacker, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                    }
                }
            }
        }
    }
}