#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UPGRADE___Proof : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weapon", },
            AutoBuffActivateEffect = new[]{ "Wujustyle_buf.troy", },
            BuffName = "UPGRADESuperCharge",
            BuffTextureName = "Heimerdinger_UPGRADE.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, 0.5f);
        }
    }
}