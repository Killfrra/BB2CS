#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class TalonShadowAssaultMisOne : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {120, 190, 260, 85, 100};
        int[] effect1 = {120, 190, 260, 85, 100};
        int[] effect2 = {120, 190, 260, 85, 100};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int count;
            TeamId ownerTeam;
            count = GetBuffCountFromCaster(target, target, nameof(Buffs.TalonShadowAssaultMisBuff));
            ownerTeam = GetTeamID(owner);
            if(count == 0)
            {
                bool isStealthed;
                Particle part; // UNUSED
                float baseDamage;
                float totalAD;
                float bonusDamage;
                isStealthed = GetStealthed(target);
                if(!isStealthed)
                {
                    SpellEffectCreate(out part, out _, "talon_ult_tar.troy", default, ownerTeam ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, true, false, false, false, false);
                    AddBuff((ObjAIBase)target, target, new Buffs.TalonShadowAssaultMisBuff(), 9, 1, 0.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(target);
                    baseDamage = GetBaseAttackDamage(owner);
                    totalAD = GetTotalAttackDamage(owner);
                    baseDamage = totalAD - baseDamage;
                    baseDamage *= 0.9f;
                    level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    bonusDamage = this.effect0[level];
                    baseDamage += bonusDamage;
                    ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                }
                else
                {
                    if(target is Champion)
                    {
                        SpellEffectCreate(out part, out _, "talon_ult_tar.troy", default, ownerTeam ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, true, false, false, false, false);
                        AddBuff((ObjAIBase)target, target, new Buffs.TalonShadowAssaultMisBuff(), 9, 1, 0.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(target);
                        baseDamage = GetBaseAttackDamage(owner);
                        totalAD = GetTotalAttackDamage(owner);
                        baseDamage = totalAD - baseDamage;
                        baseDamage *= 0.9f;
                        level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        bonusDamage = this.effect1[level];
                        baseDamage += bonusDamage;
                        ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                        level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    }
                    else
                    {
                        bool canSee;
                        canSee = CanSeeTarget(owner, target);
                        if(canSee)
                        {
                            SpellEffectCreate(out part, out _, "bowmaster_BasicAttack_tar.troy", default, ownerTeam ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, true, false, false, false, false);
                            AddBuff((ObjAIBase)target, target, new Buffs.TalonShadowAssaultMisBuff(), 9, 1, 0.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                            BreakSpellShields(target);
                            baseDamage = GetBaseAttackDamage(owner);
                            totalAD = GetTotalAttackDamage(owner);
                            baseDamage = totalAD - baseDamage;
                            baseDamage *= 0.9f;
                            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            bonusDamage = this.effect2[level];
                            baseDamage += bonusDamage;
                            ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class TalonShadowAssaultMisOne : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "BladeRogue_AOE_Knives",
        };
        int[] effect0 = {75, 65, 55, 13, 12};
        public override void OnDeactivate(bool expired)
        {
            int level;
            Vector3 ownerPos;
            float cooldownVal;
            float flatCDVal;
            float flatCD;
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.TalonShadowAssault));
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            ownerPos = GetUnitPosition(owner);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, ownerPos, 3000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.NotAffectSelf | SpellDataFlags.AffectUntargetable, default, true))
            {
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.TalonShadowAssaultMarker)) > 0)
                {
                    Vector3 unitPos;
                    float newDistance;
                    unitPos = GetUnitPosition(unit);
                    newDistance = DistanceBetweenObjects("Owner", "Unit");
                    if(newDistance > 100)
                    {
                        SpellCast((ObjAIBase)owner, owner, ownerPos, default, 4, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, unitPos);
                    }
                    SpellBuffRemove(unit, nameof(Buffs.TalonShadowAssaultMarker), (ObjAIBase)owner, 0);
                    SetInvulnerable(unit, false);
                    SetTargetable(unit, true);
                    ApplyDamage((ObjAIBase)unit, unit, 50000, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 0, false, false, attacker);
                }
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cooldownVal = this.effect0[level];
            flatCDVal = 0;
            flatCD = GetPercentCooldownMod(owner);
            flatCDVal = cooldownVal * flatCD;
            cooldownVal += flatCDVal;
            SetSlotSpellCooldownTimeVer2(cooldownVal, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            charVars.HasCastR = false;
            SetCanCast(owner, true);
        }
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            if(spellName == nameof(Spells.TalonShadowAssaultMisOne))
            {
                Vector3 ownerPos;
                TeamId teamOfOwner;
                Minion other3;
                ownerPos = GetUnitPosition(owner);
                teamOfOwner = GetTeamID(owner);
                other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", missileEndPosition, teamOfOwner ?? TeamId.TEAM_CASTER, false, true, false, false, false, true, 0, false, true, (Champion)owner);
                FaceDirection(other3, ownerPos);
                SetInvulnerable(other3, true);
                SetTargetable(other3, false);
                charVars.SAMissileNumber++;
                if(charVars.SAMissileNumber > 8)
                {
                    charVars.SAMissileNumber = 1;
                }
                AddBuff((ObjAIBase)owner, other3, new Buffs.TalonShadowAssaultMarker(), 1, 1, 100000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                DestroyMissile(charVars.MISSILEID);
                SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                SetCanCast(owner, true);
            }
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            SpellMissile missileId; // UNITIALIZED
            charVars.MISSILEID = missileId;
        }
    }
}