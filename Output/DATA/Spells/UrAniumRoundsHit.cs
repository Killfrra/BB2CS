#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UrAniumRoundsHit : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UrAniumRoundsHit",
            BuffTextureName = "Bowmaster_IceArrow.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, -1);
            IncFlatArmorMod(owner, -1);
        }
    }
}