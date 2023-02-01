#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UdyrTurtleActivation : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UdyrTurtleActivation",
            BuffTextureName = "Udyr_TurtleStance.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };
        float shieldAmount;
        Particle turtleparticle;
        Particle turtleShield;
        float oldArmorAmount;
        public UdyrTurtleActivation(float shieldAmount = default)
        {
            this.shieldAmount = shieldAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.shieldAmount);
            SpellEffectCreate(out this.turtleparticle, out _, "TurtleStance.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            SpellEffectCreate(out this.turtleShield, out _, "TurtleStance_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            IncreaseShield(owner, this.shieldAmount, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.turtleShield);
            SpellEffectRemove(this.turtleparticle);
            if(this.shieldAmount > 0)
            {
                RemoveShield(owner, this.shieldAmount, true, true);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            this.oldArmorAmount = this.shieldAmount;
            if(this.shieldAmount >= damageAmount)
            {
                this.shieldAmount -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.shieldAmount;
                ReduceShield(owner, this.oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= this.shieldAmount;
                this.shieldAmount = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}