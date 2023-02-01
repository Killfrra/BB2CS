#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CannonBallStun : BBBuffScript
    {
        public override void OnActivate()
        {
            ApplyStun(owner, owner, 1.5f);
        }
    }
}