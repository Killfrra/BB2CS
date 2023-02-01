#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptMordekaiser : BBCharScript
    {
        public override void OnUpdateActions()
        {
            if(owner.IsDead)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.MordekaiserDeathParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
            else
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MordekaiserDeathParticle)) > 0)
                {
                    SpellBuffRemove(owner, nameof(Buffs.MordekaiserDeathParticle), (ObjAIBase)owner);
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.MordekaiserIronMan(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            IncPAR(owner, -180, PrimaryAbilityResourceType.Shield);
        }
        public override void OnLevelUp()
        {
            IncPermanentFlatPARPoolMod(owner, 30, PrimaryAbilityResourceType.Shield);
            IncPAR(owner, -30, PrimaryAbilityResourceType.Shield);
        }
        public override void OnResurrect()
        {
            float temp1;
            temp1 = GetPAR(owner, PrimaryAbilityResourceType.Shield);
            temp1 *= -1;
            IncPAR(owner, temp1, PrimaryAbilityResourceType.Shield);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}