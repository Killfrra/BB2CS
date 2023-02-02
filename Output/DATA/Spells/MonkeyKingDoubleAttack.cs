#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MonkeyKingDoubleAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
        };
        Particle battleCries; // UNUSED
        int[] effect0 = {9, 8, 7, 6, 5};
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown;
            TeamId teamID; // UNITIALIZED
            nextBuffVars_SpellCooldown = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.MonkeyKingDoubleAttack(nextBuffVars_SpellCooldown), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
            SpellEffectCreate(out this.battleCries, out _, "xenZiou_battle_cry_weapon_01.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "weapon_a_bend3", default, owner, "weapon_b_bend3", default, false, default, default, false, false);
        }
    }
}
namespace Buffs
{
    public class MonkeyKingDoubleAttack : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weapon_a_bend3", "weapon_b_bend3", },
            AutoBuffActivateEffect = new[]{ "monkey_king_crushingBlow_buf_self.troy", "monkey_king_crushingBlow_buf_self.troy", },
            BuffName = "MonkeyKingDoubleAttack",
            BuffTextureName = "MonkeyKingCrushingBlow.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float spellCooldown;
        int[] effect0 = {30, 60, 90, 120, 150};
        float[] effect1 = {-0.3f, -0.3f, -0.3f, -0.3f, -0.3f};
        public MonkeyKingDoubleAttack(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, true);
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
            TeamId teamID;
            int level;
            float bonusDamage;
            float totalAD;
            float bonusADRatio;
            float damageToDeal; // UNUSED
            teamID = GetTeamID(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            bonusDamage = this.effect0[level];
            totalAD = GetTotalAttackDamage(owner);
            bonusADRatio = totalAD * 0.1f;
            bonusDamage += bonusADRatio;
            if(hitResult == HitResult.HIT_Critical)
            {
                hitResult = HitResult.HIT_Normal;
            }
            if(target is ObjAIBase)
            {
                Particle a; // UNUSED
                if(target is BaseTurret)
                {
                    damageToDeal = bonusDamage + damageAmount;
                    SpellEffectCreate(out a, out _, "monkey_king_crushingBlow_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                    SpellBuffRemove(owner, nameof(Buffs.MonkeyKingDoubleAttack), (ObjAIBase)owner, 0);
                }
                else
                {
                    float nextBuffVars_ArmorDebuff;
                    damageToDeal = bonusDamage + damageAmount;
                    nextBuffVars_ArmorDebuff = this.effect1[level];
                    BreakSpellShields(target);
                    AddBuff(attacker, target, new Buffs.MonkeyKingDoubleAttackDebuff(nextBuffVars_ArmorDebuff), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    SpellEffectCreate(out a, out _, "monkey_king_crushingBlow_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                    SpellBuffRemove(owner, nameof(Buffs.MonkeyKingDoubleAttack), (ObjAIBase)owner, 0);
                }
            }
            else
            {
                damageToDeal = bonusDamage + damageAmount;
                SpellBuffRemove(owner, nameof(Buffs.MonkeyKingDoubleAttack), (ObjAIBase)owner, 0);
            }
            damageAmount += bonusDamage;
        }
    }
}