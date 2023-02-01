#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GarenKillBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GarenKillBuff",
            BuffTextureName = "Garen_CommandingPresence.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            float tooltipBonus;
            charVars.CommandBonus++;
            tooltipBonus = charVars.CommandBonus / 2;
            SetBuffToolTipVar(1, tooltipBonus);
        }
    }
}