#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OlafFrenziedStrikes : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "L_BUFFBONE_GLB_HAND_LOC", "R_BUFFBONE_GLB_HAND_LOC", "BUFFBONE_CSTM_WEAPON_4", "BUFFBONE_CSTM_WEAPON_2", },
            AutoBuffActivateEffect = new[]{ "olaf_viciousStrikes_self.troy", "olaf_viciousStrikes_self.troy", "olaf_viciousStrikes_axes_blood.troy", "olaf_viciousStrikes_axes_blood.troy", },
            BuffName = "OlafFrenziedStrikes",
            BuffTextureName = "OlafViciousStrikes.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellVOOverrideSkins = new[]{ "BroOlaf", },
        };
        Particle particleID; // UNUSED
        float damageGain;
        float lifestealStat;
        public OlafFrenziedStrikes(float damageGain = default, float lifestealStat = default)
        {
            this.damageGain = damageGain;
            this.lifestealStat = lifestealStat;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particleID, out _, "olaf_viciousStrikes_weapon_glow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_3", default, owner, "BUFFBONE_CSTM_WEAPON_2", default, false);
            SpellEffectCreate(out this.particleID, out _, "olaf_viciousStrikes_weapon_glow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, target, false, owner, "BUFFBONE_CSTM_WEAPON_7", default, owner, "BUFFBONE_CSTM_WEAPON_4", default, false);
            //RequireVar(this.damageGain);
            //RequireVar(this.lifestealStat);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.damageGain);
            IncPercentLifeStealMod(owner, this.lifestealStat);
            IncPercentSpellVampMod(owner, this.lifestealStat);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            Particle healParticle; // UNUSED
            SpellEffectCreate(out healParticle, out _, "olaf_viciousStrikes_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
        }
    }
}
namespace Spells
{
    public class OlafFrenziedStrikes : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.01f, 0.01f, 0.01f, 0.01f, 0.01f};
        int[] effect1 = {7, 14, 21, 28, 35};
        float[] effect2 = {0.09f, 0.12f, 0.15f, 0.18f, 0.21f};
        public override void SelfExecute()
        {
            float maxHealth;
            float healthPercent;
            float baseDamage;
            float nextBuffVars_LifestealStat;
            float nextBuffVars_DamageGain;
            float healthDamage;
            maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            healthPercent = this.effect0[level];
            baseDamage = this.effect1[level];
            nextBuffVars_LifestealStat = this.effect2[level];
            healthDamage = healthPercent * maxHealth;
            nextBuffVars_DamageGain = healthDamage + baseDamage;
            AddBuff((ObjAIBase)owner, owner, new Buffs.OlafFrenziedStrikes(nextBuffVars_DamageGain, nextBuffVars_LifestealStat), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}