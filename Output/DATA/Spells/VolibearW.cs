#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VolibearW : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {80, 125, 170, 215, 260};
        public override bool CanCast()
        {
            bool returnValue = true;
            int count;
            returnValue = false;
            count = GetBuffCountFromAll(owner, nameof(Buffs.VolibearWStats));
            if(count == 3)
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            bool debuffFound; // UNUSED
            TeamId teamID;
            Particle part1; // UNUSED
            Particle part2; // UNUSED
            float damage;
            float hPPoolMod;
            float maxHP;
            float currentHP;
            float missingHP;
            float missingHPPerc;
            debuffFound = false;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out part1, out part2, "VolibearW_tar.troy", "VolibearW_tar.troy", teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, teamID, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            BreakSpellShields(target);
            damage = this.effect0[level];
            hPPoolMod = GetFlatHPPoolMod(attacker);
            hPPoolMod *= 0.15f;
            damage += hPPoolMod;
            maxHP = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            currentHP = GetHealth(target, PrimaryAbilityResourceType.MANA);
            missingHP = maxHP - currentHP;
            missingHPPerc = missingHP / maxHP;
            missingHPPerc++;
            damage *= missingHPPerc;
            ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class VolibearW : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "VolibearWBuff",
            BuffTextureName = "DrMundo_Masochism.dds",
            PersistsThroughDeath = true,
        };
        float[] effect0 = {0.08f, 0.11f, 0.14f, 0.17f, 0.2f};
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    TeamId teamID; // UNUSED
                    int level;
                    float nextBuffVars_VolibearWAS;
                    int count;
                    teamID = GetTeamID(owner);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    nextBuffVars_VolibearWAS = this.effect0[level];
                    AddBuff(attacker, attacker, new Buffs.VolibearWStats(nextBuffVars_VolibearWAS), 3, 1, 4.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    count = GetBuffCountFromAll(attacker, nameof(Buffs.VolibearWStats));
                    if(count >= 3)
                    {
                        AddBuff(attacker, attacker, new Buffs.VolibearWParticle(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                    }
                    UpdateCanCast(attacker);
                }
            }
        }
    }
}