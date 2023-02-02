#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KarmaSpiritBondC : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {7.5f, 7, 6.5f, 6, 5.5f, 5};
        float[] effect1 = {35, 37.5f, 40, 42.5f, 45, 47.5f};
        int[] effect2 = {80, 125, 170, 215, 260, 305};
        float[] effect3 = {0.2f, 0.24f, 0.28f, 0.32f, 0.36f, 0.4f};
        int[] effect4 = {5, 5, 5, 5, 5, 5};
        int[] effect5 = {5, 5, 5, 5, 5, 5};
        float[] effect6 = {-0.2f, -0.24f, -0.28f, -0.32f, -0.36f, -0.4f};
        int[] effect7 = {5, 5, 5, 5, 5, 5};
        int[] effect8 = {5, 5, 5, 5, 5, 5};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            bool isStealthed;
            float nextBuffVars_CooldownToRestore;
            isStealthed = GetStealthed(target);
            if(isStealthed)
            {
                TeamId teamID;
                Particle distanceBreak2; // UNUSED
                float manaToRestore;
                teamID = GetTeamID(owner);
                SpellEffectCreate(out distanceBreak2, out _, "karma_spiritBond_break_overhead.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                nextBuffVars_CooldownToRestore = this.effect0[level];
                manaToRestore = this.effect1[level];
                IncPAR(owner, manaToRestore, PrimaryAbilityResourceType.MANA);
                AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaSBStealthBreak(nextBuffVars_CooldownToRestore), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                int nextBuffVars_MantraBoolean;
                float nextBuffVars_DamageToDeal;
                float nextBuffVars_MoveSpeedMod;
                nextBuffVars_MantraBoolean = 1;
                nextBuffVars_DamageToDeal = this.effect2[level];
                if(target.Team == attacker.Team)
                {
                    nextBuffVars_MoveSpeedMod = this.effect3[level];
                    AddBuff(attacker, target, new Buffs.KarmaSpiritBond(nextBuffVars_MantraBoolean, nextBuffVars_MoveSpeedMod, nextBuffVars_DamageToDeal), 1, 1, this.effect4[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    AddBuff(attacker, attacker, new Buffs.KarmaSpiritBondAllySelfTooltip(), 1, 1, this.effect5[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
                else
                {
                    nextBuffVars_MoveSpeedMod = this.effect6[level];
                    BreakSpellShields(target);
                    AddBuff((ObjAIBase)owner, target, new Buffs.KarmaSpiritBondC(nextBuffVars_MantraBoolean, nextBuffVars_DamageToDeal, nextBuffVars_MoveSpeedMod), 1, 1, this.effect7[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    AddBuff((ObjAIBase)target, owner, new Buffs.KarmaSpiritBondEnemyTooltip(), 1, 1, this.effect8[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, target, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class KarmaSpiritBondC : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KarmaSpiritBondEnemySelf",
            BuffTextureName = "KarmaSpiritBond.dds",
        };
        int mantraBoolean;
        float damageToDeal;
        float moveSpeedMod;
        float negMoveSpeed;
        Particle moveSpeedPart1;
        Particle sBIdle1;
        Particle sBIdle2;
        Particle soulShackleTarget_blood;
        Particle soulShackleTarget;
        Particle soulShackleTarget2;
        Particle particleID;
        Particle soulShackleIdle;
        Particle soundOne;
        Particle soundTwo;
        public KarmaSpiritBondC(int mantraBoolean = default, float damageToDeal = default, float moveSpeedMod = default)
        {
            this.mantraBoolean = mantraBoolean;
            this.damageToDeal = damageToDeal;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            TeamId teamOfAttacker; // UNITIALIZED
            float distance;
            float offsetAngle;
            float halfDistance;
            Vector3 centerPoint;
            TeamId teamID;
            float nextBuffVars_MoveSpeedMod;
            Particle hit; // UNUSED
            //RequireVar(this.mantraBoolean);
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.damageToDeal);
            teamOfOwner = GetTeamID(attacker);
            this.negMoveSpeed = this.moveSpeedMod * -1;
            IncPercentMovementSpeedMod(attacker, this.negMoveSpeed);
            SpellEffectCreate(out this.moveSpeedPart1, out _, "karma_spiritBond_speed_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "root", default, attacker, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.sBIdle1, out _, "leBlanc_shackle_self_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.sBIdle2, out _, "leBlanc_shackle_self_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.soulShackleTarget_blood, out _, "karma_spiritBond_indicator_target_blank.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, default, default, false, false);
            if(this.mantraBoolean == 1)
            {
                SpellEffectCreate(out this.soulShackleTarget, out _, "karma_spiritBond_indicator_target_enemy.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.soulShackleTarget2, out _, "karma_spiritBond_indicator_impact_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.particleID, out this.soulShackleIdle, "karma_spiritBond_ult_beam_teamID_ally_green.troy", "karma_spiritBond_ult_beam_teamID_enemy_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "root", default, owner, "root", default, false, default, default, false, false);
                SpellEffectCreate(out this.soundOne, out this.soundTwo, "KarmaSpiritBondSoundGreen.troy", "KarmaSpiritBondSoundRed.troy", teamOfAttacker ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.soulShackleTarget, out _, "karma_spiritBond_indicator_impact.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.particleID, out this.soulShackleIdle, "karma_spiritBond_ult_beam_teamID_ally_green.troy", "karma_spiritBond_ult_beam_teamID_enemy_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "root", default, owner, "root", default, false, default, default, false, false);
                SpellEffectCreate(out this.soundOne, out this.soundTwo, "KarmaSpiritBondSoundGreen.troy", "KarmaSpiritBondSoundRed.troy", teamOfAttacker ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            }
            distance = DistanceBetweenObjects("Owner", "Attacker");
            offsetAngle = GetOffsetAngle(attacker, owner.Position);
            halfDistance = distance / 2;
            centerPoint = GetPointByUnitFacingOffset(attacker, halfDistance, offsetAngle);
            teamID = GetTeamID(attacker);
            if(teamID == TeamId.TEAM_BLUE)
            {
                foreach(AttackableUnit unit in GetUnitsInRectangle(attacker, centerPoint, 25, halfDistance, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.KarmaLinkDmgCDOrder), false))
                {
                    if(unit != attacker)
                    {
                        if(unit != owner)
                        {
                            AddBuff(attacker, unit, new Buffs.KarmaLinkDmgCDOrder(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            if(this.mantraBoolean == 1)
                            {
                                if(unit.Team == attacker.Team)
                                {
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                    }
                                }
                            }
                            else
                            {
                                if(unit.Team == attacker.Team)
                                {
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach(AttackableUnit unit in GetUnitsInRectangle(attacker, centerPoint, 25, halfDistance, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.KarmaLinkDmgCDChaos), false))
                {
                    if(unit != owner)
                    {
                        if(unit != attacker)
                        {
                            AddBuff(attacker, unit, new Buffs.KarmaLinkDmgCDChaos(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            if(this.mantraBoolean == 1)
                            {
                                if(unit.Team == attacker.Team)
                                {
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                    }
                                }
                            }
                            else
                            {
                                if(unit.Team == attacker.Team)
                                {
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.soundOne);
            SpellEffectRemove(this.soundTwo);
            SpellEffectRemove(this.moveSpeedPart1);
            SpellEffectRemove(this.sBIdle1);
            SpellEffectRemove(this.sBIdle2);
            SpellEffectRemove(this.particleID);
            if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.KarmaMantraSBSlow)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.KarmaMantraSBSlow), attacker, 0);
            }
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.KarmaSpiritBondEnemyTooltip)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.KarmaSpiritBondEnemyTooltip), attacker, 0);
            }
            SpellEffectRemove(this.soulShackleIdle);
            SpellEffectRemove(this.soulShackleTarget);
            SpellEffectRemove(this.soulShackleTarget_blood);
            if(this.mantraBoolean == 1)
            {
                SpellEffectRemove(this.soulShackleTarget2);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(attacker, this.negMoveSpeed);
        }
        public override void OnUpdateActions()
        {
            float distance;
            float offsetAngle;
            float halfDistance;
            Vector3 centerPoint;
            TeamId teamID;
            Particle distanceBreak2; // UNUSED
            float nextBuffVars_MoveSpeedMod;
            Particle hit; // UNUSED
            bool isStealthed;
            distance = DistanceBetweenObjects("Owner", "Attacker");
            offsetAngle = GetOffsetAngle(attacker, owner.Position);
            halfDistance = distance / 2;
            centerPoint = GetPointByUnitFacingOffset(attacker, halfDistance, offsetAngle);
            teamID = GetTeamID(attacker);
            if(owner.IsDead)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                if(attacker.IsDead)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    distance = DistanceBetweenObjects("Owner", "Attacker");
                    if(distance > 900)
                    {
                        Particle distanceBreak1; // UNUSED
                        SpellEffectCreate(out distanceBreak1, out _, "karma_spiritBond_break_overhead.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, default, default, false, false);
                        SpellEffectCreate(out distanceBreak2, out _, "karma_spiritBond_break_overhead.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                        SpellBuffRemoveCurrent(owner);
                    }
                }
            }
            if(teamID == TeamId.TEAM_BLUE)
            {
                foreach(AttackableUnit unit in GetUnitsInRectangle(attacker, centerPoint, 25, halfDistance, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.KarmaLinkDmgCDOrder), false))
                {
                    if(unit != owner)
                    {
                        if(unit != attacker)
                        {
                            AddBuff(attacker, unit, new Buffs.KarmaLinkDmgCDOrder(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            if(this.mantraBoolean == 1)
                            {
                                if(unit.Team == attacker.Team)
                                {
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                    }
                                }
                            }
                            else
                            {
                                if(unit.Team == attacker.Team)
                                {
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach(AttackableUnit unit in GetUnitsInRectangle(attacker, centerPoint, 25, halfDistance, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.KarmaLinkDmgCDChaos), false))
                {
                    if(unit != attacker)
                    {
                        if(unit != owner)
                        {
                            AddBuff(attacker, unit, new Buffs.KarmaLinkDmgCDChaos(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            if(this.mantraBoolean == 1)
                            {
                                if(unit.Team == attacker.Team)
                                {
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                    }
                                }
                            }
                            else
                            {
                                if(unit.Team == attacker.Team)
                                {
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            isStealthed = GetStealthed(owner);
            if(isStealthed)
            {
                SpellEffectCreate(out distanceBreak2, out _, "karma_spiritBond_break_overhead.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}