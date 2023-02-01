#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _322 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float healthPerLevel;
            int champLevel;
            float healthMod;
            healthPerLevel = talentLevel * 1.5f;
            champLevel = GetLevel(owner);
            healthMod = champLevel * healthPerLevel;
            IncMaxHealth(owner, healthMod, false);
        }
    }
}