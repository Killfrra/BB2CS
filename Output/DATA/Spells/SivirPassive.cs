#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SivirPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SivirPassive",
            BuffTextureName = "Sivir_Sprint.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is Champion)
            {
                AddBuff(attacker, attacker, new Buffs.SivirPassiveSpeed(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}