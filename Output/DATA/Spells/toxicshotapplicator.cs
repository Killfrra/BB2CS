#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Toxicshotapplicator : BBBuffScript
    {
        int[] effect0 = {6, 12, 18, 24, 30};
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int level;
            float nextBuffVars_DamagePerTick;
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(hitResult != HitResult.HIT_Miss)
                    {
                        if(hitResult != HitResult.HIT_Dodge)
                        {
                            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            nextBuffVars_DamagePerTick = this.effect0[level];
                            AddBuff((ObjAIBase)owner, target, new Buffs.ToxicShotParticle(nextBuffVars_DamagePerTick), 1, 1, 5.1f, BuffAddType.REPLACE_EXISTING, BuffType.POISON, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}