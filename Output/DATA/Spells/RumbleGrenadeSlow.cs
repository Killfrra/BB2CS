#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RumbleGrenadeSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "", "", },
            AutoBuffActivateEffect = new[]{ "Global_Slow.troy", "", "", },
            BuffName = "RumbleGrenadeSlow",
            BuffTextureName = "Rumble_Electro Harpoon.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float slowAmount;
        public RumbleGrenadeSlow(float slowAmount = default)
        {
            this.slowAmount = slowAmount;
        }
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            int level; // UNUSED
            //RequireVar(this.slowAmount);
            teamID = GetTeamID(attacker);
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            ApplyAssistMarker(attacker, attacker, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.slowAmount);
        }
    }
}