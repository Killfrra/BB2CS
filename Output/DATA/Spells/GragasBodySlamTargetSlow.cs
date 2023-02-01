#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GragasBodySlamTargetSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "", "", },
            AutoBuffActivateEffect = new[]{ "Global_Slow.troy", "", "", },
            BuffName = "GragasBodySlamTargetSlow",
            BuffTextureName = "GragasBodySlam.dds",
        };
        float slowAmount;
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            int level; // UNUSED
            teamID = GetTeamID(attacker);
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.slowAmount = -0.35f;
            ApplyAssistMarker(attacker, attacker, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.slowAmount);
        }
    }
}