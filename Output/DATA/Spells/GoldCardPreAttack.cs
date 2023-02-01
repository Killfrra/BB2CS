#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GoldCardPreAttack : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Card_Yellow.troy", },
            BuffName = "Pick A Card Gold",
            BuffTextureName = "Cardmaster_gold.dds",
        };
    }
}
namespace Spells
{
    public class GoldCardPreAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 3, SpellSlotType.ExtraSlots, level, true, true, false, false, true, false);
            SpellBuffRemove(owner, nameof(Buffs.PickACard), (ObjAIBase)owner);
        }
    }
}