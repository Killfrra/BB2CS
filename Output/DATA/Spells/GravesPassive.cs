#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GravesPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "GravesPassive",
            BuffTextureName = "GravesTrueGrit.dds",
            PersistsThroughDeath = true,
        };
        public override void OnUpdateActions()
        {
            SetBuffToolTipVar(1, charVars.ArmorAmount);
        }
    }
}