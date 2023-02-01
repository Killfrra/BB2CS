#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzTriCleaveBuffered : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RenekthonCleaveReady",
            BuffTextureName = "AkaliCrescentSlash.dds",
            SpellToggleSlot = 1,
        };
        Vector3 targetPos;
        int level;
        public FizzTriCleaveBuffered(Vector3 targetPos = default, int level = default)
        {
            this.targetPos = targetPos;
            this.level = level;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            //RequireVar(this.level);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellCast((ObjAIBase)owner, default, this.targetPos, this.targetPos, 0, SpellSlotType.ExtraSlots, this.level, true, false, false, false, false, false);
        }
    }
}