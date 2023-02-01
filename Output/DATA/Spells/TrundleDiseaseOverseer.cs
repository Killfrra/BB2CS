#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TrundleDiseaseOverseer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundleDiseaseOverseer",
            BuffTextureName = "Trundle_Contaminate.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            float tTVar2;
            tTVar2 = charVars.RegenValue * 100;
            SetBuffToolTipVar(2, tTVar2);
        }
    }
}