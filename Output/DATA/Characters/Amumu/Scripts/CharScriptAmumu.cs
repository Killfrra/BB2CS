#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptAmumu : BBCharScript
    {
        int[] effect0 = {-15, -15, -15, -15, -15, -15, -25, -25, -25, -25, -25, -25, -35, -35, -35, -35, -35, -35};
        public override void OnUpdateActions()
        {
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level >= 1)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Tantrum(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
        public override void SetVarsByLevel()
        {
            charVars.MagicResistReduction = this.effect0[level];
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float nextBuffVars_MagicResistReduction;
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(target is ObjAIBase)
                    {
                        if(target is BaseTurret)
                        {
                        }
                        else
                        {
                            nextBuffVars_MagicResistReduction = charVars.MagicResistReduction;
                            AddBuff(attacker, target, new Buffs.CursedTouch(nextBuffVars_MagicResistReduction), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.CursedTouchMarker(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}