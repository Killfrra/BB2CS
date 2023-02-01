#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SkarnerFractureMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SkarnerFracture",
            BuffTextureName = "SkarnerFracture.dds",
        };
        Particle particle1;
        int[] effect0 = {30, 45, 60, 75, 90};
        float[] effect1 = {1, 0.5f, 0.25f, 0.125f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f};
        public override void OnActivate()
        {
            ObjAIBase caster;
            TeamId teamID;
            caster = SetBuffCasterUnit();
            teamID = GetTeamID(caster);
            SpellEffectCreate(out this.particle1, out _, "Skarner_Fracture_Tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle1);
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            ObjAIBase caster;
            int count;
            float duration;
            TeamId teamID;
            float level;
            float healingAmount;
            float aPStat;
            float bonusHeal;
            float healingMod;
            Particle motaExplosion; // UNUSED
            Particle healVFX; // UNUSED
            caster = SetBuffCasterUnit();
            if(caster == attacker)
            {
                count = GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.SkarnerFracture));
                duration = GetBuffRemainingDuration(owner, nameof(Buffs.SkarnerFractureMissile));
                teamID = GetTeamID(attacker);
                SpellBuffClear(owner, nameof(Buffs.SkarnerFractureMissile));
                level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                healingAmount = this.effect0[level];
                aPStat = GetFlatMagicDamageMod(attacker);
                bonusHeal = aPStat * 0.3f;
                healingAmount += bonusHeal;
                level = count;
                level++;
                healingMod = this.effect1[level];
                healingAmount *= healingMod;
                SpellEffectCreate(out motaExplosion, out _, "Skarner_Fracture_Tar_Consume.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                IncHealth(attacker, healingAmount, attacker);
                SpellEffectCreate(out healVFX, out _, "galio_bulwark_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, default, default, attacker, default, default, true, false, false, false, false);
                AddBuff(attacker, attacker, new Buffs.SkarnerFracture(), 8, 1, duration, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Spells
{
    public class SkarnerFractureMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {80, 120, 160, 200, 240};
        int[] effect1 = {30, 45, 60, 75, 90};
        float[] effect2 = {1, 0.5f, 0.25f, 0.125f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f, 0.0625f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int count;
            TeamId teamID;
            float healingAmount;
            float aPStat;
            float bonusHeal;
            float healingMod;
            Particle motaExplosion; // UNUSED
            Particle healVFX; // UNUSED
            BreakSpellShields(target);
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
            if(target.IsDead)
            {
                count = GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.SkarnerFracture));
                teamID = GetTeamID(attacker);
                level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                healingAmount = this.effect1[level];
                aPStat = GetFlatMagicDamageMod(attacker);
                bonusHeal = aPStat * 0.3f;
                healingAmount += bonusHeal;
                level = count;
                level++;
                healingMod = this.effect2[level];
                healingAmount *= healingMod;
                SpellEffectCreate(out motaExplosion, out _, "Skarner_Fracture_Tar_Consume.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                IncHealth(attacker, healingAmount, attacker);
                SpellEffectCreate(out healVFX, out _, "galio_bulwark_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, default, default, attacker, default, default, true, false, false, false, false);
                AddBuff(attacker, attacker, new Buffs.SkarnerFracture(), 8, 1, 6, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                AddBuff(attacker, target, new Buffs.SkarnerFractureMissile(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
    }
}