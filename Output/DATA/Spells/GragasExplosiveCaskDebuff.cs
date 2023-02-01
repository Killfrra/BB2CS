#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GragasExplosiveCaskDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "GragasExplosiveCaskSlow",
            BuffTextureName = "GragasExplosiveCask.dds",
        };
        float aSDebuff;
        public GragasExplosiveCaskDebuff(float aSDebuff = default)
        {
            this.aSDebuff = aSDebuff;
        }
        public override void OnActivate()
        {
            //RequireVar(this.aSDebuff);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeAttackSpeedMod(owner, this.aSDebuff);
        }
    }
}