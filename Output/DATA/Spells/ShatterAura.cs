#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShatterAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "ShatterAura",
            BuffTextureName = "GemKnight_Shatter.dds",
        };
        float armorBonus;
        int[] effect0 = {10, 15, 20, 25, 30};
        public override void OnUpdateStats()
        {
            int level;
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.armorBonus = this.effect0[level];
            IncFlatArmorMod(owner, this.armorBonus);
        }
    }
}