#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3083 : BBItemScript
    {
        float extraHP;
        float extraRegen;
        public override void OnUpdateStats()
        {
            float extraRegenTT;
            IncFlatHPPoolMod(owner, this.extraHP);
            IncFlatHPRegenMod(owner, this.extraRegen);
            SetSpellToolTipVar(this.extraHP, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
            extraRegenTT = this.extraRegen * 5;
            SetSpellToolTipVar(extraRegenTT, 2, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)attacker);
        }
        public override void OnKill()
        {
            if(target is Champion)
            {
                this.extraHP += 35;
                this.extraRegen += 0.2f;
                this.extraHP = Math.Min(this.extraHP, 350);
                this.extraRegen = Math.Min(this.extraRegen, 2);
            }
            else
            {
                this.extraHP += 3.5f;
                this.extraRegen += 0.02f;
                this.extraHP = Math.Min(this.extraHP, 350);
                this.extraRegen = Math.Min(this.extraRegen, 2);
            }
        }
        public override void OnAssist()
        {
            if(target is Champion)
            {
                this.extraHP += 35;
                this.extraRegen += 0.2f;
                this.extraHP = Math.Min(this.extraHP, 350);
                this.extraRegen = Math.Min(this.extraRegen, 2);
            }
        }
        public override void OnActivate()
        {
            this.extraHP = 0;
            this.extraRegen = 0;
        }
    }
}