#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CaitlynHeadshotCount : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CaitlynHeadshotCount",
            BuffTextureName = "Caitlyn_Headshot.dds",
        };
        public override void OnActivate()
        {
            int count;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.CaitlynHeadshotCount));
            if(count >= charVars.TooltipAmount)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.CaitlynHeadshot(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.CaitlynHeadshotCount), 0);
            }
        }
    }
}