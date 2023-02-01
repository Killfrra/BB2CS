#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3110 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            if(!owner.IsDead)
            {
                if(ExecutePeriodically(0.9f, ref this.lastTimeExecuted, false))
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.FrozenHeart(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.FrozenHeartAura(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.FrozenHeart)) > 0)
            {
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.FrozenHeart(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}