#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Visionary : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Visionary_buf.troy", },
            BuffName = "Visions",
            BuffTextureName = "Yeti_FrostNova.dds",
            NonDispellable = true,
            SpellVOOverrideSkins = new[]{ "NunuBot", },
        };
        int[] effect0 = {-75, -85, -95, -105, -115};
        int[] effect1 = {-150, -225, -300};
        public override void OnActivate()
        {
            int levelZero;
            int levelOne;
            int level;
            float spellTwoMana;
            int levelTwo;
            float spellThreeMana;
            int levelThree;
            levelZero = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            levelOne = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            spellTwoMana = this.effect0[level];
            levelTwo = level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            spellThreeMana = this.effect1[level];
            levelThree = level;
            if(levelZero > 0)
            {
                SetPARCostInc((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, -80, PrimaryAbilityResourceType.MANA);
            }
            if(levelOne > 0)
            {
                SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, -75, PrimaryAbilityResourceType.MANA);
            }
            if(levelTwo > 0)
            {
                SetPARCostInc((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, spellTwoMana, PrimaryAbilityResourceType.MANA);
            }
            if(levelThree > 0)
            {
                SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, spellThreeMana, PrimaryAbilityResourceType.MANA);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetPARCostInc((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARCostInc((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(spellVars.DoesntTriggerSpellCasts)
            {
            }
            else
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}