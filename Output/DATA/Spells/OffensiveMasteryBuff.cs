#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OffensiveMasteryBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "FortifyBuff",
            BuffTextureName = "Summoner_fortify.dds",
        };
        int level;
        int[] effect0 = {2, 4};
        public OffensiveMasteryBuff(int level = default)
        {
            this.level = level;
        }
        public override void OnActivate()
        {
            //RequireVar(this.level);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int level;
            float damageBonus;
            level = this.level;
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    if(target is Champion)
                    {
                    }
                    else
                    {
                        damageBonus = this.effect0[level];
                        damageAmount += damageBonus;
                    }
                }
            }
        }
    }
}