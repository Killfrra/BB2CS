#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UpgradeBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "HolyFervor_buf.troy", },
            BuffName = "Upgrade Buff",
            BuffTextureName = "Heimerdinger_UPGRADE.dds",
        };
    }
}