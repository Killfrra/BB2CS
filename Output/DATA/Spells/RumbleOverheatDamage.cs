﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RumbleOverheatDamage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "root", "", "", },
            AutoBuffActivateEffect = new[]{ "dr_mundo_burning_agony_cas_02.troy", "", "", },
            BuffName = "Burning Up",
            BuffTextureName = "DrMundo_BurningAgony.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                float maxHealth;
                float percHealth;
                float burnDmg;
                maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                percHealth = maxHealth / 100;
                burnDmg = percHealth * 1;
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, burnDmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                }
            }
        }
    }
}