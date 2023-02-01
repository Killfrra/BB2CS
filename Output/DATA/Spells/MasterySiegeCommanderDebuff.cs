#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MasterySiegeCommanderDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "FortifyBuff",
            BuffTextureName = "Summoner_fortify.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, -10);
        }
    }
}