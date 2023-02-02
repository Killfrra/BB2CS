#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingDeathParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Particle particle1;
        public override void OnDeath()
        {
            SpellEffectCreate(out this.particle1, out _, "CassiopeiaDeath.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            Particle stoneRemoval; // UNUSED
            SpellEffectRemove(this.particle1);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out stoneRemoval, out _, "MonkeyKingPHRemoveRocks.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
        }
    }
}