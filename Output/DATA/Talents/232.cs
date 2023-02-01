#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _232 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            IncPercentArmorPenetrationMod(owner, 0.1f);
        }
    }
}