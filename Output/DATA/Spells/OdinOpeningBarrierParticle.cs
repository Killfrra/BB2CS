#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinOpeningBarrierParticle : BBBuffScript
    {
        Particle particle;
        Particle particle2;
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            teamOfOwner = GetTeamID(owner);
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particle, out _, "Odin_Forcefield_red.troy", default, TeamId.TEAM_BLUE, 280, 0, TeamId.TEAM_PURPLE, default, default, true, owner, default, default, owner, default, default, false, false, default, false, false);
                SpellEffectCreate(out this.particle2, out _, "Odin_Forcefield_green.troy", default, TeamId.TEAM_PURPLE, 280, 0, TeamId.TEAM_BLUE, default, default, true, owner, default, default, owner, default, default, false, false, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "Odin_Forcefield_red.troy", default, TeamId.TEAM_PURPLE, 280, 0, TeamId.TEAM_BLUE, default, default, true, owner, default, default, owner, default, default, false, false, default, false, false);
                SpellEffectCreate(out this.particle2, out _, "Odin_Forcefield_green.troy", default, TeamId.TEAM_BLUE, 280, 0, TeamId.TEAM_PURPLE, default, default, true, owner, default, default, owner, default, default, false, false, default, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            ApplyDamage((ObjAIBase)owner, owner, 25000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
        }
    }
}