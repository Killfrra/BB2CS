#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyDefenseOfDemacia : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PoppyDefenseOfDemacia",
            BuffTextureName = "PoppyDefenseOfDemacia.dds",
        };
        float armorCount;
        float increasedArmor;
        float[] effect0 = {1, 1.5f, 2, 2.5f, 3};
        public PoppyDefenseOfDemacia(float armorCount = default)
        {
            this.armorCount = armorCount;
        }
        public override void OnActivate()
        {
            int level;
            float armorPerHit;
            //RequireVar(this.armorCount);
            if(this.armorCount == 20)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyDefenseParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            armorPerHit = this.effect0[level];
            this.increasedArmor = this.armorCount * armorPerHit;
            SetBuffToolTipVar(1, this.armorCount);
            SetBuffToolTipVar(2, this.increasedArmor);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.increasedArmor);
        }
    }
}