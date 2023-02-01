#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KennenParticleHolder : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        Particle globeOne;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out this.globeOne, out _, "kennen_mos1.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.globeOne);
        }
    }
}