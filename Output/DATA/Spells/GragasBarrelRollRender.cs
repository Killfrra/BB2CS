#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GragasBarrelRollRender : BBBuffScript
    {
        public override void OnActivate()
        {
            SetCallForHelpSuppresser(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
        }
    }
}