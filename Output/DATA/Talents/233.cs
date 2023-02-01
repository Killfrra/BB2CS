#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _233 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            IncPercentMagicPenetrationMod(owner, 0.1f);
        }
    }
}