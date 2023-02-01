#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class PreAttackTest : BBCharScript
    {
        public override void OnPreAttack()
        {
            DebugSay(owner, "Avatar PreAttack event.");
        }
    }
}