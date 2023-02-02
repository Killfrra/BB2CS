#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class XenZhaoComboTarget : BBSpellScript
    {
        public override void SelfExecute()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.XenZhaoComboTarget(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class XenZhaoComboTarget : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XenZhaoComboTarget",
            BuffTextureName = "XinZhao_ThreeTalon.dds",
            SpellToggleSlot = 1,
        };
        Particle asdf2;
        Particle asdf1;
        int[] effect0 = {10, 10, 10, 10, 10};
        public override void OnActivate()
        {
            CancelAutoAttack(owner, true);
            SpellEffectCreate(out this.asdf2, out _, "xenZiou_ChainAttack_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_hand", default, owner, default, default, false);
            SpellEffectCreate(out this.asdf1, out _, "xenZiou_ChainAttack_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_hand", default, owner, default, default, false);
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.asdf2);
            SpellEffectRemove(this.asdf1);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            if(expired)
            {
                float cDMod;
                int level;
                float cooldownByLevel;
                float modulatedCD;
                float trueCD;
                cDMod = GetPercentCooldownMod(owner);
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                cooldownByLevel = this.effect0[level];
                modulatedCD = 1 + cDMod;
                trueCD = modulatedCD * cooldownByLevel;
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, trueCD);
            }
        }
        public override void OnPreAttack()
        {
            if(target is not BaseTurret)
            {
                if(target is ObjAIBase)
                {
                    int level;
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    SkipNextAutoAttack(owner);
                    SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 0, SpellSlotType.ExtraSlots, level, false, false, false, false, true, false);
                }
            }
        }
    }
}