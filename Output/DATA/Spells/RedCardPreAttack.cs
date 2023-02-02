#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RedCardPreAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, true, false);
            SpellBuffRemove(owner, nameof(Buffs.PickACard), (ObjAIBase)owner);
        }
    }
}
namespace Buffs
{
    public class RedCardPreAttack : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Card_Red.troy", },
            BuffName = "Pick A Card Red",
            BuffTextureName = "Cardmaster_red.dds",
        };
    }
}