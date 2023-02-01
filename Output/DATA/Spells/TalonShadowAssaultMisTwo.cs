#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TalonShadowAssaultMisTwo : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TalonShadowAssaultMisTwo",
        };
    }
}
namespace Spells
{
    public class TalonShadowAssaultMisTwo : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {120, 190, 260, 85, 100};
        int[] effect1 = {120, 190, 260, 85, 100};
        int[] effect2 = {120, 190, 260, 85, 100};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int count;
            TeamId ownerTeam;
            TeamId targetTeam;
            bool isStealthed;
            Particle part; // UNUSED
            float baseDamage;
            float totalAD;
            float bonusDamage;
            bool canSee;
            count = GetBuffCountFromCaster(target, target, nameof(Buffs.TalonShadowAssaultMisTwo));
            ownerTeam = GetTeamID(owner);
            targetTeam = GetTeamID(target);
            if(targetTeam != ownerTeam)
            {
                if(count == 0)
                {
                    isStealthed = GetStealthed(target);
                    if(!isStealthed)
                    {
                        SpellEffectCreate(out part, out _, "talon_ult_tar.troy", default, ownerTeam, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, true, false, false, false, false);
                        AddBuff((ObjAIBase)target, target, new Buffs.TalonShadowAssaultMisTwo(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
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
                            SpellEffectCreate(out part, out _, "talon_ult_tar.troy", default, ownerTeam, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, true, false, false, false, false);
                            AddBuff((ObjAIBase)target, target, new Buffs.TalonShadowAssaultMisTwo(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
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
                            canSee = CanSeeTarget(owner, target);
                            if(canSee)
                            {
                                SpellEffectCreate(out part, out _, "bowmaster_BasicAttack_tar.troy", default, ownerTeam, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, true, false, false, false, false);
                                AddBuff((ObjAIBase)target, target, new Buffs.TalonShadowAssaultMisTwo(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                                BreakSpellShields(target);
                                baseDamage = GetBaseAttackDamage(owner);
                                totalAD = GetTotalAttackDamage(owner);
                                baseDamage = totalAD - baseDamage;
                                baseDamage *= 0.9f;
                                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                                bonusDamage = this.effect2[level];
                                baseDamage += bonusDamage;
                                DebugSay(owner, "DAMAGE", baseDamage);
                                ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            }
                        }
                    }
                }
            }
            else if(target == owner)
            {
                DestroyMissile(missileNetworkID);
            }
        }
    }
}