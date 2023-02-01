#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GlacialStormSpell : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "RunePrison_tar.troy", },
        };
        float damagePerLevel;
        Particle ambientParticle;
        float lastTimeExecuted;
        public GlacialStormSpell(float damagePerLevel = default)
        {
            this.damagePerLevel = damagePerLevel;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerLevel);
            SpellEffectCreate(out this.ambientParticle, out _, "cryo_storm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            SetNoRender(owner, true);
            SetCanMove(owner, false);
            SetGhosted(owner, true);
            SetCanAttack(owner, false);
            SetInvulnerable(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetForceRenderParticles(owner, true);
            SetTargetable(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.ambientParticle);
            SetTargetable(owner, true);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_RAW, 1, 1, 1, false, false);
        }
        public override void OnUpdateStats()
        {
            SetNoRender(owner, true);
            SetCanMove(owner, false);
            SetGhosted(owner, true);
            SetCanAttack(owner, false);
            SetInvulnerable(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetForceRenderParticles(owner, true);
            SetTargetable(owner, false);
        }
        public override void OnUpdateActions()
        {
            float distance; // UNUSED
            float nextBuffVars_MovementSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                SpellEffectCreate(out this.ambientParticle, out _, "cryo_storm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                if(GetBuffCountFromCaster(attacker, owner, nameof(Buffs.GlacialStorm)) > 0)
                {
                    distance = DistanceBetweenObjects("Attacker", "Owner");
                    foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
                    {
                        ApplyDamage(attacker, unit, this.damagePerLevel, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.0625f, 1, false, false);
                        nextBuffVars_MovementSpeedMod = -0.3f;
                        nextBuffVars_AttackSpeedMod = -0.15f;
                        AddBuff(attacker, unit, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0);
                    }
                }
                else
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}