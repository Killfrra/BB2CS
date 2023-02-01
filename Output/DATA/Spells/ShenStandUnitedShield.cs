#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShenStandUnitedShield : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.prt",
            BuffName = "Shen Stand United Shield",
            BuffTextureName = "Shen_StandUnited.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };
        float shieldHealth;
        Particle shieldz;
        float oldArmorAmount;
        public ShenStandUnitedShield(float shieldHealth = default)
        {
            this.shieldHealth = shieldHealth;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            //RequireVar(this.shieldHealth);
            SetBuffToolTipVar(1, this.shieldHealth);
            ApplyAssistMarker(attacker, owner, 10);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.shieldz, out _, "Shen_StandUnited_shield_v2.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            IncreaseShield(owner, this.shieldHealth, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.shieldz);
            if(this.shieldHealth > 0)
            {
                RemoveShield(owner, this.shieldHealth, true, true);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            this.oldArmorAmount = this.shieldHealth;
            if(this.shieldHealth >= damageAmount)
            {
                this.shieldHealth -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.shieldHealth;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SetBuffToolTipVar(1, this.shieldHealth);
            }
            else
            {
                damageAmount -= this.shieldHealth;
                this.shieldHealth = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}