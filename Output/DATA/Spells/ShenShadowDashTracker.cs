#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShenShadowDashTracker : BBBuffScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.ShenShadowDashPassive(), 1, 1, 1.2f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
            }
        }
    }
}