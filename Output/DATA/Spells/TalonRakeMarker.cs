#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TalonRakeMarker : BBBuffScript
    {
        Particle particleZ;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particleZ, out _, "BladeRgoue_BladeAOE_TEMP.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, attacker, default, default, false, default, default, false, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleZ);
        }
    }
}