#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaSpellSword : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "OrianaSpellSword",
            BuffTextureName = "OriannaPowerDagger.dds",
            IsDeathRecapSource = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        Particle particle; // UNUSED
        int[] effect0 = {5, 5, 5, 10, 10, 10, 15, 15, 15, 20, 20, 20, 25, 25, 25, 30, 30, 30, 35, 35, 35};
        int[] effect1 = {5, 5, 5, 10, 10, 10, 15, 15, 15, 20, 20, 20, 25, 25, 25, 30, 30, 30, 35, 35, 35};
        int[] effect2 = {5, 5, 5, 10, 10, 10, 15, 15, 15, 20, 20, 20, 25, 25, 25, 30, 30, 30, 35, 35, 35};
        public override void OnUpdateActions()
        {
            int level;
            float baseDamage;
            float aPBonus;
            float damage;
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, true))
            {
                level = GetLevel(owner);
                baseDamage = this.effect0[level];
                SetBuffToolTipVar(2, baseDamage);
                baseDamage = this.effect1[level];
                aPBonus = GetFlatMagicDamageMod(owner);
                damage = aPBonus * 0.2f;
                damage += baseDamage;
                SetBuffToolTipVar(3, damage);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            int castSlot;
            TeamId teamID;
            bool deployed;
            Vector3 targetPos;
            bool isStealth;
            castSlot = GetSpellSlot();
            if(castSlot == 3)
            {
                teamID = GetTeamID(owner);
                deployed = false;
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectUntargetable, 1, nameof(Buffs.OrianaGhost), true))
                {
                    deployed = true;
                    targetPos = GetUnitPosition(unit);
                    if(unit is Champion)
                    {
                        isStealth = GetStealthed(owner);
                        if(!isStealth)
                        {
                            SpellEffectCreate(out this.particle, out _, "OrianaVacuumIndicator_ally.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "spinnigtopridge", targetPos, default, default, targetPos, true, false, false, false, false);
                            charVars.UltimateType = 0;
                            charVars.UltimateTargetPos = targetPos;
                        }
                        else
                        {
                            SpellEffectCreate(out this.particle, out _, "OrianaVacuumIndicatorSelfNoRing.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", targetPos, default, default, targetPos, true, false, false, false, false);
                            SpellEffectCreate(out this.particle, out _, "OrianaVacuumIndicatorSelfRing.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", targetPos, default, default, targetPos, true, false, false, false, false);
                            charVars.UltimateType = 1;
                            charVars.UltimateTargetPos = targetPos;
                        }
                    }
                    else
                    {
                        SpellEffectCreate(out this.particle, out _, "OrianaVacuumIndicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "spinnigtopridge", targetPos, default, default, targetPos, true, false, false, false, false);
                        charVars.UltimateType = 1;
                        charVars.UltimateTargetPos = targetPos;
                    }
                }
                if(!deployed)
                {
                    if(GetBuffCountFromCaster(owner, default, nameof(Buffs.OriannaBallTracker)) > 0)
                    {
                        targetPos = charVars.BallPosition;
                        SpellEffectCreate(out this.particle, out _, "OrianaVacuumIndicatorSelfNoRing.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", targetPos, default, default, targetPos, true, false, false, false, false);
                        SpellEffectCreate(out this.particle, out _, "OrianaVacuumIndicatorSelfRing.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", targetPos, default, default, targetPos, true, false, false, false, false);
                        charVars.UltimateType = 5;
                        charVars.UltimateTargetPos = targetPos;
                    }
                    else
                    {
                        targetPos = GetPointByUnitFacingOffset(owner, 0, 0);
                        SpellEffectCreate(out this.particle, out _, "OrianaVacuumIndicatorSelfNoRing.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spinnigtopridge", targetPos, default, default, targetPos, true, false, false, false, false);
                        SpellEffectCreate(out this.particle, out _, "OrianaVacuumIndicatorSelfRing.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, "root", targetPos, default, default, targetPos, true, false, false, false, false);
                        charVars.UltimateType = 3;
                        charVars.UltimateTargetPos = targetPos;
                    }
                }
            }
        }
        public override void OnDeath()
        {
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, default, nameof(Buffs.OrianaGhost)) > 0)
                {
                    SpellBuffClear(unit, nameof(Buffs.OrianaGhost));
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float aPBonus;
            int level;
            float baseDamage;
            float damage;
            int count;
            float multiplier;
            aPBonus = GetFlatMagicDamageMod(owner);
            level = GetLevel(owner);
            baseDamage = this.effect2[level];
            damage = aPBonus * 0.2f;
            damage += baseDamage;
            count = GetBuffCountFromCaster(target, owner, nameof(Buffs.OrianaPowerDagger));
            multiplier = 0.15f * count;
            multiplier++;
            damage *= multiplier;
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(GetBuffCountFromCaster(target, owner, nameof(Buffs.OrianaPowerDagger)) == 0)
                    {
                        SpellBuffClear(owner, nameof(Buffs.OrianaPowerDaggerDisplay));
                    }
                    AddBuff((ObjAIBase)owner, target, new Buffs.OrianaPowerDagger(), 3, 1, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.OrianaPowerDagger), true))
                    {
                        if(unit != target)
                        {
                            SpellBuffClear(unit, nameof(Buffs.OrianaPowerDagger));
                        }
                    }
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OrianaPowerDaggerDisplay(), 3, 1, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(target is ObjAIBase)
                    {
                        ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                    }
                }
            }
        }
    }
}