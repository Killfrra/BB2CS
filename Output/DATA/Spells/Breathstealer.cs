#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Breathstealer : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            string name;
            string name1;
            string name2;
            string name3;
            string name4;
            string name5;
            Vector3 targetPos;
            name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if(name == nameof(Spells.Breathstealer))
            {
                SetSlotSpellCooldownTimeVer2(90, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name1 == nameof(Spells.Breathstealer))
            {
                SetSlotSpellCooldownTimeVer2(90, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name2 == nameof(Spells.Breathstealer))
            {
                SetSlotSpellCooldownTimeVer2(90, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name3 == nameof(Spells.Breathstealer))
            {
                SetSlotSpellCooldownTimeVer2(90, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name4 == nameof(Spells.Breathstealer))
            {
                SetSlotSpellCooldownTimeVer2(90, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            if(name5 == nameof(Spells.Breathstealer))
            {
                SetSlotSpellCooldownTimeVer2(90, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
            SetSpell((ObjAIBase)owner, 7, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.BreathstealerSpell));
            targetPos = GetUnitPosition(target);
            FaceDirection(owner, targetPos);
            SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 7, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class Breathstealer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, null, "head", },
            AutoBuffActivateEffect = new[]{ "Summoner_Banish.troy", null, "Global_miss.troy", },
            BuffName = "",
            BuffTextureName = "",
        };
        public override void OnActivate()
        {
            IncPermanentPercentCooldownMod(owner, -0.15f);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentPercentCooldownMod(owner, 0.15f);
        }
    }
}