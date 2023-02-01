#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ScurvyStrike : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ScurvyStrike",
            IsDeathRecapSource = true,
        };
        float dotDamage;
        float moveSpeedMod;
        float lastTimeExecuted;
        public ScurvyStrike(float dotDamage = default, float moveSpeedMod = default)
        {
            this.dotDamage = dotDamage;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.dotDamage);
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnUpdateActions()
        {
            int count;
            float damageToDeal;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                count = GetBuffCountFromCaster(owner, attacker, nameof(Buffs.ScurvyStrikeParticle));
                damageToDeal = this.dotDamage * count;
                ApplyDamage(attacker, owner, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            }
        }
        public override void OnUpdateStats()
        {
            int count;
            float totalSlow;
            count = GetBuffCountFromCaster(owner, attacker, nameof(Buffs.ScurvyStrikeParticle));
            totalSlow = this.moveSpeedMod * count;
            IncPercentMultiplicativeMovementSpeedMod(owner, totalSlow);
        }
    }
}