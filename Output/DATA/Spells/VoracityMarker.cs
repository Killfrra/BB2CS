#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VoracityMarker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = true,
        };
        public override void OnActivate()
        {
            if(owner is Champion)
            {
            }
            else
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnDeath()
        {
            Particle placeholder; // UNUSED
            float dLCooldown;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes))
            {
                if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.Voracity)) > 0)
                {
                    SpellEffectCreate(out placeholder, out _, "katarina_spell_refresh_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, false);
                    IncGold(unit, 50);
                    SetSlotSpellCooldownTime((ObjAIBase)unit, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    SetSlotSpellCooldownTime((ObjAIBase)unit, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    SetSlotSpellCooldownTime((ObjAIBase)unit, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    dLCooldown = GetSlotSpellCooldownTime((ObjAIBase)unit, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(dLCooldown > 15)
                    {
                        dLCooldown -= 15;
                        SetSlotSpellCooldownTime((ObjAIBase)unit, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, dLCooldown);
                    }
                    else
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)unit, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    }
                }
            }
        }
    }
}