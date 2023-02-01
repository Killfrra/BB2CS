#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DeathDefied : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Death Defied",
            BuffTextureName = "Lich_Untransmutable.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnDeath()
        {
            bool becomeZombie; // UNUSED
            if(owner is Champion)
            {
                becomeZombie = true;
            }
        }
        public override void OnZombie()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DeathDefiedBuff)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.DeathDefiedBuff(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DeathDefiedBuff)) > 0)
            {
                damageAmount = 0;
            }
        }
    }
}