#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JudicatorRighteousFury : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weapon", "head", },
            AutoBuffActivateEffect = new[]{ "Flamesword.troy", "RighteousFuryHalo_buf.troy", },
            BuffName = "JudicatorRighteousFury",
            BuffTextureName = "Judicator_RighteousFury.dds",
        };
        float attackRangeIncrease;
        public JudicatorRighteousFury(float attackRangeIncrease = default)
        {
            this.attackRangeIncrease = attackRangeIncrease;
        }
        public override void OnActivate()
        {
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
            //RequireVar(this.attackRangeIncrease);
            IncFlatAttackRangeMod(owner, this.attackRangeIncrease);
        }
        public override void OnDeactivate(bool expired)
        {
            this.attackRangeIncrease *= -1;
            IncFlatAttackRangeMod(owner, this.attackRangeIncrease);
            RemoveOverrideAutoAttack(owner, true);
            CancelAutoAttack(owner, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatAttackRangeMod(owner, this.attackRangeIncrease);
        }
    }
}
namespace Spells
{
    public class JudicatorRighteousFury : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {400, 400, 400, 400, 400};
        public override void SelfExecute()
        {
            float nextBuffVars_AttackRangeIncrease;
            nextBuffVars_AttackRangeIncrease = this.effect0[level];
            AddBuff(attacker, owner, new Buffs.JudicatorRighteousFury(nextBuffVars_AttackRangeIncrease), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.KayleRighteousFuryAnim(), 1, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}