#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CasterMinionAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Caster Minion Aura",
            BuffTextureName = "3022_Frozen_Heart.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentPhysicalDamageMod(owner, 1);
            IncPercentMagicDamageMod(owner, -1);
        }
    }
}