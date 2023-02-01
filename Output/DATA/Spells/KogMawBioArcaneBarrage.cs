#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KogMawBioArcaneBarrage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "C_Tail", "BUFFBONE_CSTM_ANGLER", },
            AutoBuffActivateEffect = new[]{ "KogMawBioArcaneBarrage_buf.troy", "kogmaw_deepsea_bio_glow.troy", },
            BuffName = "KogMawBioArcaneBarrage",
            BuffTextureName = "KogMaw_BioArcaneBarrage.dds",
        };
        float attackRangeIncrease;
        public KogMawBioArcaneBarrage(float attackRangeIncrease = default)
        {
            this.attackRangeIncrease = attackRangeIncrease;
        }
        public override void OnActivate()
        {
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
            //RequireVar(this.attackRangeIncrease);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            this.attackRangeIncrease *= -1;
            IncFlatAttackRangeMod(owner, this.attackRangeIncrease);
            CancelAutoAttack(owner, false);
            RemoveOverrideAutoAttack(owner, true);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnUpdateStats()
        {
            IncFlatAttackRangeMod(owner, this.attackRangeIncrease);
        }
    }
}
namespace Spells
{
    public class KogMawBioArcaneBarrage : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {130, 150, 170, 190, 210};
        public override void SelfExecute()
        {
            float nextBuffVars_AttackRangeIncrease;
            nextBuffVars_AttackRangeIncrease = this.effect0[level];
            AddBuff(attacker, target, new Buffs.KogMawBioArcaneBarrage(nextBuffVars_AttackRangeIncrease), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}