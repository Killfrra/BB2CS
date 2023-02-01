#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlueCardPreattack : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Card_Blue.troy", },
            BuffName = "Pick A Card Blue",
            BuffTextureName = "Cardmaster_blue.dds",
        };
    }
}
namespace Spells
{
    public class BlueCardPreattack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, true, false);
            SpellBuffRemove(owner, nameof(Buffs.PickACard), (ObjAIBase)owner);
        }
    }
}