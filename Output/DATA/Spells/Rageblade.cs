#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Rageblade : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GuinsoosRageblade",
            BuffTextureName = "3064_Spike_the_Ripper.dds",
        };
        public override void OnActivate()
        {
            IncPermanentPercentAttackSpeedMod(owner, 0.04f);
            IncPermanentFlatMagicDamageMod(owner, 6);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentPercentAttackSpeedMod(owner, -0.04f);
            IncPermanentFlatMagicDamageMod(owner, -6);
        }
    }
}