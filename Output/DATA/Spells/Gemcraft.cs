#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Gemcraft : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Gemcraft",
            BuffTextureName = "GemKnight_Gemcraft.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    float damagePercent;
                    damagePercent = 0.075f * damageAmount;
                    IncPAR(owner, damagePercent);
                }
            }
        }
    }
}