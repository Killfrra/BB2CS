#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BowMasterFocusDisplay : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "BowMasterFocusDisplay",
            BuffTextureName = "Bowmaster_Bullseye.dds",
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            float critToAdd;
            float critToDisplay;
            float critToTooltip;
            critToAdd = charVars.NumSecondsSinceLastCrit * charVars.CritPerSecond;
            critToDisplay = 100 * critToAdd;
            critToTooltip = Math.Min(100, critToDisplay);
            SetBuffToolTipVar(1, critToTooltip);
        }
    }
}