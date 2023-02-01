#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyParagonParticle : BBBuffScript
    {
        Particle maxParticle;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.maxParticle, out _, "PoppyDemacia_max.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_finger", default, owner, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.maxParticle);
        }
    }
}