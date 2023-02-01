#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyParagonStats : BBBuffScript
    {
        float[] effect0 = {1.5f, 2, 2.5f, 3, 3.5f};
        public override void OnUpdateStats()
        {
            int level;
            float armorDamageValue;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            armorDamageValue = this.effect0[level];
            IncFlatPhysicalDamageMod(owner, armorDamageValue);
            IncFlatArmorMod(owner, armorDamageValue);
        }
    }
}