#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptSwain : BBCharScript
    {
        float lastTimeExecuted;
        float[] effect0 = {1.08f, 1.11f, 1.14f, 1.17f, 1.2f};
        public override void OnUpdateActions()
        {
            float cooldownStat;
            float multiplier;
            float newCooldown;
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                cooldownStat = GetPercentCooldownMod(owner);
                multiplier = 1 + cooldownStat;
                newCooldown = multiplier * 1;
                SetSpellToolTipVar(newCooldown, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.SwainTacticalSupremacy(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float damagePercent;
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SwainTorment)) > 0)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level > 0)
                {
                    damagePercent = this.effect0[level];
                    damageAmount *= damagePercent;
                }
            }
        }
    }
}