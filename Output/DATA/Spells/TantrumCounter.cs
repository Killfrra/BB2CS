#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TantrumCounter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Tantrum Counter",
            BuffTextureName = "SadMummy_Tantrum.dds",
        };
        int[] effect0 = {13, 11, 9, 7, 5};
        public override void OnActivate()
        {
            int count;
            int level;
            int hitsRequired;
            count = GetBuffCountFromAll(owner, nameof(Buffs.TantrumCounter));
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            hitsRequired = this.effect0[level];
            if(count >= hitsRequired)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.TantrumCanCast(), 1, 1, 12, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.TantrumCounter), 0);
            }
        }
    }
}