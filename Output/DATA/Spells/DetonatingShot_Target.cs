#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DetonatingShot_Target : BBBuffScript
    {
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}