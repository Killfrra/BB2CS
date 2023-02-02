#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AlistarTrample : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Trample Buff",
            BuffTextureName = "Minotaur_ColossalStrength.dds",
            IsDeathRecapSource = true,
        };
        Particle tremorsFx;
        float lastTimeExecuted;
        int[] effect0 = {10, 11, 12, 12, 13, 14, 15, 15, 16, 17, 18, 18, 19, 20, 21, 21, 22, 23};
        public override void OnActivate()
        {
            SetGhosted(owner, true);
            SpellEffectCreate(out this.tremorsFx, out _, "alistar_trample_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            OverrideAnimation("Run", "Run2", owner);
            OverrideAnimation("Idle1", "Idle5", owner);
            SpellEffectCreate(out this.tremorsFx, out _, "alistar_nose_puffs.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_NOSE1", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.tremorsFx, out _, "alistar_trample_head.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.tremorsFx, out _, "alistar_nose_puffs.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_NOSE2", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.tremorsFx, out _, "alistar_trample_hand.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.tremorsFx, out _, "alistar_trample_hand.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, target, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetGhosted(owner, false);
            SpellEffectRemove(this.tremorsFx);
            ClearOverrideAnimation("Run", owner);
            ClearOverrideAnimation("Idle1", owner);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                int level;
                float damageByRank;
                float totalAttackDamage;
                float baseAttackDamage;
                float abilityPower;
                float bonusAttackDamage;
                float attackDamageToAdd;
                float abilityPowerToAdd;
                float damageToDeal;
                level = GetLevel(owner);
                damageByRank = this.effect0[level];
                totalAttackDamage = GetTotalAttackDamage(owner);
                baseAttackDamage = GetBaseAttackDamage(owner);
                abilityPower = GetFlatMagicDamageMod(owner);
                bonusAttackDamage = totalAttackDamage - baseAttackDamage;
                attackDamageToAdd = bonusAttackDamage * 0;
                abilityPowerToAdd = abilityPower * 0.1f;
                damageToDeal = damageByRank + abilityPowerToAdd;
                damageToDeal += attackDamageToAdd;
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectBuildings | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, default, true))
                {
                    if(unit is Champion)
                    {
                        ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    }
                    else if(unit is not ObjAIBase)
                    {
                        ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    }
                    else if(unit is BaseTurret)
                    {
                        ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    }
                    else
                    {
                        ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 2, 0, 1, false, false, attacker);
                    }
                }
            }
        }
    }
}