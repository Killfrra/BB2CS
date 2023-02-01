#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3020 : BBItemScript
    {
        public override void OnUpdateStats()
        {
            IncFlatMagicPenetrationMod(owner, 20);
        }
    }
}