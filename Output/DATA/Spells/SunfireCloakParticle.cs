﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SunfireCloakParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Sunfire Cape Self",
            BuffTextureName = "3068_Sunfire_Cape.dds",
        };
        Particle sCP;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.sCP, out _, "SunfireCape_Aura.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.sCP);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                if(owner is Champion)
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        ApplyDamage(attacker, unit, 35, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0, 0, false, false, attacker);
                        AddBuff((ObjAIBase)owner, unit, new Buffs.SunfireCapeAura(), 1, 1, 1.05f, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                    }
                }
                else
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.SunfireCapeAura), false))
                    {
                        AddBuff(attacker, unit, new Buffs.SunfireCapeAura(), 1, 1, 1.05f, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                    }
                }
            }
        }
    }
}