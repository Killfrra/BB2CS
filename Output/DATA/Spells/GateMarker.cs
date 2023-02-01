#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GateMarker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Gate Target",
            BuffTextureName = "Cardmaster_Premonition.dds",
        };
        Particle teleportParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.teleportParticle, out _, "GateMarker.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            SetNoRender(owner, true);
            SetGhosted(owner, true);
            SetGhosted(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetTargetable(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetable(owner, true);
            SpellEffectRemove(this.teleportParticle);
        }
    }
}