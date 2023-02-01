#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Volley : BBSpellScript
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
            pos = GetPointByUnitFacingOffset(owner, 1000, -14);
            SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, default, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, 0);
            SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, default, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, 7);
            SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, default, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, -7);
            SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, default, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, 14);
            SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, default, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, -21);
            SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, default, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, 21);
            SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, default, false);
        }
    }
}