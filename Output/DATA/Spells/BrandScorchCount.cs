#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BrandScorchCount : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CaitlynHeadshotCount",
            BuffTextureName = "Caitlyn_Headshot.dds",
        };
        public override void OnActivate()
        {
            int count;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.BrandScorchCount));
            if(count >= 7)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.BrandScorch(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.BrandScorchCount), 0);
            }
        }
    }
}