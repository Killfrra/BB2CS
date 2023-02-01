#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HextechSpellvamp : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Atma's Impaler",
            BuffTextureName = "3005_Atmas_Impaler.dds",
        };
        public override void OnUpdateStats()
        {
            float percHealth;
            float percMissing;
            float vamp;
            percHealth = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            percMissing = 1 - percHealth;
            vamp = percMissing / 2.5f;
            IncPercentSpellVampMod(owner, vamp);
        }
    }
}