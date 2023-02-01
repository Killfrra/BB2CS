#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VolibearRChargetracker : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", null, "", },
            AutoBuffActivateEffect = new[]{ "", "", "", "", },
            BuffName = "VolibearRApplicator",
            BuffTextureName = "Minotaur_Pulverize.dds",
        };
    }
}