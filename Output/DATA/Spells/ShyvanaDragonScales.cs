#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShyvanaDragonScales : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ShyvanaDragonScales",
            BuffTextureName = "ShyvanaReinforcedScales.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float defenseToAdd;
        int[] effect0 = {30, 40, 50};
        int[] effect1 = {15, 20, 25};
        public override void OnActivate()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaTransform)) > 0)
            {
                this.defenseToAdd = this.effect0[level];
            }
            else
            {
                this.defenseToAdd = this.effect1[level];
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.defenseToAdd);
            IncFlatSpellBlockMod(owner, this.defenseToAdd);
        }
    }
}