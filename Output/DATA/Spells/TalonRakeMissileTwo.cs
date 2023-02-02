#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class TalonRakeMissileTwo : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {30, 55, 80, 105, 130};
        float[] effect1 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        int[] effect2 = {30, 55, 80, 105, 130};
        float[] effect3 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        int[] effect4 = {30, 55, 80, 105, 130};
        float[] effect5 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int count;
            TeamId ownerTeam;
            TeamId targetTeam;
            count = GetBuffCountFromCaster(target, target, nameof(Buffs.TalonRakeMissileTwo));
            ownerTeam = GetTeamID(owner);
            targetTeam = GetTeamID(target);
            if(targetTeam != ownerTeam)
            {
                if(count == 0)
                {
                    bool isStealthed;
                    Particle part; // UNUSED
                    float nextBuffVars_MoveSpeedMod;
                    float baseDamage;
                    float totalAD;
                    float bonusDamage;
                    isStealthed = GetStealthed(target);
                    if(!isStealthed)
                    {
                        SpellEffectCreate(out part, out _, "talon_w_tar.troy", default, ownerTeam ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, true, false, false, false, false);
                        AddBuff((ObjAIBase)target, target, new Buffs.TalonRakeMissileTwo(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
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
                    }
                    else
                    {
                        if(target is Champion)
                        {
                            SpellEffectCreate(out part, out _, "talon_w_tar.troy", default, ownerTeam ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, true, false, false, false, false);
                            AddBuff((ObjAIBase)target, target, new Buffs.TalonRakeMissileTwo(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                            BreakSpellShields(target);
                            baseDamage = GetBaseAttackDamage(owner);
                            totalAD = GetTotalAttackDamage(owner);
                            baseDamage = totalAD - baseDamage;
                            baseDamage *= 0.6f;
                            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            bonusDamage = this.effect2[level];
                            baseDamage += bonusDamage;
                            ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            nextBuffVars_MoveSpeedMod = this.effect3[level];
                            AddBuff(attacker, target, new Buffs.TalonSlow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                        }
                        else
                        {
                            bool canSee;
                            canSee = CanSeeTarget(owner, target);
                            if(canSee)
                            {
                                SpellEffectCreate(out part, out _, "talon_w_tar.troy", default, ownerTeam ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, true, false, false, false, false);
                                AddBuff((ObjAIBase)target, target, new Buffs.TalonRakeMissileTwo(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                                BreakSpellShields(target);
                                baseDamage = GetBaseAttackDamage(owner);
                                totalAD = GetTotalAttackDamage(owner);
                                baseDamage = totalAD - baseDamage;
                                baseDamage *= 0.6f;
                                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                                bonusDamage = this.effect4[level];
                                baseDamage += bonusDamage;
                                ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                                nextBuffVars_MoveSpeedMod = this.effect5[level];
                                AddBuff(attacker, target, new Buffs.TalonSlow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
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
namespace Buffs
{
    public class TalonRakeMissileTwo : BBBuffScript
    {
    }
}