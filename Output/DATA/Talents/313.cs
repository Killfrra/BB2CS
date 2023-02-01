#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _313 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float armorMod;
            armorMod = 2 * talentLevel;
            IncFlatArmorMod(owner, armorMod);
        }
    }
}