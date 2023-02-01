#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AkaliSBBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "AkaliTwilightShroudBuff",
            BuffTextureName = "AkaliTwilightShroud.dds",
        };
        float armorIncrease;
        int[] effect0 = {10, 20, 30, 40, 50};
        public override void OnActivate()
        {
            int level;
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.armorIncrease = this.effect0[level];
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.armorIncrease);
            IncFlatSpellBlockMod(owner, this.armorIncrease);
        }
    }
}