#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WallOfPainBeam : BBBuffScript
    {
        Particle particle1;
        Particle particle;
        Particle particle2;
        Particle particle3;
        Particle particleID6;
        Particle noParticle; // UNUSED
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            teamOfOwner = GetTeamID(owner);
            SetForceRenderParticles(owner, true);
            SetForceRenderParticles(attacker, true);
            SpellEffectCreate(out this.particle1, out this.particle, "wallofpain_new_post_green.troy", "wallofpain_new_post_red.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.particle2, out this.particle3, "wallofpain_new_post_green.troy", "wallofpain_new_post_red.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, attacker, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.particleID6, out this.noParticle, "wallofpain__new_beam.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "bottom", default, attacker, "bottom", default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle1);
            SpellEffectRemove(this.particle2);
            SpellEffectRemove(this.particle3);
            SpellEffectRemove(this.particleID6);
        }
    }
}