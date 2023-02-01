#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KogMawCausticSpittleCharged : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "KogMawCausticSpittleCharged",
            BuffTextureName = "KogMaw_CausticSpittle.dds",
        };
        int[] effect0 = {-5, -10, -15, -20, -25};
        int[] effect1 = {-5, -10, -15, -20, -25};
        public override void OnUpdateStats()
        {
            int level;
            float armorReduction;
            float magicReduction;
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            armorReduction = this.effect0[level];
            magicReduction = this.effect1[level];
            IncFlatSpellBlockMod(owner, magicReduction);
            IncFlatArmorMod(owner, armorReduction);
        }
    }
}