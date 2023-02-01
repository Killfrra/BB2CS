#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptHeimerdinger : BBCharScript
    {
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.HeimerdingerTurretDetonation(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.TechmaturgicalIcon(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            charVars.Time1 = 25000;
            charVars.Time2 = 25000;
            charVars.Time3 = 25000;
            charVars.Time4 = 25000;
            charVars.Time5 = 25000;
            charVars.Time6 = 25000;
            charVars.Level1 = 4;
            charVars.Level2 = 4;
            charVars.Level3 = 4;
            charVars.Level4 = 4;
            charVars.Level5 = 4;
            charVars.Level6 = 4;
        }
        public override void OnResurrect()
        {
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level >= 1)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.HeimerdingerTurretReady(), 2, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}