#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SonaAriaPCDeathRecapFix : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
        float totalDamage;
        public SonaAriaPCDeathRecapFix(float totalDamage = default)
        {
            this.totalDamage = totalDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.totalDamage);
            ApplyDamage(attacker, owner, this.totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
        }
    }
}