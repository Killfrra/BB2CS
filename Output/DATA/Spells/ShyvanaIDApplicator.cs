#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShyvanaIDApplicator : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
        };
        float damagePerTick;
        Particle a;
        public ShyvanaIDApplicator(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            //RequireVar(this.damagePerTick);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetNoRender(owner, true);
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out this.a, out _, "shyvana_scorchedEarth_dragon_01_trail.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
            SpellEffectRemove(this.a);
        }
        public override void OnUpdateStats()
        {
            SetIgnoreCallForHelp(owner, true);
            SetSuppressCallForHelp(owner, true);
            SetTargetable(owner, false);
            SetGhosted(owner, true);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_DamagePerTick;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.ShyvanaIDDamage)) == 0)
                {
                    nextBuffVars_DamagePerTick = this.damagePerTick;
                    AddBuff(attacker, unit, new Buffs.ShyvanaIDDamage(nextBuffVars_DamagePerTick), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}