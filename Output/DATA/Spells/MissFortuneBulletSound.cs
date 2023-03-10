#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MissFortuneBulletSound : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MissFortuneBulletSound",
            BuffTextureName = "MissFortune_BulletTime.dds",
        };
    }
}