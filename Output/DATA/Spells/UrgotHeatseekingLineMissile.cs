#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class UrgotHeatseekingLineMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {10, 40, 70, 100, 130};
        float[] effect1 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        float[] effect2 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        float[] effect3 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float baseDamage;
            float attackDamage;
            float bonusAD;
            float totalDamage;
            bool isStealthed;
            float nextBuffVars_MoveSpeedMod;
            Particle asdf1; // UNUSED
            Particle asdf; // UNUSED
            teamID = GetTeamID(attacker);
            baseDamage = this.effect0[level];
            attackDamage = GetTotalAttackDamage(owner);
            bonusAD = 0.85f * attackDamage;
            totalDamage = baseDamage + bonusAD;
            isStealthed = GetStealthed(target);
            if(!isStealthed)
            {
                BreakSpellShields(target);
                ApplyDamage((ObjAIBase)owner, target, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UrgotTerrorCapacitorActive2)) > 0)
                {
                    level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    nextBuffVars_MoveSpeedMod = this.effect1[level];
                    AddBuff(attacker, target, new Buffs.UrgotSlow(), 100, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
                DestroyMissile(missileNetworkID);
                SpellEffectCreate(out asdf1, out _, "BloodSlash.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                SpellEffectCreate(out asdf, out _, "UrgotHeatSeekingMissile_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                AddBuff((ObjAIBase)owner, target, new Buffs.UrgotEntropyPassive(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            else
            {
                if(target is Champion)
                {
                    BreakSpellShields(target);
                    ApplyDamage((ObjAIBase)owner, target, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UrgotTerrorCapacitorActive2)) > 0)
                    {
                        level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        nextBuffVars_MoveSpeedMod = this.effect2[level];
                        AddBuff(attacker, target, new Buffs.UrgotSlow(), 100, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                    }
                    DestroyMissile(missileNetworkID);
                    SpellEffectCreate(out asdf1, out _, "BloodSlash.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                    SpellEffectCreate(out asdf, out _, "UrgotHeatSeekingMissile_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                    AddBuff((ObjAIBase)owner, target, new Buffs.UrgotEntropyPassive(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
                else
                {
                    bool canSee;
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        BreakSpellShields(target);
                        ApplyDamage((ObjAIBase)owner, target, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UrgotTerrorCapacitorActive2)) > 0)
                        {
                            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            nextBuffVars_MoveSpeedMod = this.effect3[level];
                            AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 1.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                        }
                        DestroyMissile(missileNetworkID);
                        SpellEffectCreate(out asdf1, out _, "BloodSlash.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, default, default, false, false);
                        SpellEffectCreate(out asdf, out _, "UrgotHeatSeekingMissile_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                        AddBuff((ObjAIBase)owner, target, new Buffs.UrgotEntropyPassive(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}