#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JudicatorHolyFervorDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JudicatorHolyFervorDebuff",
            BuffTextureName = "Judicator_DivineBlessing.dds",
        };
        public override void OnActivate()
        {
            IncPercentArmorMod(owner, -0.03f);
            IncPercentSpellBlockMod(owner, -0.03f);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorMod(owner, -0.03f);
            IncPercentSpellBlockMod(owner, -0.03f);
        }
    }
}