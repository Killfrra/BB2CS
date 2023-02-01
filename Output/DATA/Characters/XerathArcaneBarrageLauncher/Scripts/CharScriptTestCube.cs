#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptTestCube : BBCharScript
    {
        public override void OnActivate()
        {
            SetNoRender(owner, true);
        }
    }
}