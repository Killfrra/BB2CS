#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinBloodbursterBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinBloodbursterBuff",
            BuffTextureName = "3181_SanguineBlade.dds",
        };
        float aDBuff;
        float lSBuff;
        public override void OnActivate()
        {
            this.aDBuff = 5;
            this.lSBuff = 0.01f;
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.aDBuff);
            IncPercentLifeStealMod(owner, this.lSBuff);
        }
    }
}