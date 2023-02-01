#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MaokaiDrain3Tally : BBBuffScript
    {
        float drainAmount;
        public MaokaiDrain3Tally(float drainAmount = default)
        {
            this.drainAmount = drainAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.drainAmount);
            charVars.Tally += this.drainAmount;
        }
    }
}