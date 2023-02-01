#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickRARemovePet : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        public override void OnDeactivate(bool expired)
        {
            float duration;
            duration = GetBuffRemainingDuration(owner, nameof(Buffs.YorickRARemovePet));
            if(duration > 0.5f)
            {
                if(GetBuffCountFromCaster(attacker, owner, nameof(Buffs.YorickRAPetBuff2)) > 0)
                {
                    ApplyDamage(attacker, attacker, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, attacker);
                }
            }
        }
    }
}