#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AlZaharVoidlingCount : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AlZaharSummonVoidlingBuff",
            BuffTextureName = "AlZahar_VoidlingCharging.dds",
            NonDispellable = true,
        };
        public override void OnActivate()
        {
            int count;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.AlZaharVoidlingCount));
            if(count >= 3)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AlZaharSummonVoidling(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.AlZaharVoidlingCount), 0);
            }
        }
    }
}