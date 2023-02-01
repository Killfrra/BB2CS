#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyDiplomaticImmunitySlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Freeze.troy", },
            BuffName = "PoppyDiplomaticImmunitySlow",
            BuffTextureName = "Poppy_DiplomaticImmunity.dds",
        };
        float slowValue;
        float[] effect0 = {-0.1f, -0.15f, -0.2f};
        public override void OnActivate()
        {
            int level;
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.slowValue = this.effect0[level];
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.slowValue);
        }
    }
}