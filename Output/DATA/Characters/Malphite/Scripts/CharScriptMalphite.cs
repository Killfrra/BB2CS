#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptMalphite : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MalphiteShieldBeenHit)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.MalphiteShieldEffect(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                float armorVal;
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level > 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MalphiteCleave(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
                armorVal = GetArmor(owner);
                armorVal *= 0.5f;
                SetSpellToolTipVar(armorVal, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.MalphiteShieldBeenHit(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.MalphiteShield(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}