#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BandageToss : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 18f, 16f, 14f, 12f, 10f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos;
            targetPos = GetCastSpellTargetPos();
            FaceDirection(owner, targetPos);
            targetPos = GetPointByUnitFacingOffset(owner, 1100, 0);
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false);
        }
    }
}