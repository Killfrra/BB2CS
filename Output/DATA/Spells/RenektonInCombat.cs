#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RenektonInCombat : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "RenekthonPredator",
            BuffTextureName = "Corki_RapidReload.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}