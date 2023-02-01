#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TalonRakeMissileOne : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TalonRakeMissileOne",
            IsDeathRecapSource = true,
        };
        int[] effect0 = {14, 13, 12, 11, 10};
        public override void OnDeactivate(bool expired)
        {
            int level;
            Vector3 ownerPos;
            Vector3 unitPos;
            float newDistance;
            float cooldownVal;
            float flatCDVal;
            float flatCD;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            ownerPos = GetUnitPosition(owner);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, ownerPos, 3000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectUntargetable, default, true))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.TalonRakeMarker)) > 0)
                {
                    unitPos = GetUnitPosition(unit);
                    newDistance = DistanceBetweenObjects("Owner", "Unit");
                    if(newDistance > 100)
                    {
                        SpellCast((ObjAIBase)owner, owner, ownerPos, default, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, unitPos);
                    }
                    SpellBuffRemove(unit, nameof(Buffs.TalonRakeMarker), (ObjAIBase)owner, 0);
                    SetInvulnerable(unit, false);
                    SetTargetable(unit, true);
                    ApplyDamage((ObjAIBase)unit, unit, 50000, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 0, false, false, attacker);
                }
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cooldownVal = this.effect0[level];
            flatCDVal = 0;
            flatCD = GetPercentCooldownMod(owner);
            flatCDVal = cooldownVal * flatCD;
            cooldownVal += flatCDVal;
        }
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            Vector3 ownerPos;
            TeamId teamOfOwner;
            Minion other3;
            Vector3 unitPos; // UNUSED
            int level; // UNUSED
            if(spellName == nameof(Spells.TalonRakeMissileOne))
            {
                ownerPos = GetUnitPosition(owner);
                teamOfOwner = GetTeamID(owner);
                other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", missileEndPosition, teamOfOwner, false, true, false, false, false, true, 0, false, true, (Champion)owner);
                FaceDirection(other3, ownerPos);
                SetInvulnerable(other3, true);
                SetTargetable(other3, false);
                charVars.MissileNumber++;
                unitPos = GetUnitPosition(other3);
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(charVars.MissileNumber > 2)
                {
                    charVars.MissileNumber = 0;
                    SpellBuffRemove(owner, nameof(Buffs.TalonRakeMissileOne), (ObjAIBase)owner, 0);
                }
                AddBuff((ObjAIBase)owner, other3, new Buffs.TalonRakeMarker(), 1, 1, 100000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                DestroyMissile(charVars.MISSILEID2);
            }
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            SpellMissile missileId; // UNITIALIZED
            charVars.MISSILEID2 = missileId;
        }
    }
}
namespace Spells
{
    public class TalonRakeMissileOne : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {30, 55, 80, 105, 130};
        float[] effect1 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        float[] effect2 = {-0.15f, -0.2f, -0.25f, -0.3f, -0.35f};
        int[] effect3 = {30, 55, 80, 105, 130};
        float[] effect4 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        float[] effect5 = {-0.15f, -0.2f, -0.25f, -0.3f, -0.35f};
        int[] effect6 = {30, 55, 80, 105, 130};
        float[] effect7 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        float[] effect8 = {-0.15f, -0.2f, -0.25f, -0.3f, -0.35f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int count;
            TeamId ownerTeamID;
            bool isStealthed;
            Particle part; // UNUSED
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_MovementSpeedMod;
            float baseDamage;
            float totalAD;
            float bonusDamage;
            bool canSee;
            count = GetBuffCountFromCaster(target, target, nameof(Buffs.TalonRakeMissileOneMarker));
            ownerTeamID = GetTeamID(owner);
            if(count == 0)
            {
                isStealthed = GetStealthed(target);
                if(!isStealthed)
                {
                    SpellEffectCreate(out part, out _, "talon_w_tar.troy", default, ownerTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, true, false, false, false, false);
                    AddBuff((ObjAIBase)target, target, new Buffs.TalonRakeMissileOneMarker(), 9, 1, 0.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(target);
                    baseDamage = GetBaseAttackDamage(owner);
                    totalAD = GetTotalAttackDamage(owner);
                    baseDamage = totalAD - baseDamage;
                    baseDamage *= 0.6f;
                    level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    bonusDamage = this.effect0[level];
                    baseDamage += bonusDamage;
                    ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    nextBuffVars_MoveSpeedMod = this.effect1[level];
                    AddBuff(attacker, target, new Buffs.TalonSlow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                    if(level >= 1)
                    {
                        nextBuffVars_MovementSpeedMod = this.effect2[level];
                    }
                }
                else
                {
                    if(target is Champion)
                    {
                        SpellEffectCreate(out part, out _, "talon_w_tar.troy", default, ownerTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, true, false, false, false, false);
                        AddBuff((ObjAIBase)target, target, new Buffs.TalonRakeMissileOneMarker(), 9, 1, 0.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(target);
                        baseDamage = GetBaseAttackDamage(owner);
                        totalAD = GetTotalAttackDamage(owner);
                        baseDamage = totalAD - baseDamage;
                        baseDamage *= 0.6f;
                        level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        bonusDamage = this.effect3[level];
                        baseDamage += bonusDamage;
                        ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                        level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        nextBuffVars_MoveSpeedMod = this.effect4[level];
                        AddBuff(attacker, target, new Buffs.TalonSlow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                        if(level >= 1)
                        {
                            nextBuffVars_MovementSpeedMod = this.effect5[level];
                        }
                    }
                    else
                    {
                        canSee = CanSeeTarget(owner, target);
                        if(canSee)
                        {
                            SpellEffectCreate(out part, out _, "talon_w_tar.troy", default, ownerTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, true, false, false, false, false);
                            AddBuff((ObjAIBase)target, target, new Buffs.TalonRakeMissileOneMarker(), 9, 1, 0.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                            BreakSpellShields(target);
                            baseDamage = GetBaseAttackDamage(owner);
                            totalAD = GetTotalAttackDamage(owner);
                            baseDamage = totalAD - baseDamage;
                            baseDamage *= 0.6f;
                            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            bonusDamage = this.effect6[level];
                            baseDamage += bonusDamage;
                            ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            nextBuffVars_MoveSpeedMod = this.effect7[level];
                            AddBuff(attacker, target, new Buffs.TalonSlow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                            if(level >= 1)
                            {
                                nextBuffVars_MovementSpeedMod = this.effect8[level];
                            }
                        }
                    }
                }
            }
        }
    }
}