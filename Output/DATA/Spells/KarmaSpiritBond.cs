#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KarmaSpiritBond : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {7.5f, 7, 6.5f, 6, 5.5f, 5};
        float[] effect1 = {35, 37.5f, 40, 42.5f, 45, 47.5f};
        int[] effect2 = {80, 125, 170, 215, 260, 305};
        float[] effect3 = {0.1f, 0.12f, 0.14f, 0.16f, 0.18f, 0.2f};
        int[] effect4 = {5, 5, 5, 5, 5, 5};
        int[] effect5 = {5, 5, 5, 5, 5, 5};
        float[] effect6 = {-0.1f, -0.12f, -0.14f, -0.16f, -0.18f, -0.2f};
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
                nextBuffVars_MantraBoolean = 0;
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
                    AddBuff(attacker, target, new Buffs.KarmaSpiritBondC(nextBuffVars_MantraBoolean, nextBuffVars_DamageToDeal, nextBuffVars_MoveSpeedMod), 1, 1, this.effect7[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    AddBuff((ObjAIBase)target, owner, new Buffs.KarmaSpiritBondEnemyTooltip(), 1, 1, this.effect8[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, target, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
            }
        }
    }
}
namespace Buffs
{
    public class KarmaSpiritBond : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KarmaSpiritBondAlly",
            BuffTextureName = "KarmaSpiritBond.dds",
        };
        int mantraBoolean;
        float moveSpeedMod;
        float damageToDeal;
        float negMoveSpeed;
        Particle sBIdle1;
        Particle sBIdle2;
        Particle soulShackleIdle;
        Particle soulShackleTarget_blood;
        Particle moveSpeedPart1;
        Particle moveSpeedPart2;
        Particle soulShackleTarget2;
        Particle dmgIndicatorL2;
        Particle dmgIndicatorR2;
        Particle dmgIndicatorR;
        Particle dmgIndicatorL;
        Particle particleID;
        Particle soundOne;
        Particle soundTwo;
        public KarmaSpiritBond(int mantraBoolean = default, float moveSpeedMod = default, float damageToDeal = default)
        {
            this.mantraBoolean = mantraBoolean;
            this.moveSpeedMod = moveSpeedMod;
            this.damageToDeal = damageToDeal;
        }
        public override void OnActivate()
        {
            TeamId teamOfAttacker;
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
            ApplyAssistMarker(attacker, owner, 10);
            teamOfAttacker = GetTeamID(attacker);
            LinkVisibility(attacker, owner);
            this.negMoveSpeed = this.moveSpeedMod * -1;
            SpellEffectCreate(out this.sBIdle1, out _, "leBlanc_shackle_self_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.sBIdle2, out _, "leBlanc_shackle_self_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.soulShackleIdle, out _, "karma_spiritBond_indicator_target_blank.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.soulShackleTarget_blood, out _, "karma_spiritBond_indicator_impact.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.moveSpeedPart1, out _, "karma_spiritBond_speed_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.moveSpeedPart2, out _, "karma_spiritBond_speed_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "root", default, attacker, default, default, false, default, default, false, false);
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
            IncPercentMovementSpeedMod(attacker, this.moveSpeedMod);
            if(this.mantraBoolean == 1)
            {
                SpellEffectCreate(out this.soulShackleTarget2, out _, "karma_spiritBond_indicator_target.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.dmgIndicatorL2, out _, "karma_spiritBond_dmg_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.dmgIndicatorR2, out _, "karma_spiritBond_dmg_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.dmgIndicatorR, out _, "karma_spiritBond_dmg_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "R_hand", default, attacker, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.dmgIndicatorL, out _, "karma_spiritBond_dmg_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "L_hand", default, attacker, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.particleID, out this.soulShackleIdle, "karma_spiritBond_ult_beam_teamID_ally_green.troy", "karma_spiritBond_ult_beam_teamID_enemy_red.troy", teamOfAttacker ?? TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "root", default, owner, "root", default, false, default, default, false, false);
                SpellEffectCreate(out this.soundOne, out this.soundTwo, "KarmaSpiritBondSoundGreen.troy", "KarmaSpiritBondSoundRed.troy", teamOfAttacker ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particleID, out this.soulShackleIdle, "karma_spiritBond_ult_beam_teamID_ally_green.troy", "karma_spiritBond_ult_beam_teamID_enemy_red.troy", teamOfAttacker ?? TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "root", default, owner, "root", default, false, default, default, false, false);
                SpellEffectCreate(out this.soundOne, out this.soundTwo, "KarmaSpiritBondSoundGreen.troy", "KarmaSpiritBondSoundRed.troy", teamOfAttacker ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            }
            distance = DistanceBetweenObjects("Owner", "Attacker");
            offsetAngle = GetOffsetAngle(owner, attacker.Position);
            halfDistance = distance / 2;
            centerPoint = GetPointByUnitFacingOffset(owner, halfDistance, offsetAngle);
            teamID = GetTeamID(owner);
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
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                        AddBuff((ObjAIBase)owner, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                    }
                                }
                            }
                            else
                            {
                                if(unit.Team == attacker.Team)
                                {
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                        AddBuff((ObjAIBase)owner, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
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
                            AddBuff((ObjAIBase)owner, unit, new Buffs.KarmaLinkDmgCDChaos(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            if(this.mantraBoolean == 1)
                            {
                                if(unit.Team == attacker.Team)
                                {
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
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
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
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
            SpellEffectRemove(this.sBIdle1);
            SpellEffectRemove(this.sBIdle2);
            SpellEffectRemove(this.particleID);
            SpellEffectRemove(this.soulShackleIdle);
            SpellEffectRemove(this.soulShackleTarget_blood);
            SpellEffectRemove(this.moveSpeedPart1);
            SpellEffectRemove(this.moveSpeedPart2);
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.KarmaSpiritBondAllySelfTooltip)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.KarmaSpiritBondAllySelfTooltip), attacker, 0);
            }
            if(this.mantraBoolean == 1)
            {
                SpellEffectRemove(this.dmgIndicatorR);
                SpellEffectRemove(this.dmgIndicatorL);
                SpellEffectRemove(this.dmgIndicatorL2);
                SpellEffectRemove(this.dmgIndicatorR2);
                SpellEffectRemove(this.soulShackleTarget2);
            }
            RemoveLinkVisibility(attacker, owner);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
            IncPercentMovementSpeedMod(attacker, this.moveSpeedMod);
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
            offsetAngle = GetOffsetAngle(owner, attacker.Position);
            halfDistance = distance / 2;
            centerPoint = GetPointByUnitFacingOffset(owner, halfDistance, offsetAngle);
            teamID = GetTeamID(owner);
            if(attacker.IsDead)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                if(owner.IsDead)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    if(distance > 900)
                    {
                        Particle distanceBreak1; // UNUSED
                        SpellEffectCreate(out distanceBreak1, out _, "karma_spiritBond_break_overhead.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                        SpellEffectCreate(out distanceBreak2, out _, "karma_spiritBond_break_overhead.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, default, default, false, false);
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
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                        AddBuff((ObjAIBase)owner, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                                    }
                                }
                            }
                            else
                            {
                                if(unit.Team == attacker.Team)
                                {
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
                                        AddBuff((ObjAIBase)owner, unit, new Buffs.KarmaMantraSBSlow(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
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
                            AddBuff((ObjAIBase)owner, unit, new Buffs.KarmaLinkDmgCDChaos(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            if(this.mantraBoolean == 1)
                            {
                                if(unit.Team == attacker.Team)
                                {
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
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
                                        nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                                        AddBuff(attacker, unit, new Buffs.KarmaMantraSBHaste(nextBuffVars_MoveSpeedMod), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                                    }
                                }
                                else
                                {
                                    SpellEffectCreate(out hit, out _, "karma_spiritBond_damage_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                                    BreakSpellShields(unit);
                                    ApplyDamage(attacker, unit, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 0, false, false, attacker);
                                    if(unit is Champion)
                                    {
                                        nextBuffVars_MoveSpeedMod = this.negMoveSpeed;
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