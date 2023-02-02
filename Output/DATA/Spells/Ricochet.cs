#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Ricochet : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {9, 7.5f, 6, 4.5f, 3};
        public override void SelfExecute()
        {
            float nextBuffVars_SpellCooldown;
            SetSlotSpellCooldownTimeVer2(0, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            nextBuffVars_SpellCooldown = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.Ricochet(nextBuffVars_SpellCooldown), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Ricochet : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "weapon", "", },
            AutoBuffActivateEffect = new[]{ "SivirRicochetBuff.troy", "", },
            BuffName = "Ricochet",
            BuffTextureName = "Sivir_Ricochet.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float spellCooldown;
        public Ricochet(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            int level;
            //RequireVar(this.spellCooldown);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, level, false);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
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
            SetSlotSpellCooldownTimeVer2(newCooldown, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            RemoveOverrideAutoAttack(owner, false);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnLaunchAttack()
        {
            SpellBuffRemove(owner, nameof(Buffs.Ricochet), (ObjAIBase)owner, 0);
        }
    }
}