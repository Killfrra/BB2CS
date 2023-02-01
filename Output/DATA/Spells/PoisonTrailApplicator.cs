﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoisonTrailApplicator : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
        };
        float damagePerTick;
        public PoisonTrailApplicator(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetNoRender(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
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
            int count;
            float nextBuffVars_DamagePerTick;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 180, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                count = GetBuffCountFromAll(unit, nameof(Buffs.PoisonTrailMarker));
                if(count == 0)
                {
                    AddBuff(attacker, unit, new Buffs.PoisonTrailMarker(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    nextBuffVars_DamagePerTick = this.damagePerTick;
                    AddBuff(attacker, unit, new Buffs.PoisonTrailTarget(nextBuffVars_DamagePerTick), 1, 1, 2.1f, BuffAddType.RENEW_EXISTING, BuffType.POISON, 0, true, false);
                }
            }
        }
    }
}