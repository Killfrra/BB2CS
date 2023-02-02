#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class OdinRegenerationPotion : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            float curHealth;
            float maxHealth;
            float percentHealth;
            curHealth = GetHealth(target, PrimaryAbilityResourceType.MANA);
            maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            percentHealth = curHealth / maxHealth;
            if(percentHealth > 0.99f)
            {
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            string name;
            string name1;
            string name2;
            string name3;
            string name4;
            string name5;
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinRegenerationPotion(), 1, 1, 20, BuffAddType.RENEW_EXISTING, BuffType.HEAL, 0, true, false, false);
            name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if(name == nameof(Spells.OdinRegenerationPotion))
            {
                SetSlotSpellCooldownTimeVer2(60, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name1 == nameof(Spells.OdinRegenerationPotion))
            {
                SetSlotSpellCooldownTimeVer2(60, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name2 == nameof(Spells.OdinRegenerationPotion))
            {
                SetSlotSpellCooldownTimeVer2(60, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name3 == nameof(Spells.OdinRegenerationPotion))
            {
                SetSlotSpellCooldownTimeVer2(60, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name4 == nameof(Spells.OdinRegenerationPotion))
            {
                SetSlotSpellCooldownTimeVer2(60, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name5 == nameof(Spells.OdinRegenerationPotion))
            {
                SetSlotSpellCooldownTimeVer2(60, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
        }
    }
}
namespace Buffs
{
    public class OdinRegenerationPotion : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Regenerationpotion_itm.troy", },
            BuffName = "Health Potion",
            BuffTextureName = "2003_Regeneration_Potion.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            float curHealth;
            float maxHealth;
            float percentHealth; // UNUSED
            curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            percentHealth = curHealth / maxHealth;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                IncHealth(owner, 10, owner);
            }
        }
    }
}