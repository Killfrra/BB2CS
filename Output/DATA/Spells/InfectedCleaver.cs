#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class InfectedCleaver : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            Vector3 pos; // UNITIALIZED
            SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, level, true, true, false);
        }
    }
}