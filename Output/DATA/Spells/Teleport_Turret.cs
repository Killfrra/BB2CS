#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Teleport_Turret : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Teleport_target.troy", },
            BuffName = "Teleport Target",
            BuffTextureName = "Summoner_teleport.dds",
        };
    }
}