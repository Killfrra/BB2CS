#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolibearHatred : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VolibearHatred",
            BuffTextureName = "VolibearHatred.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.VolibearHatredZilean)) > 0)
            {
                damageAmount *= 1.01f;
            }
        }
        public override void OnKill()
        {
            if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.VolibearHatredZilean)) > 0)
            {
                AddBuff(attacker, attacker, new Buffs.VolibearKillsZilean(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                IncGold(attacker, 11);
            }
        }
    }
}