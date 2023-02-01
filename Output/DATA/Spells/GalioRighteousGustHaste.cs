#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GalioRighteousGustHaste : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "GalioRighteousGustHaste",
            BuffTextureName = "Galio_RighteousGust.dds",
        };
        float moveSpeedMod;
        Particle buffVFXAlly;
        Particle buffVFXEnemy;
        public GalioRighteousGustHaste(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            //RequireVar(this.moveSpeedMod);
            IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedMod);
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.buffVFXAlly, out _, "galio_windTunnel_speed_buf.troy", default, TeamId.TEAM_PURPLE, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, "root", default, owner, default, default, false);
                SpellEffectCreate(out this.buffVFXEnemy, out _, "galio_windTunnel_speed_buf_team_red.tro", default, TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "root", default, owner, default, default, false);
            }
            else
            {
                SpellEffectCreate(out this.buffVFXAlly, out _, "galio_windTunnel_speed_buf.troy", default, TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "root", default, owner, default, default, false);
                SpellEffectCreate(out this.buffVFXEnemy, out _, "galio_windTunnel_speed_buf_team_red.tro", default, TeamId.TEAM_PURPLE, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, "root", default, owner, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffVFXAlly);
            SpellEffectRemove(this.buffVFXEnemy);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedMod);
        }
    }
}