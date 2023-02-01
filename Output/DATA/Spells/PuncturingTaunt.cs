#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class PuncturingTaunt : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {-10, -15, -20, -25, -30};
        float[] effect1 = {1, 1.5f, 2, 2.5f, 3};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nextBuffVars_ArmorDebuff;
            float tauntDuration;
            nextBuffVars_ArmorDebuff = this.effect0[level];
            tauntDuration = this.effect1[level];
            AddBuff(attacker, target, new Buffs.PuncturingTauntArmorDebuff(nextBuffVars_ArmorDebuff), 1, 1, tauntDuration, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
            ApplyTaunt(attacker, target, tauntDuration);
        }
    }
}