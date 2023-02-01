#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ToxicShotParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "Global_poison.troy", },
            BuffName = "Toxic Shot",
            BuffTextureName = "Teemo_PoisonedDart.dds",
            IsDeathRecapSource = true,
        };
        float damagePerTick;
        float damagePerTickFirst;
        float lastTimeExecuted;
        public ToxicShotParticle(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            this.damagePerTickFirst = this.damagePerTick * 1.5f;
            ApplyDamage(attacker, owner, this.damagePerTickFirst, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0.14f, 1, false, false, attacker);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                ApplyDamage(attacker, owner, this.damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0.14f, 1, false, false, attacker);
            }
        }
    }
}