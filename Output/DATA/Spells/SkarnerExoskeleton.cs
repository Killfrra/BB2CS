#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SkarnerExoskeleton : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {70, 115, 160, 205, 250};
        float[] effect1 = {0.15f, 0.17f, 0.19f, 0.21f, 0.23f};
        float[] effect2 = {0.3f, 0.35f, 0.4f, 0.45f, 0.5f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamageBlock;
            float abilityPower;
            float bonusHealth;
            float damageBlock;
            float nextBuffVars_DamageBlock;
            float nextBuffVars_MSBonus;
            float nextBuffVars_ASBonus;
            baseDamageBlock = this.effect0[level];
            PlayAnimation("Spell2", 0, owner, false, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            abilityPower = GetFlatMagicDamageMod(owner);
            bonusHealth = abilityPower * 0.6f;
            damageBlock = baseDamageBlock + bonusHealth;
            nextBuffVars_DamageBlock = damageBlock;
            nextBuffVars_MSBonus = this.effect1[level];
            nextBuffVars_ASBonus = this.effect2[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.SkarnerExoskeleton(nextBuffVars_DamageBlock, nextBuffVars_MSBonus, nextBuffVars_ASBonus), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class SkarnerExoskeleton : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "R_sub_hand", "L_sub_hand", "chest", "tail", },
            AutoBuffActivateEffect = new[]{ "Skarner_Exoskeleton_buf_r_arm.troy", "Skarner_Exoskeleton_buf_l_arm.troy", "Skarner_Exoskeleton_body.troy", "Skarner_Exoskeleton_tail.troy", },
            BuffName = "SkarnerExoskeleton",
            BuffTextureName = "SkarnerExoskeleton.dds",
        };
        float damageBlock;
        float mSBonus;
        float aSBonus;
        Particle partname; // UNUSED
        float oldArmorAmount;
        public SkarnerExoskeleton(float damageBlock = default, float mSBonus = default, float aSBonus = default)
        {
            this.damageBlock = damageBlock;
            this.mSBonus = mSBonus;
            this.aSBonus = aSBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageBlock);
            //RequireVar(this.mSBonus);
            //RequireVar(this.aSBonus);
            IncreaseShield(owner, this.damageBlock, true, true);
            IncPercentMovementSpeedMod(owner, this.mSBonus);
            IncPercentAttackSpeedMod(owner, this.aSBonus);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID; // UNUSED
            if(this.damageBlock > 0)
            {
                RemoveShield(owner, this.damageBlock, true, true);
            }
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.partname, out _, "Skarner_Exoskeleon_Shatter.troy", default, TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, false, false, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.mSBonus);
            IncPercentAttackSpeedMod(owner, this.aSBonus);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            this.oldArmorAmount = this.damageBlock;
            if(this.damageBlock >= damageAmount)
            {
                this.damageBlock -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.damageBlock;
                ReduceShield(owner, this.oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= this.damageBlock;
                this.damageBlock = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}