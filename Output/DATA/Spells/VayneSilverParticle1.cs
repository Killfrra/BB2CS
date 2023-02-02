#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VayneSilverParticle1 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
        };
        Particle globeOne;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.globeOne, out _, "vayne_W_ring1.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.globeOne);
        }
    }
}