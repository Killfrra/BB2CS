#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MaladyCounter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaladySpell",
            BuffTextureName = "3114_Malady.dds",
        };
        public override void OnActivate()
        {
            int count;
            float resistanceShred;
            count = GetBuffCountFromAll(owner, nameof(Buffs.MaladyCounter));
            resistanceShred = count * 6;
            SetBuffToolTipVar(1, resistanceShred);
        }
    }
}