#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GarenCommandKill : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "GarenCommandKill",
            BuffTextureName = "Garen_CommandingPresence.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float bonusArmor;
        float bonusMR;
        int[] effect0 = {25, 25, 25, 25, 25};
        public GarenCommandKill(float bonusArmor = default, float bonusMR = default)
        {
            this.bonusArmor = bonusArmor;
            this.bonusMR = bonusMR;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusArmor);
            //RequireVar(this.bonusMR);
            //RequireVar(this.maxBonus);
        }
        public override void OnKill()
        {
            float bonusAdd;
            if(charVars.TotalBonus < charVars.MaxBonus)
            {
                bonusAdd = 0.5f + charVars.TotalBonus;
                charVars.TotalBonus = bonusAdd;
                IncPermanentFlatSpellBlockMod(owner, 0.5f);
                IncPermanentFlatArmorMod(owner, 0.5f);
                AddBuff((ObjAIBase)owner, owner, new Buffs.GarenKillBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            int level;
            float nextBuffVars_BonusArmor;
            float nextBuffVars_BonusMR;
            float nextBuffVars_MaxBonus;
            if(slot == 1)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_BonusArmor = this.bonusArmor;
                nextBuffVars_BonusMR = this.bonusMR;
                charVars.MaxBonus = this.effect0[level];
                nextBuffVars_MaxBonus = charVars.MaxBonus;
                AddBuff((ObjAIBase)owner, owner, new Buffs.GarenCommandKill(nextBuffVars_BonusArmor, nextBuffVars_BonusMR), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}