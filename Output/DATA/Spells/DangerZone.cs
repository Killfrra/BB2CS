#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DangerZone : BBBuffScript
    {
        float damagePerTick;
        Particle particle;
        float lastTimeExecuted;
        public DangerZone(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            //RequireVar(this.damagePerTick);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.particle, out _, "corki_valkrie_impact_cas.troy", default, teamOfOwner, 900, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 0, false, false, attacker);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_DamagePerTick;
            Particle hi1; // UNUSED
            nextBuffVars_DamagePerTick = this.damagePerTick;
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 150, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    SpellEffectCreate(out hi1, out _, "corki_fire_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "pelvis", default, unit, default, default, false, false, false, false, false);
                    if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.DangerZoneTarget)) > 0)
                    {
                    }
                    else
                    {
                        AddBuff(attacker, unit, new Buffs.DangerZoneTarget(nextBuffVars_DamagePerTick), 1, 1, 0.49f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
    }
}