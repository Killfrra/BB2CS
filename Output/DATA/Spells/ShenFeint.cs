#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShenFeint : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {50, 100, 150, 200, 250};
        float[] effect1 = {2.5f, 2.5f, 2.5f, 2.5f, 2.5f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamageBlock;
            float abilityPower;
            float bonusHealth;
            float damageBlock;
            float nextBuffVars_DamageBlock;
            baseDamageBlock = this.effect0[level];
            abilityPower = GetFlatMagicDamageMod(owner);
            bonusHealth = abilityPower * 0.75f;
            damageBlock = baseDamageBlock + bonusHealth;
            nextBuffVars_DamageBlock = damageBlock;
            AddBuff(attacker, target, new Buffs.ShenFeint(nextBuffVars_DamageBlock), 1, 1, this.effect1[level], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class ShenFeint : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "shen_Feint_self.troy", },
            BuffName = "Shen Feint Buff",
            BuffTextureName = "Shen_Feint.dds",
            OnPreDamagePriority = 3,
        };
        float damageBlock;
        bool willRemove;
        float oldArmorAmount;
        public ShenFeint(float damageBlock = default, bool willRemove = default)
        {
            this.damageBlock = damageBlock;
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageBlock);
            IncreaseShield(owner, this.damageBlock, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            if(!this.willRemove)
            {
                Particle ar; // UNUSED
                SpellEffectCreate(out ar, out _, "shen_Feint_self_deactivate.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
            }
            if(this.damageBlock > 0)
            {
                RemoveShield(owner, this.damageBlock, true, true);
            }
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
                TeamId teamID;
                Particle ar; // UNUSED
                teamID = GetTeamID(owner);
                damageAmount -= this.damageBlock;
                this.damageBlock = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellEffectCreate(out ar, out _, "shen_Feint_block.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}