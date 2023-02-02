#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RivenFengShuiEngine : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {15, 15, 15};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff(attacker, target, new Buffs.RivenFengShuiEngine(), 1, 1, this.effect0[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class RivenFengShuiEngine : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", "", },
            BuffName = "RivenFengShuiEngine",
            BuffTextureName = "RivenBladeoftheExile.dds",
            NonDispellable = true,
            SpellToggleSlot = 4,
        };
        float bonusAD;
        Particle temp2;
        Particle temp3;
        Particle temp4;
        Particle temp;
        int[] effect0 = {75, 60, 45, 45, 45};
        public override void OnActivate()
        {
            float totalAD;
            float bonusAD;
            float attackDamage;
            float baseAD;
            float qAttackDamage;
            float rAttackDamage;
            float eAttackDamage;
            float wAttackDamage;
            totalAD = GetTotalAttackDamage(owner);
            bonusAD = totalAD * 0.2f;
            this.bonusAD = bonusAD;
            IncFlatPhysicalDamageMod(owner, this.bonusAD);
            IncFlatAttackRangeMod(owner, 75);
            OverrideAnimation("Attack1", "Attack1_ult", owner);
            OverrideAnimation("Attack2", "Attack2_ult", owner);
            OverrideAnimation("Attack3", "Attack3_ult", owner);
            OverrideAnimation("Crit", "Crit_ult", owner);
            OverrideAnimation("Idle1", "Idle1_ult", owner);
            OverrideAnimation("Run", "Run_ult", owner);
            OverrideAnimation("Spell1a", "Spell1a_ult", owner);
            OverrideAnimation("Spell1b", "Spell1b_ult", owner);
            OverrideAnimation("Spell1c", "Spell1c_ult", owner);
            OverrideAnimation("Spell2", "Spell2_ult", owner);
            OverrideAnimation("Spell3", "Spell3_ult", owner);
            attackDamage = GetTotalAttackDamage(owner);
            baseAD = GetBaseAttackDamage(owner);
            attackDamage -= baseAD;
            qAttackDamage = 0.7f * attackDamage;
            SetSpellToolTipVar(qAttackDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            rAttackDamage = 1.8f * attackDamage;
            SetSpellToolTipVar(rAttackDamage, 2, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            eAttackDamage = attackDamage * 1;
            SetSpellToolTipVar(eAttackDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            wAttackDamage = 1 * attackDamage;
            SetSpellToolTipVar(wAttackDamage, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            SpellEffectCreate(out this.temp2, out _, "exile_ult_blade_swap_base.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_2", default, owner, "BUFFBONE_GLB_WEAPON_2", default, false, false, false, false, false);
            SpellEffectCreate(out this.temp3, out _, "exile_ult_attack_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", default, owner, "BUFFBONE_GLB_WEAPON_2", default, false, false, false, false, false);
            SpellEffectCreate(out this.temp4, out this.temp, "exile_ult_attack_buf.troy", "RivenBladePiece", TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_hand", default, owner, "BUFFBONE_GLB_WEAPON_2", default, false, false, false, false, false);
            rAttackDamage = 0.6f * attackDamage;
            SetSpellToolTipVar(rAttackDamage, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RivenWindSlashReady(), 1, 1, 15, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            SetVoiceOverride("Ult", owner);
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.RivenIzunaBlade));
            SetSlotSpellCooldownTimeVer2(0.5f, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            float cDReduction;
            int level;
            float baseCD;
            float lowerCD;
            float newCD;
            float attackDamage;
            float baseAD;
            float qAttackDamage;
            float rAttackDamage;
            float eAttackDamage;
            float wAttackDamage;
            cDReduction = GetPercentCooldownMod(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseCD = this.effect0[level];
            lowerCD = baseCD * cDReduction;
            newCD = baseCD + lowerCD;
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.RivenFengShuiEngine));
            SetSlotSpellCooldownTimeVer2(newCD, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            ClearOverrideAnimation("Attack1", owner);
            ClearOverrideAnimation("Attack2", owner);
            ClearOverrideAnimation("Attack3", owner);
            ClearOverrideAnimation("Crit", owner);
            ClearOverrideAnimation("Idle1", owner);
            ClearOverrideAnimation("Run", owner);
            ClearOverrideAnimation("Spell1a", owner);
            ClearOverrideAnimation("Spell1b", owner);
            ClearOverrideAnimation("Spell1c", owner);
            ClearOverrideAnimation("Spell2", owner);
            ClearOverrideAnimation("Spell3", owner);
            attackDamage = GetTotalAttackDamage(owner);
            baseAD = GetBaseAttackDamage(owner);
            attackDamage -= baseAD;
            qAttackDamage = 0.7f * attackDamage;
            SetSpellToolTipVar(qAttackDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            rAttackDamage = 0.6f * attackDamage;
            SetSpellToolTipVar(rAttackDamage, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            rAttackDamage = 1.8f * attackDamage;
            SetSpellToolTipVar(rAttackDamage, 2, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            eAttackDamage = attackDamage * 1;
            SetSpellToolTipVar(eAttackDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            wAttackDamage = 1 * attackDamage;
            SetSpellToolTipVar(wAttackDamage, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            SpellEffectRemove(this.temp);
            SpellEffectRemove(this.temp2);
            SpellEffectRemove(this.temp3);
            SpellEffectRemove(this.temp4);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SpellBuffClear(owner, nameof(Buffs.RivenWindSlashReady));
            ResetVoiceOverride(owner);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, this.bonusAD);
            IncFlatAttackRangeMod(owner, 75);
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            string name;
            name = GetSpellName();
            if(name == nameof(Spells.RivenIzunaBlade))
            {
                SpellEffectRemove(this.temp2);
                SpellEffectCreate(out this.temp2, out _, "exile_ult_blade_swap_base.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_WEAPON_2", default, owner, "BUFFBONE_GLB_WEAPON_2", default, false, false, false, false, false);
            }
        }
    }
}