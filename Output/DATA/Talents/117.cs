#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _117 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            IncFlatGoldPer10Mod(owner, 1);
        }
    }
}