#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class TalonRake : BBSpellScript
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
            AddBuff((ObjAIBase)owner, owner, new Buffs.TalonRakeMissileOne(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            pos = GetPointByUnitFacingOffset(owner, 750, 0);
            SpellCast((ObjAIBase)owner, default, pos, pos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 750, 20);
            SpellCast((ObjAIBase)owner, default, pos, pos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 750, -20);
            SpellCast((ObjAIBase)owner, default, pos, pos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}