#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MercuryTreads : BBBuffScript
    {
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.SNARE)
                {
                    duration *= 0.65f;
                }
                if(type == BuffType.SLOW)
                {
                    duration *= 0.65f;
                }
                if(type == BuffType.FEAR)
                {
                    duration *= 0.65f;
                }
                if(type == BuffType.CHARM)
                {
                    duration *= 0.65f;
                }
                if(type == BuffType.SLEEP)
                {
                    duration *= 0.65f;
                }
                if(type == BuffType.STUN)
                {
                    duration *= 0.65f;
                }
                if(type == BuffType.TAUNT)
                {
                    duration *= 0.65f;
                }
            }
            return returnValue;
        }
    }
}