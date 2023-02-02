#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Takedown : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {5, 5, 5, 5, 5};
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown;
            nextBuffVars_SpellCooldown = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.Takedown(nextBuffVars_SpellCooldown), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}
namespace Buffs
{
    public class Takedown : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "R_hand", "L_hand", "neck", },
            AutoBuffActivateEffect = new[]{ "", "", "nidalee_takedown_cas_02.troy", "", },
            BuffName = "Takedown",
            BuffTextureName = "Nidalee_TakeDown.dds",
        };
        float spellCooldown;
        public Takedown(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetDodgePiercing(owner, true);
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
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
            SetDodgePiercing(owner, false);
            RemoveOverrideAutoAttack(owner, true);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
    }
}