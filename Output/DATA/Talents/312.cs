#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _312 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float magicResistanceMod;
            magicResistanceMod = 2 * talentLevel;
            IncFlatSpellBlockMod(owner, magicResistanceMod);
        }
    }
}