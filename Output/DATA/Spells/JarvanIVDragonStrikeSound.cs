#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JarvanIVDragonStrikeSound : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        Particle dragonStrikeSound;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.dragonStrikeSound, out _, "JarvanDemacianStandard_flag_hit.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.dragonStrikeSound);
        }
    }
}