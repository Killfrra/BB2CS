#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RivenIzunaBlade : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 pos;
            SpellBuffClear(owner, nameof(Buffs.RivenWindSlashReady));
            targetPos = GetCastSpellTargetPos();
            FaceDirection(owner, targetPos);
            pos = GetPointByUnitFacingOffset(owner, 150, 0);
            SpellCast((ObjAIBase)owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 150, 9);
            SpellCast((ObjAIBase)owner, default, pos, pos, 3, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 150, -9);
            SpellCast((ObjAIBase)owner, default, pos, pos, 3, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.RivenFengShuiEngine)) > 0)
            {
                SetSlotSpellCooldownTimeVer2(0, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
    }
}