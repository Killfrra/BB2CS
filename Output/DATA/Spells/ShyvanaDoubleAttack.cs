#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShyvanaDoubleAttack : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "R_Hand", "L_Hand", },
            AutoBuffActivateEffect = new[]{ "shyvana_doubleAttack_buf.troy", "shyvana_doubleAttack_buf.troy", },
            BuffName = "ShyvanaDoubleAttack",
            BuffTextureName = "ShyvanaTwinBite.dds",
            IsDeathRecapSource = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float spellCooldown;
        public ShyvanaDoubleAttack(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
            SetDodgePiercing(owner, true);
            CancelAutoAttack(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            float spellCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            spellCooldown = this.spellCooldown;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            RemoveOverrideAutoAttack(owner, true);
            SetDodgePiercing(owner, false);
        }
        public override void OnUpdateStats()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            SpellBuffRemove(owner, nameof(Buffs.ShyvanaDoubleAttack), (ObjAIBase)owner, 0);
        }
    }
}
namespace Spells
{
    public class ShyvanaDoubleAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
        };
        int[] effect0 = {10, 9, 8, 7, 6};
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown;
            nextBuffVars_SpellCooldown = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaDoubleAttack(nextBuffVars_SpellCooldown), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}