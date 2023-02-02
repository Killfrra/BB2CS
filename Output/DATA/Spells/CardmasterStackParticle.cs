#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CardmasterStackParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "CardMasterStackParticle",
            BuffTextureName = "Cardmaster_RapidToss.dds",
        };
        Particle a;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.a, out _, "Cardmaster_stackready.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.a);
        }
    }
}