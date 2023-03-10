#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KayleInterventionAnim : BBBuffScript
    {
        public override void OnActivate()
        {
            PlayAnimation("Spell4", 0, owner, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
        }
    }
}