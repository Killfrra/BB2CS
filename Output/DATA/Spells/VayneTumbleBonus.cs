#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VayneTumbleBonus : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "l_hand", "r_hand", },
            AutoBuffActivateEffect = new[]{ "vayne_Q_buf.troy", "vayne_Q_buf.troy", "vayne_q_cas.troy", },
            BuffName = "VayneTumble",
            BuffTextureName = "Vayne_Tumble.dds",
        };
        int[] effect0 = {6, 5, 4, 3, 2};
        public override void OnActivate()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneInquisition)) > 0)
            {
                OverrideAnimation("Idle1", "Idle_TumbleUlt", owner);
                OverrideAnimation("Idle2", "Idle_TumbleUlt", owner);
                OverrideAnimation("Idle3", "Idle_TumbleUlt", owner);
                OverrideAnimation("Idle4", "Idle_TumbleUlt", owner);
                OverrideAnimation("Attack1", "Attack_TumbleUlt", owner);
                OverrideAnimation("Attack2", "Attack_TumbleUlt", owner);
                OverrideAnimation("Crit", "Attack_TumbleUlt", owner);
                OverrideAnimation("Spell3", "Attack_TumbleUlt", owner);
                OverrideAnimation("Run", "Run_TumbleUlt", owner);
                OverrideAutoAttack(5, SpellSlotType.ExtraSlots, owner, 1, false);
            }
            else
            {
                OverrideAnimation("Idle1", "Idle_Tumble", owner);
                OverrideAnimation("Idle2", "Idle_Tumble", owner);
                OverrideAnimation("Idle3", "Idle_Tumble", owner);
                OverrideAnimation("Idle4", "Idle_Tumble", owner);
                OverrideAnimation("Attack1", "Attack_Tumble", owner);
                OverrideAnimation("Attack2", "Attack_Tumble", owner);
                OverrideAnimation("Crit", "Attack_Tumble", owner);
                OverrideAnimation("Run", "Run_Tumble", owner);
                OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            float spellCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            spellCooldown = this.effect0[level];
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            RemoveOverrideAutoAttack(owner, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneInquisition)) > 0)
            {
                OverrideAnimation("Idle1", "Idle_Ult", owner);
                OverrideAnimation("Idle2", "Idle_Ult", owner);
                OverrideAnimation("Idle3", "Idle_Ult", owner);
                OverrideAnimation("Idle4", "Idle_Ult", owner);
                OverrideAnimation("Attack1", "Attack_Ult", owner);
                OverrideAnimation("Attack2", "Attack_Ult", owner);
                OverrideAnimation("Crit", "Attack_Ult", owner);
                OverrideAnimation("Spell3", "Attack_Ult", owner);
                OverrideAnimation("Run", "Run_Ult", owner);
                OverrideAutoAttack(4, SpellSlotType.ExtraSlots, owner, 1, false);
            }
            else
            {
                ClearOverrideAnimation("Idle1", owner);
                ClearOverrideAnimation("Idle2", owner);
                ClearOverrideAnimation("Idle3", owner);
                ClearOverrideAnimation("Idle4", owner);
                ClearOverrideAnimation("Attack1", owner);
                ClearOverrideAnimation("Attack2", owner);
                ClearOverrideAnimation("Crit", owner);
                ClearOverrideAnimation("Spell3", owner);
                ClearOverrideAnimation("Run", owner);
            }
        }
    }
}