#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FizzJumpDelay : BBBuffScript
    {
        int[] effect0 = {16, 14, 12, 10, 8};
        public override void OnActivate()
        {
            Vector3 targetPos; // UNUSED
            TeamId teamID; // UNUSED
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.FizzJumpTwo));
            SetSlotSpellCooldownTimeVer2(0, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            targetPos = GetUnitPosition(owner);
            teamID = GetTeamID(owner);
            OverrideAnimation("Idle1", "Spell3b", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.FizzJumpTwo)) == 0)
            {
                float cDReduction;
                int level;
                float baseCD;
                float lowerCD;
                float newCD;
                SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
                SetTargetable(owner, true);
                SetGhosted(owner, false);
                SetCanAttack(owner, true);
                SetCanAttack(owner, true);
                SetSilenced(owner, false);
                SetForceRenderParticles(owner, false);
                SetCallForHelpSuppresser(owner, false);
                SetInvulnerable(owner, false);
                PlayAnimation("Spell3c", 0, owner, false, true, true);
                AddBuff((ObjAIBase)owner, owner, new Buffs.FizzTrickSlamSoundDummy(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                AddBuff((ObjAIBase)owner, owner, new Buffs.FizzTrickSlam(), 1, 1, 0.45f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
                SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
                SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.FizzJump));
                cDReduction = GetPercentCooldownMod(owner);
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                baseCD = this.effect0[level];
                lowerCD = baseCD * cDReduction;
                newCD = baseCD + lowerCD;
                SetSlotSpellCooldownTimeVer2(newCD, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
            ClearOverrideAnimation("Idle1", owner);
        }
    }
}