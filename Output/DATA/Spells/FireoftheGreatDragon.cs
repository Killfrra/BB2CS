#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FireoftheGreatDragon : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "FireoftheGreatDragon",
            BuffTextureName = "Annie_Incinerate.dds",
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    AddBuff(attacker, target, new Buffs.Burning(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0);
                }
            }
        }
    }
}