#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptNidalee_Spear : BBCharScript
    {
        public override void OnActivate()
        {
            SetTargetable(owner, false);
        }
    }
}