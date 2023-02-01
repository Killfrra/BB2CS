#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CounterStrikeDodgeUp : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CounterStrikeDodgeUp",
            BuffTextureName = "Armsmaster_Disarm.dds",
        };
        public override void OnUpdateStats()
        {
            int level;
            float lvlDodgeMod;
            float dodgeMod;
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            lvlDodgeMod = level * 0.02f;
            dodgeMod = lvlDodgeMod + 0.08f;
            IncFlatDodgeMod(owner, dodgeMod);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.CounterStrikeCanCast)) == 0)
            {
                SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
    }
}