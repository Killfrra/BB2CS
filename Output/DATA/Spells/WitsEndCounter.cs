#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WitsEndCounter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "WitsEndBuff",
            BuffTextureName = "3091_Wits_End.dds",
        };
        public override void OnActivate()
        {
            int count;
            float resistanceBuff;
            count = GetBuffCountFromAll(owner, nameof(Buffs.WitsEndCounter));
            resistanceBuff = count * 5;
            SetBuffToolTipVar(1, resistanceBuff);
        }
    }
}