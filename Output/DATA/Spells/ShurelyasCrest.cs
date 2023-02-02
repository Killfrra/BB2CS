#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShurelyasCrest : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
        };
        public override void SelfExecute()
        {
            float nextBuffVars_MoveSpeedMod;
            Particle a; // UNUSED
            string name;
            string name1;
            string name2;
            string name3;
            string name4;
            string name5;
            SetSpell((ObjAIBase)owner, 7, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ShurelyasSpell));
            nextBuffVars_MoveSpeedMod = 0.4f;
            SpellEffectCreate(out a, out _, "ShurelyasCrest_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 700, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
            {
                SpellCast((ObjAIBase)owner, unit, unit.Position, unit.Position, 7, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 700, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyAssistMarker(attacker, unit, 10);
                AddBuff((ObjAIBase)owner, unit, new Buffs.Haste(nextBuffVars_MoveSpeedMod), 100, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true, false, false);
            }
            name = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name1 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name2 = GetSlotSpellName((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name3 = GetSlotSpellName((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name4 = GetSlotSpellName((ObjAIBase)owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            name5 = GetSlotSpellName((ObjAIBase)owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if(name == nameof(Spells.ShurelyasCrest))
            {
                SetSlotSpellCooldownTimeVer2(60, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name1 == nameof(Spells.ShurelyasCrest))
            {
                SetSlotSpellCooldownTimeVer2(60, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name2 == nameof(Spells.ShurelyasCrest))
            {
                SetSlotSpellCooldownTimeVer2(60, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name3 == nameof(Spells.ShurelyasCrest))
            {
                SetSlotSpellCooldownTimeVer2(60, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name4 == nameof(Spells.ShurelyasCrest))
            {
                SetSlotSpellCooldownTimeVer2(60, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            if(name5 == nameof(Spells.ShurelyasCrest))
            {
                SetSlotSpellCooldownTimeVer2(60, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
        }
    }
}
namespace Buffs
{
    public class ShurelyasCrest : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Spirit Visage",
            BuffTextureName = "3065_Spirit_Visage.dds",
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