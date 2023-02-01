#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Visionary_Counter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Visionary Counter",
            BuffTextureName = "Yeti_FrostNova_Charging.dds",
        };
        public override void OnActivate()
        {
            int count;
            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Visionary_Counter));
            if(count >= 7)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.Visionary(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.Visionary_Counter), 0);
            }
        }
    }
}