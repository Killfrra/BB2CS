#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BouncingBladeMS : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "global_grievousWound_tar.troy", },
            BuffName = "BouncingBladeMS",
            BuffTextureName = "Katarina_BouncingBlade.dds",
        };
        public override void OnActivate()
        {
            IncPermanentPercentHPRegenMod(owner, -0.5f);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentPercentHPRegenMod(owner, 0.5f);
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            float effectiveHeal;
            if(health >= 0)
            {
                effectiveHeal = health * 0.5f;
                returnValue = effectiveHeal;
            }
            return returnValue;
        }
    }
}