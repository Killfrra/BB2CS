#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class InfernalGuardianTimer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "InfernalGuardianTimer",
            BuffTextureName = "Annie_GuardianIncinerate.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}