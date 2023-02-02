#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class UdyrBearStance : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {2, 2.5f, 3, 3.5f, 4};
        public override void SelfExecute()
        {
            float cooldownPerc;
            float currentCD;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrPhoenixStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrPhoenixStance), (ObjAIBase)owner);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTurtleStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrTurtleStance), (ObjAIBase)owner);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UdyrTigerStance)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.UdyrTigerStance), (ObjAIBase)owner);
            }
            cooldownPerc = GetPercentCooldownMod(owner);
            cooldownPerc++;
            cooldownPerc *= 1.5f;
            currentCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            currentCD = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(currentCD <= cooldownPerc)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownPerc);
            }
            AddBuff((ObjAIBase)owner, owner, new Buffs.UdyrBearStance(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UdyrBearActivation(), 1, 1, this.effect0[level], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class UdyrBearStance : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "R_hand", "L_hand", },
            AutoBuffActivateEffect = new[]{ "Global_DmgHands_buf.troy", "Global_DmgHands_buf.troy", },
            BuffName = "UdyrBearStance",
            BuffTextureName = "Udyr_BearStance.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };
        int casterID; // UNUSED
        public override void OnActivate()
        {
            this.casterID = PushCharacterData("Udyr", owner, false);
            OverrideAutoAttack(3, SpellSlotType.ExtraSlots, owner, 1, true);
            OverrideAnimation("Run", "Run2", owner);
            OverrideAnimation("Idle1", "Idle2", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, true);
            ClearOverrideAnimation("Run", owner);
            ClearOverrideAnimation("Idle1", owner);
        }
    }
}