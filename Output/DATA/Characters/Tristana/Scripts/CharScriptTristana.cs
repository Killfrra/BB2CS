#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptTristana : BBCharScript
    {
        int[] effect0 = {0, 9, 18, 27, 36, 45, 54, 63, 72, 81, 90, 99, 108, 117, 126, 135, 144, 153, 162};
        public override void OnUpdateStats()
        {
            IncFlatAttackRangeMod(owner, charVars.BonusRange);
        }
        public override void OnUpdateActions()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DetonatingShot)) > 0)
            {
            }
            else
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level >= 1)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.DetonatingShot(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
                }
            }
        }
        public override void SetVarsByLevel()
        {
            charVars.BonusRange = this.effect0[level];
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.DrawABead(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false);
        }
    }
}