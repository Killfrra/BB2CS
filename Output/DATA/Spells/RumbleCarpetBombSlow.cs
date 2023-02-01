#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RumbleCarpetBombSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "", "", },
            AutoBuffActivateEffect = new[]{ "Global_Slow.troy", "", "", },
            BuffName = "RumbleCarpetBombSlow",
            BuffTextureName = "GragasBodySlam.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float slowAmount;
        float[] effect0 = {-0.35f, -0.35f, -0.35f};
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            int level;
            teamID = GetTeamID(attacker);
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.slowAmount = this.effect0[level];
            ApplyAssistMarker(attacker, attacker, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.slowAmount);
        }
    }
}