#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShyvanaPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ShyvanaPassive",
            BuffTextureName = "ShyvanaReinforcedScales.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        float[] effect0 = {0.8f, 0.85f, 0.9f, 0.95f, 1};
        public override void OnUpdateActions()
        {
            float totalAttackDamage;
            int level;
            float damagePercent;
            float damageToDisplay;
            float bonusAD;
            float bonusAD20;
            if(ExecutePeriodically(5, ref this.lastTimeExecuted, true))
            {
                totalAttackDamage = GetTotalAttackDamage(owner);
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level > 0)
                {
                    damagePercent = this.effect0[level];
                }
                else
                {
                    damagePercent = 0.8f;
                }
                damageToDisplay = totalAttackDamage * damagePercent;
                SetSpellToolTipVar(damageToDisplay, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                bonusAD = GetFlatPhysicalDamageMod(owner);
                bonusAD20 = bonusAD * 0.2f;
                SetSpellToolTipVar(bonusAD20, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
    }
}