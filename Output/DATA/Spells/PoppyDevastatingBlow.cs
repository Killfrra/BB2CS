#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyDevastatingBlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "hammer_b", "", },
            AutoBuffActivateEffect = new[]{ "Poppy_DevastatingBlow_buf.troy", "", },
            BuffName = "PoppyDevastatingBlow",
            BuffTextureName = "PoppyDevastatingBlow.dds",
        };
        float spellCooldown;
        float bonusDamage;
        int[] effect0 = {75, 150, 225, 300, 375};
        public PoppyDevastatingBlow(float spellCooldown = default, float bonusDamage = default)
        {
            this.spellCooldown = spellCooldown;
            this.bonusDamage = bonusDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spellCooldown);
            //RequireVar(this.bonusDamage);
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
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int level;
            float maxDamage;
            TeamId teamID;
            float tarMaxHealth;
            float damageToDeal;
            Particle a; // UNUSED
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            maxDamage = this.effect0[level];
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    teamID = GetTeamID(owner);
                    tarMaxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                    tarMaxHealth *= 0.08f;
                    damageToDeal = tarMaxHealth + this.bonusDamage;
                    damageToDeal = Math.Min(damageToDeal, maxDamage);
                    SpellEffectCreate(out a, out _, "Poppy_DevastatingBlow_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                    damageToDeal += damageAmount;
                    BreakSpellShields(target);
                    ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
                    SpellBuffRemove(owner, nameof(Buffs.PoppyDevastatingBlow), (ObjAIBase)owner);
                    damageAmount -= damageAmount;
                }
            }
        }
    }
}
namespace Spells
{
    public class PoppyDevastatingBlow : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = 0,
                CanHitSameTarget = 0,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 4, 4, 4, 4, 4, },
            },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {8, 7, 6, 5, 4};
        int[] effect1 = {20, 40, 60, 80, 100};
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown;
            float nextBuffVars_BonusDamage;
            nextBuffVars_SpellCooldown = this.effect0[level];
            nextBuffVars_BonusDamage = this.effect1[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyDevastatingBlow(nextBuffVars_SpellCooldown, nextBuffVars_BonusDamage), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}