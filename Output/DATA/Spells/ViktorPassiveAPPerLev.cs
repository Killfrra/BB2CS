#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ViktorPassiveAPPerLev : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "ViktorStormTimer",
            BuffTextureName = "ViktorChaosStormGuide.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float aPPERLEV;
        public override void OnActivate()
        {
            int ownerLevel;
            this.aPPERLEV = 0;
            ownerLevel = GetLevel(owner);
            this.aPPERLEV = ownerLevel * 3;
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, this.aPPERLEV);
        }
        public override void OnUpdateActions()
        {
            int ownerLevel;
            ownerLevel = GetLevel(owner);
            this.aPPERLEV = ownerLevel * 3;
        }
    }
}