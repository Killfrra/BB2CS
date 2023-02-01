#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ArcaneMastery : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Arcane Mastery",
            BuffTextureName = "Ryze_SpellStrike.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            int slot;
            float cooldown;
            float newCooldown;
            if(!spellVars.DoesntTriggerSpellCasts)
            {
                slot = GetSpellSlot();
                if(slot != 0)
                {
                    cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(cooldown > 0)
                    {
                        newCooldown = cooldown - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
                    }
                }
                if(slot != 1)
                {
                    cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(cooldown > 0)
                    {
                        newCooldown = cooldown - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
                    }
                }
                if(slot != 2)
                {
                    cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(cooldown > 0)
                    {
                        newCooldown = cooldown - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
                    }
                }
                if(slot != 3)
                {
                    cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(cooldown > 0)
                    {
                        newCooldown = cooldown - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
                    }
                }
            }
        }
    }
}