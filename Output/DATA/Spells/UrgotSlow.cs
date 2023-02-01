#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UrgotSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "", "", },
            AutoBuffActivateEffect = new[]{ "Global_Slow.troy", "", "", },
            BuffName = "Terror Capacitor Slow",
            BuffTextureName = "Chronokeeper_Timestop.dds",
        };
        float slowAmount;
        float[] effect0 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            int level;
            teamID = GetTeamID(attacker);
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.slowAmount = this.effect0[level];
            ApplyAssistMarker(attacker, attacker, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.slowAmount);
        }
    }
}