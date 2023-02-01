#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JudicatorHolyFervor : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "JudicatorHolyFervor",
            BuffTextureName = "Judicator_DivineBlessing.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}