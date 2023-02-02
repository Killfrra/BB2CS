#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenTriCleaveDamageDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "BlindMonkSafeguard",
        };
        public override void OnActivate()
        {
            TeamId ownerVar;
            Particle a; // UNUSED
            ownerVar = GetTeamID(owner);
            SpellEffectCreate(out a, out _, "exile_Q_tar_01.troy", default, ownerVar ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, true, false, false, false, false);
            SpellEffectCreate(out a, out _, "exile_Q_tar_02.troy", default, ownerVar ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, owner, default, default, true, false, false, false, false);
        }
    }
}