#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FrozenHeartAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "FrozenHeartAura",
            BuffTextureName = "122_Frozen_Heart.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeAttackSpeedMod(owner, -0.2f);
        }
    }
}