#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VoidStone : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VoidStone",
            BuffTextureName = "Kassadin_VoidStone.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int attackSpeedBoost; // UNUSED
        public override void OnActivate()
        {
            this.attackSpeedBoost = 0;
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageType == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                if(damageAmount > 0)
                {
                    float attackSpeedBoost; // UNUSED
                    attackSpeedBoost = damageAmount * 0.0015f;
                    damageAmount *= charVars.MagicAbsorb;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.VoidStoneAttackSpeedBoost(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
    }
}