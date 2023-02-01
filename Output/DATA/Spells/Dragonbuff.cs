#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Dragonbuff : BBBuffScript
    {
        public override void OnDeath()
        {
            AddBuff((ObjAIBase)owner, attacker, new Buffs.FireoftheGreatDragon(), 1, 1, 180, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
        }
    }
}