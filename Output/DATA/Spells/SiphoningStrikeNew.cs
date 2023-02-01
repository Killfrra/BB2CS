#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SiphoningStrikeNew : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weapon_b", },
            AutoBuffActivateEffect = new[]{ "nassus_siphonStrike_buf.troy", },
            BuffName = "SiphoningStrike",
            BuffTextureName = "Nasus_SiphoningStrike.dds",
            IsDeathRecapSource = true,
        };
        float spellCooldown;
        Particle particleID;
        int[] effect0 = {30, 50, 70, 90, 110};
        public SiphoningStrikeNew(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            SetCanAttack(owner, false);
            SetCanAttack(owner, true);
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SpellEffectCreate(out this.particleID, out _, "nassus_siphonStrike_beam_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "weapon_b4", default, owner, "weapon_b1", default, false, false, false, false, false);
            SetDodgePiercing(owner, true);
            CancelAutoAttack(owner, true);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int level;
            float baseDamage;
            float damageToDeal;
            TeamId teamID;
            Particle sdg; // UNUSED
            float spellCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = this.effect0[level];
            damageToDeal = baseDamage + damageAmount;
            damageToDeal += charVars.DamageBonus;
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    teamID = GetTeamID(owner);
                    SpellEffectCreate(out sdg, out _, "nassus_siphonStrike_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                    AddBuff((ObjAIBase)owner, target, new Buffs.SiphoningStrike(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            SpellEffectRemove(this.particleID);
            SpellBuffClear(owner, nameof(Buffs.SiphoningStrikeNew));
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            damageAmount *= 0;
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            spellCooldown = this.spellCooldown;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SetDodgePiercing(owner, false);
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
    }
}
namespace Spells
{
    public class SiphoningStrikeNew : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {8, 7, 6, 5, 4};
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown;
            nextBuffVars_SpellCooldown = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.SiphoningStrikeNew(nextBuffVars_SpellCooldown), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}