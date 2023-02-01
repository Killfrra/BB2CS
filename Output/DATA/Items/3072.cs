#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3072 : BBItemScript
    {
        float physicalDamageBonus;
        float percentLifeSteal;
        public override void OnUpdateStats()
        {
            float percentLifeStealTT;
            IncFlatPhysicalDamageMod(owner, this.physicalDamageBonus);
            IncPercentLifeStealMod(owner, this.percentLifeSteal);
            SetSpellToolTipVar(this.physicalDamageBonus, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            percentLifeStealTT = this.percentLifeSteal * 100;
            SetSpellToolTipVar(percentLifeStealTT, 2, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
        public override void OnKill()
        {
            this.physicalDamageBonus++;
            this.percentLifeSteal += 0.0025f;
            this.physicalDamageBonus = Math.Min(this.physicalDamageBonus, 40);
            this.percentLifeSteal = Math.Min(this.percentLifeSteal, 0.1f);
        }
        public override void OnDeath()
        {
            this.physicalDamageBonus = 0;
            this.percentLifeSteal = 0;
        }
        public override void OnActivate()
        {
            this.physicalDamageBonus = 0;
            this.percentLifeSteal = 0;
        }
    }
}