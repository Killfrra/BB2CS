#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GalioBulwark : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.troy",
            BuffName = "GalioBulwark",
            BuffTextureName = "Galio_Bulwark.dds",
        };
        float bonusDefense;
        float healAmount;
        Particle targetVFX;
        Particle selfTargetVFX;
        public GalioBulwark(float bonusDefense = default, float healAmount = default)
        {
            this.bonusDefense = bonusDefense;
            this.healAmount = healAmount;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            //RequireVar(this.bonusDefense);
            //RequireVar(this.healAmount);
            teamID = GetTeamID(owner);
            if(owner != attacker)
            {
                ApplyAssistMarker(attacker, owner, 10);
                SpellEffectCreate(out this.targetVFX, out _, "galio_bullwark_target_shield_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.selfTargetVFX, out _, "galio_bullwark_target_shield_01_self.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "chest", default, owner, default, default, false, default, default, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if(owner != attacker)
            {
                SpellEffectRemove(this.targetVFX);
            }
            else
            {
                SpellEffectRemove(this.selfTargetVFX);
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.bonusDefense);
            IncFlatSpellBlockMod(owner, this.bonusDefense);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            ObjAIBase caster;
            float nextBuffVars_HealAmount;
            Particle galioHitVFX; // UNUSED
            Particle allyHitVFX; // UNUSED
            if(damageSource != default)
            {
                caster = SetBuffCasterUnit();
                nextBuffVars_HealAmount = this.healAmount;
                AddBuff(caster, caster, new Buffs.GalioBulwarkHeal(nextBuffVars_HealAmount), 1, 1, 0, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                this.healAmount *= 0.8f;
                this.healAmount = Math.Max(this.healAmount, 1);
                if(owner == caster)
                {
                    SpellEffectCreate(out galioHitVFX, out _, "galio_bullwark_shield_activate_self.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", default, owner, default, default, false, default, default, false, false);
                }
                else
                {
                    SpellEffectCreate(out allyHitVFX, out _, "galio_bullwark_shield_activate.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
                }
            }
        }
    }
}
namespace Spells
{
    public class GalioBulwark : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {30, 45, 60, 75, 90};
        int[] effect1 = {25, 40, 55, 70, 85};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_BonusDefense;
            float nextBuffVars_HealAmount;
            float baseHeal;
            float aPStat;
            float bonusHeal;
            float healAmount;
            nextBuffVars_BonusDefense = this.effect0[level];
            baseHeal = this.effect1[level];
            aPStat = GetFlatMagicDamageMod(owner);
            bonusHeal = aPStat * 0.3f;
            healAmount = baseHeal + bonusHeal;
            nextBuffVars_HealAmount = healAmount;
            AddBuff(attacker, target, new Buffs.GalioBulwark(nextBuffVars_BonusDefense, nextBuffVars_HealAmount), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}