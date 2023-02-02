#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class EnhancedRegen : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Spell Shield Regen",
            BuffTextureName = "Sivir_SpellBlock.dds",
        };
        float manaRegenBonus;
        float[] effect0 = {0.4f, 0.8f, 1.2f, 1.6f, 2};
        public override void OnActivate()
        {
            //RequireVar(this.manaRegenBonus);
        }
        public override void OnUpdateStats()
        {
            IncFlatPARRegenMod(owner, this.manaRegenBonus, PrimaryAbilityResourceType.MANA);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if(slot == 2)
            {
                int level;
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                this.manaRegenBonus = this.effect0[level];
            }
        }
    }
}