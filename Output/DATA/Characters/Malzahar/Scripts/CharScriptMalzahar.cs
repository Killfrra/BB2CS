#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptMalzahar : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.4f, ref this.lastTimeExecuted, false))
            {
                if(owner.IsDead)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AlZaharDeathParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
                }
                else
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.AlZaharDeathParticle)) > 0)
                    {
                        SpellBuffRemove(owner, nameof(Buffs.AlZaharDeathParticle), (ObjAIBase)owner);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.AlZaharVoidlingDetonation(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, default, false);
        }
    }
}