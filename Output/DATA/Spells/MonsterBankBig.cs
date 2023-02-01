#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonsterBankBig : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "treasure_chest_gold_sparkle.troy", },
            BuffName = "Monster Bank Big",
            BuffTextureName = "Treasure_Chest.dds",
        };
    }
}