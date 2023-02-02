#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MaokaiDrain3Toggle : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.MaokaiDrain3), (ObjAIBase)owner);
        }
    }
}
namespace Buffs
{
    public class MaokaiDrain3Toggle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GlacialStorm",
            BuffTextureName = "Cryophoenix_GlacialStorm.dds",
            SpellToggleSlot = 4,
        };
    }
}