#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GuardianAngel : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "LifeAura.troy", },
            BuffTextureName = "Cryophoenix_Rebirth.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            float currentCooldown;
            float currentCooldown2;
            SpellEffectCreate(out _, out _, "LifeAura.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            PlayAnimation("Death", 4, owner, false, false, true);
            IncPAR(owner, -10000, PrimaryAbilityResourceType.MANA);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetInvulnerable(owner, true);
            SetTargetable(owner, false);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffRemoveType(owner, BuffType.POISON);
            SpellBuffRemoveType(owner, BuffType.COMBAT_DEHANCER);
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.INVISIBILITY);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.DAMAGE);
            SpellBuffRemoveType(owner, BuffType.HEAL);
            SpellBuffRemoveType(owner, BuffType.HASTE);
            SpellBuffRemoveType(owner, BuffType.SPELL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.PHYSICAL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.INVULNERABILITY);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.COMBAT_ENCHANCER);
            SpellBuffRemoveType(owner, BuffType.SHRED);
            SpellBuffRemove(owner, nameof(Buffs.WillRevive), (ObjAIBase)owner, 0);
            currentCooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            currentCooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(currentCooldown <= 4)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 4);
            }
            if(currentCooldown2 <= 4)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 4);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            Particle part; // UNUSED
            IncHealth(owner, 750, owner);
            IncPAR(owner, 375, PrimaryAbilityResourceType.MANA);
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            SetSuppressCallForHelp(owner, false);
            SetCallForHelpSuppresser(owner, false);
            SetIgnoreCallForHelp(owner, false);
            SetInvulnerable(owner, false);
            SetTargetable(owner, true);
            UnlockAnimation(owner, false);
            PlayAnimation("idle1", 0, owner, false, false, true);
            UnlockAnimation(owner, false);
            SpellEffectCreate(out part, out _, "GuardianAngel_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.HasBeenRevived(), 1, 1, 300, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetInvulnerable(owner, true);
            SetTargetable(owner, false);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageSource != default)
            {
                damageAmount = 0;
            }
        }
    }
}