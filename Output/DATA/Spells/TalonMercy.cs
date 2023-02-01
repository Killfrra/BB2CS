#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TalonMercy : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "", "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "BladeRogue_CheatDeath",
            BuffTextureName = "22.dds",
            PersistsThroughDeath = true,
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            bool isCCD;
            if(target is ObjAIBase)
            {
                if(HasBuffOfType(target, BuffType.SLOW))
                {
                    isCCD = true;
                }
                if(HasBuffOfType(target, BuffType.STUN))
                {
                    isCCD = true;
                }
                if(HasBuffOfType(target, BuffType.CHARM))
                {
                    isCCD = true;
                }
                if(HasBuffOfType(target, BuffType.SUPPRESSION))
                {
                    isCCD = true;
                }
                if(isCCD)
                {
                    damageAmount *= 1.1f;
                }
            }
        }
    }
}