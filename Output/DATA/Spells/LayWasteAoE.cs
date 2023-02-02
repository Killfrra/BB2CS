#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LayWasteAoE : BBBuffScript
    {
        float damagePerTick;
        Particle particle;
        float lastTimeExecuted;
        public LayWasteAoE(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SpellEffectCreate(out this.particle, out _, default, default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
                {
                    Particle hi1; // UNUSED
                    Particle hi2; // UNUSED
                    ApplyDamage(attacker, unit, this.damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0.1f);
                    SpellEffectCreate(out hi1, out _, default, default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "L_foot", default, unit, default, default, false);
                    SpellEffectCreate(out hi2, out _, default, default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "R_foot", default, unit, default, default, false);
                }
            }
        }
    }
}