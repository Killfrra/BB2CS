#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MaokaiSapMagicHot : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaokaiSapMagicHot",
            BuffTextureName = "Maokai_SapMagic.dds",
        };
        public override void OnActivate()
        {
            int count;
            count = GetBuffCountFromCaster(owner, default, nameof(Buffs.MaokaiSapMagicHot));
            if(count >= 5)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.MaokaiSapMagicMelee(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.MaokaiSapMagicHot), 0);
            }
        }
    }
}