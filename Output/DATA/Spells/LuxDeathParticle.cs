#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LuxDeathParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        Particle particle2; // UNUSED
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particle2, out _, "Lux_death.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, "C_BUFFBONE_GLB_CENTER_LOC", default, false);
        }
    }
}