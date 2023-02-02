#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KennenLightningRush : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {85, 125, 165, 205, 245};
        public override void SelfExecute()
        {
            int nextBuffVars_RushDamage;
            nextBuffVars_RushDamage = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.KennenLightningRushDamage(nextBuffVars_RushDamage), 1, 1, 2.2f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0.1f, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.KennenLightningRush(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.HASTE, 0, true, false);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KennenLRCancel));
            SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.5f);
        }
    }
}
namespace Buffs
{
    public class KennenLightningRush : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            SpellToggleSlot = 3,
        };
        Particle ar;
        float moveSpeedMod;
        int defenseBonus;
        Fade litRush;
        int[] effect0 = {10, 20, 30, 40, 50};
        int[] effect1 = {10, 9, 8, 7, 6};
        public override void OnActivate()
        {
            int level;
            float nextBuffVars_DefenseBonus;
            SpellEffectCreate(out this.ar, out _, "kennen_lr_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.moveSpeedMod = 1;
            this.defenseBonus = this.effect0[level];
            SetGhosted(owner, true);
            SetForceRenderParticles(owner, true);
            SetCanAttack(owner, false);
            IncFlatAttackRangeMod(owner, -575);
            this.litRush = PushCharacterFade(owner, 0, 0.1f);
            nextBuffVars_DefenseBonus = this.defenseBonus;
            AddBuff((ObjAIBase)owner, owner, new Buffs.KennenLightningRushBuff(nextBuffVars_DefenseBonus), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            float spellCD;
            float cDMod;
            float superCDMod;
            float realCD;
            Particle supervar; // UNUSED
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KennenLightningRush));
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            spellCD = this.effect1[level];
            cDMod = GetPercentCooldownMod(owner);
            superCDMod = 1 + cDMod;
            realCD = spellCD * superCDMod;
            SpellEffectRemove(this.ar);
            SetGhosted(owner, false);
            SpellBuffRemove(owner, nameof(Buffs.KennenLightningRush), (ObjAIBase)owner);
            SetForceRenderParticles(owner, false);
            SpellEffectCreate(out supervar, out _, "kennen_lr_off.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            SetCanAttack(owner, true);
            PopCharacterFade(owner, this.litRush);
            IncFlatAttackRangeMod(owner, 575);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, realCD);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
            IncFlatAttackRangeMod(owner, -575);
        }
    }
}