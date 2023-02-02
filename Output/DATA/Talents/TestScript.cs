#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class TestScript : BBCharScript
    {
        float[] effect0 = {0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.16f, 0.16f, 0.16f, 0.16f, 0.16f, 0.22f, 0.22f, 0.22f, 0.22f, 0.22f, 0.28f, 0.28f, 0.28f, 0.28f};
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.8f, ref charVars.LastTimeExecuted))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 400, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes))
                {
                    float nextBuffVars_AttackSpeedIncrease; // UNUSED
                    nextBuffVars_AttackSpeedIncrease = charVars.AttackSpeedIncrease;
                    AddBuff((ObjAIBase)owner, unit, new Buffs.DivineBlessingAura(), 1, default, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA);
                }
            }
            avatarVars.Test = 10;
        }
        public override void SetVarsByLevel()
        {
            charVars.AttackSpeedIncrease = this.effect0[level];
        }
    }
}