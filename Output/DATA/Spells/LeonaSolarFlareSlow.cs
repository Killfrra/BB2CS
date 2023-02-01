#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeonaSolarFlareSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Slow",
            BuffTextureName = "LeonaSolarFlare.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        Particle sSSlow;
        float mSPenalty;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.sSSlow, out _, "Global_Slow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            this.mSPenalty = -0.8f;
            IncPercentMultiplicativeMovementSpeedMod(owner, this.mSPenalty);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.sSSlow);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.mSPenalty);
        }
    }
}