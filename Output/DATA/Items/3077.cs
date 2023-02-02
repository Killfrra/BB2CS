#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3077 : BBItemScript
    {
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    float tempTable1_ThirdDA;
                    SpellEffectCreate(out _, out _, "TiamatMelee_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
                    if(IsRanged(owner))
                    {
                        tempTable1_ThirdDA = 0.35f * damageAmount;
                    }
                    else
                    {
                        if(GetBuffCountFromCaster(owner, default, nameof(Buffs.JudicatorRighteousFury)) > 0)
                        {
                            tempTable1_ThirdDA = 0.35f * damageAmount;
                        }
                        else
                        {
                            tempTable1_ThirdDA = 0.5f * damageAmount;
                        }
                    }
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 210, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        if(target != unit)
                        {
                            if(damageType == DamageType.DAMAGE_TYPE_MAGICAL)
                            {
                                ApplyDamage((ObjAIBase)owner, unit, tempTable1_ThirdDA, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, true, true, attacker);
                            }
                            else if(damageType == DamageType.DAMAGE_TYPE_PHYSICAL)
                            {
                                ApplyDamage((ObjAIBase)owner, unit, tempTable1_ThirdDA, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, true, true, attacker);
                            }
                            else
                            {
                                ApplyDamage((ObjAIBase)owner, unit, tempTable1_ThirdDA, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, true, true, attacker);
                            }
                        }
                    }
                }
            }
        }
    }
}