#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JackInTheBoxHardLock : BBBuffScript
    {
        public override void OnActivate()
        {
            SpellBuffClear(owner, nameof(Buffs.JackInTheBoxSoftLock));
        }
    }
}