#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class InfinityEdge : BBBuffScript
    {
        public override void OnUpdateStats()
        {
            IncFlatCritDamageMod(owner, 0.5f);
        }
    }
}