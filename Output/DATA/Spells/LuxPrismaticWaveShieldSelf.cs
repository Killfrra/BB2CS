#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LuxPrismaticWaveShieldSelf : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "LuxPrismaticWave_shieldself.troy", },
            BuffName = "LuxShield",
            BuffTextureName = "LuxPrismaWrap.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };
        float damageBlock;
        bool willRemove;
        float oldArmorAmount;
        public LuxPrismaticWaveShieldSelf(float damageBlock = default, bool willRemove = default)
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
                SpellEffectCreate(out ar, out _, "shen_Feint_self_deactivate.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
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
                SpellEffectCreate(out ar, out _, "SpellEffect_proc.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}