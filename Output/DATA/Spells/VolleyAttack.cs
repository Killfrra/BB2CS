#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolleyAttack : BBBuffScript
    {
    }
}
namespace Spells
{
    public class VolleyAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {40, 50, 60, 70, 80};
        float[] effect1 = {-0.15f, -0.2f, -0.25f, -0.3f, -0.35f};
        int[] effect2 = {40, 50, 60, 70, 80};
        float[] effect3 = {-0.15f, -0.2f, -0.25f, -0.3f, -0.35f};
        int[] effect4 = {40, 50, 60, 70, 80};
        float[] effect5 = {-0.15f, -0.2f, -0.25f, -0.3f, -0.35f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int count;
            bool isStealthed;
            Particle part; // UNUSED
            float nextBuffVars_MovementSpeedMod;
            float baseDamage;
            float bonusDamage;
            bool canSee;
            count = GetBuffCountFromCaster(target, target, nameof(Buffs.VolleyAttack));
            if(count == 0)
            {
                isStealthed = GetStealthed(target);
                if(!isStealthed)
                {
                    SpellEffectCreate(out part, out _, "bowmaster_BasicAttack_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, false);
                    AddBuff((ObjAIBase)target, target, new Buffs.VolleyAttack(), 9, 1, 0.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false);
                    BreakSpellShields(target);
                    baseDamage = GetBaseAttackDamage(owner);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    bonusDamage = this.effect0[level];
                    baseDamage += bonusDamage;
                    ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(level >= 1)
                    {
                        nextBuffVars_MovementSpeedMod = this.effect1[level];
                        AddBuff((ObjAIBase)owner, target, new Buffs.FrostArrow(nextBuffVars_MovementSpeedMod), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false);
                    }
                    DestroyMissile(missileNetworkID);
                }
                else
                {
                    if(target is Champion)
                    {
                        SpellEffectCreate(out part, out _, "bowmaster_BasicAttack_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, false);
                        AddBuff((ObjAIBase)target, target, new Buffs.VolleyAttack(), 9, 1, 0.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false);
                        BreakSpellShields(target);
                        baseDamage = GetBaseAttackDamage(owner);
                        level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        bonusDamage = this.effect2[level];
                        baseDamage += bonusDamage;
                        ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false);
                        level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        if(level >= 1)
                        {
                            nextBuffVars_MovementSpeedMod = this.effect3[level];
                            AddBuff((ObjAIBase)owner, target, new Buffs.FrostArrow(nextBuffVars_MovementSpeedMod), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false);
                        }
                        DestroyMissile(missileNetworkID);
                    }
                    else
                    {
                        canSee = CanSeeTarget(owner, target);
                        if(canSee)
                        {
                            SpellEffectCreate(out part, out _, "bowmaster_BasicAttack_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, target.Position, target, default, default, false);
                            AddBuff((ObjAIBase)target, target, new Buffs.VolleyAttack(), 9, 1, 0.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false);
                            BreakSpellShields(target);
                            baseDamage = GetBaseAttackDamage(owner);
                            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            bonusDamage = this.effect4[level];
                            baseDamage += bonusDamage;
                            ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false);
                            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            if(level >= 1)
                            {
                                nextBuffVars_MovementSpeedMod = this.effect5[level];
                                AddBuff((ObjAIBase)owner, target, new Buffs.FrostArrow(nextBuffVars_MovementSpeedMod), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false);
                            }
                            DestroyMissile(missileNetworkID);
                        }
                    }
                }
            }
        }
    }
}