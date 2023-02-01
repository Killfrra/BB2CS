#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NashorsToothSpell : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "NashorsTooth",
            BuffTextureName = "MasterYi_LeapStrike.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, 35);
        }
    }
}