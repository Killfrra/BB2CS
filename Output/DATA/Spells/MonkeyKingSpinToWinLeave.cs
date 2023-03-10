#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MonkeyKingSpinToWinLeave : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.MonkeyKingSpinToWin), (ObjAIBase)owner, 0);
        }
    }
}