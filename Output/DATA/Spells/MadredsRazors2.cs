#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MadredsRazors2 : BBBuffScript
    {
        public override void OnPreAttack()
        {
            if(target is ObjAIBase)
            {
                if(RandomChance() < 0.15f)
                {
                    if(target is BaseTurret)
                    {
                    }
                    else
                    {
                        if(target is Champion)
                        {
                        }
                        else
                        {
                            AddBuff(attacker, target, new Buffs.MadredsRazors(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0);
                        }
                    }
                }
            }
        }
    }
}