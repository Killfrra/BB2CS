#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DrawABead : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Draw a Bead",
            BuffTextureName = "Tristana_DrawABead.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTooltip;
        float lastTimeExecuted;
        int[] effect0 = {0, 9, 18, 27, 36, 45, 54, 63, 72, 81, 90, 99, 108, 117, 126, 135, 144, 153, 162};
        public override void OnActivate()
        {
            this.lastTooltip = 0;
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, true))
            {
                int level;
                float tooltipAmount;
                level = GetLevel(owner);
                tooltipAmount = this.effect0[level];
                if(tooltipAmount > this.lastTooltip)
                {
                    this.lastTooltip = tooltipAmount;
                    SetBuffToolTipVar(1, tooltipAmount);
                }
            }
        }
        public override void OnKill()
        {
            if(target is Champion)
            {
                int level;
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level >= 1)
                {
                    float cooldown;
                    cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(cooldown > 0)
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    }
                }
            }
        }
        public override void OnAssist()
        {
            if(target is Champion)
            {
                int level;
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level >= 1)
                {
                    float cooldown;
                    cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(cooldown > 0)
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    }
                }
            }
        }
    }
}