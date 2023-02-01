#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyParagonIcon : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PoppyParagonManager",
            BuffTextureName = "PoppyDefenseOfDemacia.dds",
        };
        float[] effect0 = {1.5f, 2, 2.5f, 3, 3.5f};
        public override void OnActivate()
        {
            int count;
            int level;
            float armDmgValue;
            count = GetBuffCountFromAll(owner, nameof(Buffs.PoppyParagonStats));
            SetBuffToolTipVar(1, count);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            armDmgValue = this.effect0[level];
            armDmgValue *= count;
            SetBuffToolTipVar(2, armDmgValue);
        }
    }
}