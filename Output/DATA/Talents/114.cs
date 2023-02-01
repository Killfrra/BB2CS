#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _114 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float dodgeMod;
            dodgeMod = 0.005f * talentLevel;
            IncFlatDodgeMod(owner, dodgeMod);
        }
    }
}