#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JannaHowlingGaleAnim : BBBuffScript
    {
        public override void OnActivate()
        {
            PlayAnimation("Spell1", 0, owner, false, true, false);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
        }
    }
}