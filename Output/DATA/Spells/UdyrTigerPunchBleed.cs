#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UdyrTigerPunchBleed : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UdyrTigerPunchBleed",
            BuffTextureName = "Udyr_TigerStance.dds",
            IsDeathRecapSource = true,
        };
        float dotDamage;
        float lastTimeExecuted;
        public UdyrTigerPunchBleed(float dotDamage = default)
        {
            this.dotDamage = dotDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.dotDamage);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, this.dotDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 0, false, false, attacker);
            }
        }
    }
}