#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlindMonkPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "L_hand", "R_hand", },
            AutoBuffActivateEffect = new[]{ "blindMonk_passive_buf.troy", "blindMonk_passive_buf.troy", },
            BuffName = "BlindMonkFlurry",
            BuffTextureName = "BlindMonkPassive.dds",
        };
        float totalHits;
        public override void OnActivate()
        {
            IncPercentAttackSpeedMod(owner, 0.5f);
            this.totalHits = 2;
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, 0.5f);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            IncPAR(owner, 15, PrimaryAbilityResourceType.Energy);
            this.totalHits--;
            if(this.totalHits == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}