#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinCaptureSoundEmptying : BBBuffScript
    {
        Particle particle;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particle, out _, "Odin-Capture-Emptying.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "Crystal", owner.Position, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
        }
    }
}