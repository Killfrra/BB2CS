#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class EnrageMaxHP : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CannibalismMaxHPGained",
            BuffTextureName = "Sion_SpiritFeast.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float hPGain;
        public EnrageMaxHP(float hPGain = default)
        {
            this.hPGain = hPGain;
        }
        public override void OnActivate()
        {
            charVars.HPGain += this.hPGain;
        }
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, charVars.HPGain);
        }
    }
}