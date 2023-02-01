#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HasBeenRevived : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "HasBeenRevived",
            BuffTextureName = "3026_Guardian_Angel_Charging.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.WillRevive(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnUpdateActions()
        {
            SpellBuffRemove(owner, nameof(Buffs.WillRevive), (ObjAIBase)owner, 0);
        }
    }
}