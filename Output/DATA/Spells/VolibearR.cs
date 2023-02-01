#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VolibearR : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.75f,
            SpellDamageRatio = 0.75f,
        };
        int[] effect0 = {75, 115, 155};
        int[] effect1 = {0, 0, 0};
        int[] effect2 = {4, 5, 6};
        int[] effect3 = {12, 12, 12};
        public override void SelfExecute()
        {
            int nextBuffVars_VolibearRDamage;
            int nextBuffVars_VolibearRSpeed;
            float nextBuffVars_VolibearRRatio;
            int volibearRCharges; // UNUSED
            nextBuffVars_VolibearRDamage = this.effect0[level];
            nextBuffVars_VolibearRSpeed = this.effect1[level];
            nextBuffVars_VolibearRRatio = 0.3f;
            volibearRCharges = this.effect2[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.VolibearRApplicator(nextBuffVars_VolibearRDamage, nextBuffVars_VolibearRSpeed, nextBuffVars_VolibearRRatio), 1, 1, this.effect3[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            CancelAutoAttack(owner, true);
        }
    }
}