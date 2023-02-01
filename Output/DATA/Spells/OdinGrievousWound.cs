#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinGrievousWound : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinGrievousWound",
            BuffTextureName = "GW_Debuff.dds",
            PersistsThroughDeath = true,
        };
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            float effectiveHeal;
            if(health >= 0)
            {
                effectiveHeal = health * 0.8f;
                returnValue = effectiveHeal;
            }
            return returnValue;
        }
    }
}