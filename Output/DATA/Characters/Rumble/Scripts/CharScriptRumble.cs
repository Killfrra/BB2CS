#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptRumble : BBCharScript
    {
        float punchdmg;
        int baseCDR; // UNUSED
        int[] effect0 = {25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110};
        public override void OnUpdateStats()
        {
            level = GetLevel(owner);
            this.punchdmg = this.effect0[level];
            SetBuffToolTipVar(1, this.punchdmg);
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RumbleHeatSystem(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RumbleHeatPunchTT(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            IncPAR(owner, -100, PrimaryAbilityResourceType.Other);
            charVars.DangerZone = 50;
            charVars.ShieldAmount = 0;
            this.baseCDR = 10;
        }
        public override void OnResurrect()
        {
            float temp1;
            temp1 = GetPAR(owner, PrimaryAbilityResourceType.Other);
            temp1 *= -1;
            IncPAR(owner, temp1, PrimaryAbilityResourceType.Other);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}