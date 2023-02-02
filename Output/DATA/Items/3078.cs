#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3078 : BBItemScript
    {
        int cooldownResevoir; // UNUSED
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(spellVars.DoesntTriggerSpellCasts)
            {
            }
            else
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SheenDelay)) == 0)
                {
                    float baseDamage;
                    float nextBuffVars_BaseDamage;
                    bool nextBuffVars_IsSheen;
                    baseDamage = GetBaseAttackDamage(owner);
                    nextBuffVars_BaseDamage = baseDamage;
                    nextBuffVars_IsSheen = false;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.Sheen(nextBuffVars_BaseDamage, nextBuffVars_IsSheen), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(target is ObjAIBase)
                    {
                        if(RandomChance() < 0.25f)
                        {
                            if(target is not BaseTurret)
                            {
                                AddBuff((ObjAIBase)target, target, new Buffs.Internal_35Slow(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                                AddBuff((ObjAIBase)owner, target, new Buffs.ItemSlow(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false);
                            }
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            this.cooldownResevoir = 0;
        }
    }
}