#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _212 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float attackDamageBonus;
            attackDamageBonus = 1 * talentLevel;
            IncFlatPhysicalDamageMod(owner, attackDamageBonus);
        }
    }
}