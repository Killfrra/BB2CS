#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HeimerdingerTurretReady : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Heimerdinger Turret Ready",
            BuffTextureName = "Heimerdinger_H28GEvolutionTurret.dds",
        };
    }
}