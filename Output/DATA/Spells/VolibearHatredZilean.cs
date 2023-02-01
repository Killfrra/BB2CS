#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolibearHatredZilean : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VolibearHatredZilean",
            BuffTextureName = "VolibearHatredZilean.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnKill()
        {
            if(GetBuffCountFromCaster(target, target, nameof(Buffs.VolibearHatred)) > 0)
            {
                IncGold(attacker, 10);
            }
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(GetBuffCountFromCaster(target, target, nameof(Buffs.VolibearHatred)) > 0)
            {
                damageAmount *= 1.01f;
            }
        }
    }
}