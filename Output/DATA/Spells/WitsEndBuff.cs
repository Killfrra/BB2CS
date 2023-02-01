#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WitsEndBuff : BBBuffScript
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
            count = GetBuffCountFromAll(owner, nameof(Buffs.WitsEndBuff));
            resistanceBuff = 5 * count;
            IncFlatSpellBlockMod(owner, resistanceBuff);
        }
        public override void OnUpdateStats()
        {
            int count;
            float resistanceBuff;
            count = GetBuffCountFromAll(owner, nameof(Buffs.WitsEndCounter));
            resistanceBuff = 5 * count;
            IncFlatSpellBlockMod(owner, resistanceBuff);
        }
    }
}