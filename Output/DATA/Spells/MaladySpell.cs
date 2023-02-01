#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MaladySpell : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaladySpell",
            BuffTextureName = "3114_Malady.dds",
        };
        public override void OnUpdateStats()
        {
            int count;
            float resistanceShred;
            count = GetBuffCountFromAll(owner, nameof(Buffs.MaladyCounter));
            resistanceShred = -6 * count;
            IncFlatSpellBlockMod(owner, resistanceShred);
        }
        public override void OnActivate()
        {
            int count;
            float resistanceShred;
            count = GetBuffCountFromAll(owner, nameof(Buffs.MaladyCounter));
            resistanceShred = -6 * count;
            IncFlatSpellBlockMod(owner, resistanceShred);
        }
    }
}