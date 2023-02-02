#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptSwainRaven : BBCharScript
    {
        float[] effect0 = {1.08f, 1.11f, 1.14f, 1.17f, 1.2f};
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.RebirthMarker(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RebirthReady(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.SwainTorment)) > 0)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level > 0)
                {
                    float damagePercent;
                    damagePercent = this.effect0[level];
                    damageAmount *= damagePercent;
                }
            }
        }
    }
}