#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UrgotCorrosiveDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "UrgotCorrosiveDamage",
            BuffTextureName = "UrgotCorrosiveCharge.dds",
            IsDeathRecapSource = true,
        };
        Particle particle1;
        float tickDamage;
        float lastTimeExecuted;
        public UrgotCorrosiveDebuff(float tickDamage = default)
        {
            this.tickDamage = tickDamage;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particle1, out _, "UrgotCorrosiveDebuff_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            //RequireVar(this.tickDamage);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle1);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, this.tickDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
        }
    }
}