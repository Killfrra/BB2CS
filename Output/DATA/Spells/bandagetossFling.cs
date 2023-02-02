#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BandagetossFling : BBBuffScript
    {
        bool willBeam;
        public override void OnActivate()
        {
            SetNoRender(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetForceRenderParticles(owner, true);
            //RequireVar(this.willBeam);
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1);
        }
        public override void OnUpdateStats()
        {
            SetNoRender(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetForceRenderParticles(owner, true);
            if(this.willBeam)
            {
                Particle nextBuffVars_ParticleID;
                SpellEffectCreate(out nextBuffVars_ParticleID, out _, default, default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, attacker, "R_hand", default, false);
                this.willBeam = false;
                AddBuff(attacker, attacker, new Buffs.BandagetossFlingCaster(nextBuffVars_ParticleID), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}