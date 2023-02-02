#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class FizzMarinerDoomBoom : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            SpellBuffClear(owner, nameof(Buffs.FizzMarinerDoomMissile));
            foreach(Champion unit in GetChampions(TeamId.TEAM_UNKNOWN, default, true))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.FizzMarinerDoomBomb)) > 0)
                {
                    SpellBuffClear(unit, nameof(Buffs.FizzMarinerDoomBomb));
                }
            }
        }
    }
}
namespace Buffs
{
    public class FizzMarinerDoomBoom : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "",
            BuffTextureName = "",
            PopupMessage = new[]{ "game_floatingtext_Snared", },
            SpellToggleSlot = 4,
        };
        int[] effect0 = {40, 35, 30};
        public override void OnActivate()
        {
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.FizzMarinerDoomBoom));
        }
        public override void OnDeactivate(bool expired)
        {
            float cDReduction;
            int level;
            float baseCD;
            float lowerCD;
            float newCD;
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.FizzMarinerDoom));
            cDReduction = GetPercentCooldownMod(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseCD = this.effect0[level];
            lowerCD = baseCD * cDReduction;
            newCD = baseCD + lowerCD;
            SetSlotSpellCooldownTimeVer2(newCD, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}