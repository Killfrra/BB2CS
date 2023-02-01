#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptTalon : BBCharScript
    {
        int[] effect0 = {1, 1, 1, 1, 1};
        public override void OnUpdateActions()
        {
            float bonusAD;
            float wBonusAD;
            float eBonusAD;
            float qTotalBonus;
            float rBonusAD;
            float qMagicBonus; // UNUSED
            float baseDamage;
            float totalAD;
            float w2BonusAD;
            int qDamagePercentVal; // UNUSED
            float baseAP;
            float qBonusAD2;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            bonusAD = 0;
            wBonusAD = 0;
            eBonusAD = 0;
            qTotalBonus = 0;
            rBonusAD = 0;
            qMagicBonus = 0;
            baseDamage = GetBaseAttackDamage(owner);
            totalAD = GetTotalAttackDamage(owner);
            bonusAD = totalAD - baseDamage;
            wBonusAD = bonusAD * 0.6f;
            wBonusAD = MathF.Ceiling(wBonusAD);
            SetSpellToolTipVar(wBonusAD, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)target);
            w2BonusAD = bonusAD * 0.3f;
            w2BonusAD = MathF.Ceiling(w2BonusAD);
            SetSpellToolTipVar(w2BonusAD, 2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)target);
            eBonusAD = bonusAD * 1.2f;
            eBonusAD = MathF.Ceiling(eBonusAD);
            SetSpellToolTipVar(eBonusAD, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)target);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            qDamagePercentVal = this.effect0[level];
            qTotalBonus = bonusAD * 0.3f;
            qTotalBonus = MathF.Ceiling(qTotalBonus);
            SetSpellToolTipVar(qTotalBonus, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)target);
            rBonusAD = bonusAD * 0.9f;
            rBonusAD = MathF.Ceiling(rBonusAD);
            SetSpellToolTipVar(rBonusAD, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)target);
            baseAP = GetFlatMagicDamageMod(owner);
            qMagicBonus = baseAP * 0.1f;
            qBonusAD2 = bonusAD * 1.2f;
            qBonusAD2 = MathF.Ceiling(qBonusAD2);
            SetSpellToolTipVar(qBonusAD2, 2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)target);
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            string slotName;
            slotName = GetSpellName();
            if(slotName == nameof(Spells.BladeRogue_ShackleShot))
            {
                charVars.FirstTargetHit = false;
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.LastHit = 0;
            charVars.AttackCounter = 1;
            charVars.MissileNumber = 0;
            AddBuff(attacker, target, new Buffs.TalonMercy(), 1, 1, 250000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class CharScriptTalon : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Wolfman_InnerHunger.dds",
        };
        public override void OnResurrect()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
    }
}