#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptTutorial_Red_Minion_Basic : BBCharScript
    {
        public override void OnActivate()
        {
            Particle part; // UNUSED
            SpellEffectCreate(out part, out _, "HallucinatePoof.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, owner.Position, false);
        }
    }
}