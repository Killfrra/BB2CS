#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KarmaChakra : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "karma_matraCharge_self.troy", },
            BuffName = "KarmaMantraEnergized",
            BuffTextureName = "KarmaMantraActivate.dds",
            SpellToggleSlot = 4,
        };
        public override void OnActivate()
        {
            float cooldown;
            float cooldown2;
            float cooldown3;
            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KarmaHeavenlyWaveC));
            SetSlotSpellCooldownTimeVer2(cooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KarmaSpiritBondC));
            SetSlotSpellCooldownTimeVer2(cooldown2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cooldown3 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KarmaSoulShieldC));
            SetSlotSpellCooldownTimeVer2(cooldown3, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            int count;
            float cooldown;
            float cooldown2;
            float cooldown3;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.KarmaChakra));
            if(count == 0)
            {
                cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KarmaHeavenlyWave));
                SetSlotSpellCooldownTimeVer2(cooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                cooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KarmaSpiritBond));
                SetSlotSpellCooldownTimeVer2(cooldown2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                cooldown3 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KarmaSoulShield));
                SetSlotSpellCooldownTimeVer2(cooldown3, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
        }
    }
}
namespace Spells
{
    public class KarmaChakra : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            int count;
            count = GetBuffCountFromAll(owner, nameof(Buffs.KarmaChakraCharge));
            if(count > 1)
            {
                returnValue = true;
            }
            else
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            int count;
            float remainingDuration;
            count = GetBuffCountFromAll(owner, nameof(Buffs.KarmaChakraCharge));
            if(count > 2)
            {
                SpellBuffRemove(owner, nameof(Buffs.KarmaChakraCharge), (ObjAIBase)owner, charVars.MantraTimerCooldown);
                SpellBuffRemove(owner, nameof(Buffs.KarmaTwoMantraParticle), (ObjAIBase)owner, 0);
                AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaOneMantraParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.KarmaOneMantraParticle), (ObjAIBase)owner, 0);
                SpellBuffRemove(owner, nameof(Buffs.KarmaChakraCharge), (ObjAIBase)owner, 0);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaChakra(), 2, 1, 8, BuffAddType.STACKS_AND_OVERLAPS, BuffType.COMBAT_ENCHANCER, 0, false, false, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.KarmaChakraCharge)) > 0)
            {
                SetSlotSpellCooldownTimeVer2(0, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
            }
            else
            {
                remainingDuration = GetBuffRemainingDuration(owner, nameof(Buffs.KarmaChakraTimer));
                SetSlotSpellCooldownTimeVer2(remainingDuration, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
            }
        }
    }
}