#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VayneCondemn : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            charVars.CastPoint = GetUnitPosition(owner);
            SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}