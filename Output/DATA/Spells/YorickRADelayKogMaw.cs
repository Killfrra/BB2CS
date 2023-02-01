#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickRADelayKogMaw : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffTextureName = "Cryophoenix_Rebirth.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(attacker.Team != owner.Team)
            {
                if(type != BuffType.INTERNAL)
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            float currentCooldown;
            float currentCooldown2;
            PlayAnimation("Death", 4, owner, false, false, true);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetInvulnerable(owner, true);
            SetTargetable(owner, false);
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
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombieKogMaw)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.YorickRAZombieKogMaw(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
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