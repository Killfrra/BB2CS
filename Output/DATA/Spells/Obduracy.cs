#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Obduracy : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.2f, 0.25f, 0.3f, 0.35f, 0.4f};
        public override void SelfExecute()
        {
            float nextBuffVars_PercMod;
            nextBuffVars_PercMod = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.ObduracyBuff(nextBuffVars_PercMod), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}