#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GravesSmokeGrenadeSecretPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Freeze.troy", },
            BuffName = "Iceblast Slow",
            BuffTextureName = "3022_Frozen_Heart.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
    }
}