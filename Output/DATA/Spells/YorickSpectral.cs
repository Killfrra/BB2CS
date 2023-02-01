#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickSpectral : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "l_hand", "r_hand", },
            AutoBuffActivateEffect = new[]{ "yorick_spectralGhoul_attack_buf_self.troy", "yorick_spectralGhoul_attack_buf_self.troy", },
            BuffName = "YorickSpectralPreHit",
            BuffTextureName = "YorickOmenOfWarPreHit.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float bonusDamage;
        float spellCooldown;
        public YorickSpectral(float bonusDamage = default, float spellCooldown = default)
        {
            this.bonusDamage = bonusDamage;
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusDamage);
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
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
            SetDodgePiercing(owner, false);
        }
        public override void OnUpdateStats()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            TeamId teamID;
            Particle a; // UNUSED
            float totalDamage;
            Vector3 targetPos;
            int level;
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonSpectral)) > 0)
                    {
                        SpellBuffClear(owner, nameof(Buffs.YorickSummonSpectral));
                    }
                    if(hitResult == HitResult.HIT_Critical)
                    {
                        hitResult = HitResult.HIT_Normal;
                    }
                    teamID = GetTeamID(owner);
                    SpellEffectCreate(out a, out _, "yorick_spectralGhoul_attack_buf_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                    damageAmount *= 1.2f;
                    totalDamage = damageAmount + this.bonusDamage;
                    AddBuff(attacker, target, new Buffs.YorickSpectralPrimaryTarget(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(target);
                    ApplyDamage(attacker, target, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, true, true, attacker);
                    targetPos = GetPointByUnitFacingOffset(owner, 25, 0);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 2, SpellSlotType.ExtraSlots, level, true, true, false, true, false, false);
                    SpellBuffRemove(owner, nameof(Buffs.YorickSpectral), (ObjAIBase)owner, 0);
                    damageAmount -= damageAmount;
                }
            }
        }
    }
}
namespace Spells
{
    public class YorickSpectral : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
        int[] effect0 = {9, 8, 7, 6, 5};
        int[] effect1 = {30, 60, 90, 120, 150};
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown;
            float nextBuffVars_BonusDamage;
            nextBuffVars_SpellCooldown = this.effect0[level];
            nextBuffVars_BonusDamage = this.effect1[level];
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickSpectralUnlock)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.YorickSpectralUnlock), (ObjAIBase)owner, 0);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.YorickSpectral(nextBuffVars_BonusDamage, nextBuffVars_SpellCooldown), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.YorickSpectralUnlock(), 1, 1, 11, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}