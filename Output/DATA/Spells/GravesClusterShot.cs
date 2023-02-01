#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GravesClusterShot : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 20f, 16f, 12f, 8f, 4f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            Vector3 pos;
            pos = GetPointByUnitFacingOffset(owner, 925, 0);
            SpellCast((ObjAIBase)owner, default, pos, pos, 4, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 50, 0);
            SpellCast((ObjAIBase)owner, default, pos, pos, 7, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 925, 16);
            SpellCast((ObjAIBase)owner, default, pos, pos, 4, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 925, -16);
            SpellCast((ObjAIBase)owner, default, pos, pos, 4, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
        }
    }
}