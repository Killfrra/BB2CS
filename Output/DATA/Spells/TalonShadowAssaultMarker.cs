#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TalonShadowAssaultMarker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TalonShadowAssaultMarker",
        };
        Particle particleZ;
        Particle particleY;
        public override void OnActivate()
        {
            TeamId attackerTeam;
            int attackerSkinID;
            attackerTeam = GetTeamID(attacker);
            attackerSkinID = GetSkinID(attacker);
            if(attackerSkinID == 3)
            {
                SpellEffectCreate(out this.particleZ, out this.particleY, "talon_ult_blade_hold_dragon.troy", "talon_ult_blade_hold_team_ID_red_dragon.troy", attackerTeam, 1, 0, TeamId.TEAM_UNKNOWN, attackerTeam, owner, false, owner, "root", default, attacker, default, default, false, false, false, false, true);
            }
            else
            {
                SpellEffectCreate(out this.particleZ, out this.particleY, "talon_ult_blade_hold.troy", "talon_ult_blade_hold_team_ID_red.troy", attackerTeam, 1, 0, TeamId.TEAM_UNKNOWN, attackerTeam, owner, false, owner, "root", default, attacker, default, default, false, false, false, false, true);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleZ);
            SpellEffectRemove(this.particleY);
        }
    }
}