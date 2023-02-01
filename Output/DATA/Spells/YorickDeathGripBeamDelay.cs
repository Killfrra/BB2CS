#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickDeathGripBeamDelay : BBBuffScript
    {
        int durationLevel;
        Particle particle;
        Particle particle1;
        Particle particle2;
        Particle particle3;
        Particle particleID6;
        public YorickDeathGripBeamDelay(int durationLevel = default)
        {
            this.durationLevel = durationLevel;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            //RequireVar(this.durationLevel);
            teamOfOwner = GetTeamID(owner);
            SetForceRenderParticles(owner, true);
            SetForceRenderParticles(attacker, true);
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particle, out _, "wallofpain_new_post_red.troy", default, TeamId.TEAM_PURPLE, 200, 0, TeamId.TEAM_PURPLE, default, default, true, owner, default, default, target, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.particle1, out _, "wallofpain_new_post_green.troy", default, TeamId.TEAM_BLUE, 200, 0, TeamId.TEAM_BLUE, default, default, true, owner, default, default, target, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.particle2, out _, "wallofpain_new_post_red.troy", default, TeamId.TEAM_PURPLE, 200, 0, TeamId.TEAM_PURPLE, default, default, true, attacker, default, default, target, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.particle3, out _, "wallofpain_new_post_green.troy", default, TeamId.TEAM_BLUE, 200, 0, TeamId.TEAM_BLUE, default, default, true, attacker, default, default, target, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.particleID6, out _, "YorickPHWallBlue.troy", default, TeamId.TEAM_BLUE, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "bottom", default, attacker, "bottom", default, false, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "wallofpain_new_post_red.troy", default, TeamId.TEAM_BLUE, 200, 0, TeamId.TEAM_BLUE, default, default, true, owner, default, default, target, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.particle1, out _, "wallofpain_new_post_green.troy", default, TeamId.TEAM_PURPLE, 200, 0, TeamId.TEAM_PURPLE, default, default, true, owner, default, default, target, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.particle2, out _, "wallofpain_new_post_red.troy", default, TeamId.TEAM_BLUE, 200, 0, TeamId.TEAM_BLUE, default, default, true, attacker, default, default, target, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.particle3, out _, "wallofpain_new_post_green.troy", default, TeamId.TEAM_PURPLE, 200, 0, TeamId.TEAM_PURPLE, default, default, true, attacker, default, default, target, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.particleID6, out _, "YorickPHWallBlue.troy", default, TeamId.TEAM_PURPLE, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "bottom", default, attacker, "bottom", default, false, default, default, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            float durationLevel;
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle1);
            SpellEffectRemove(this.particle2);
            SpellEffectRemove(this.particle3);
            SpellEffectRemove(this.particleID6);
            durationLevel = this.durationLevel;
            AddBuff(attacker, owner, new Buffs.YorickDeathGripBeam(), 1, 1, durationLevel, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}