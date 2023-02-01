#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UrgotDeathParticle : BBBuffScript
    {
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "UrgotDeath.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "f_flesh", default, target, default, default, false);
        }
    }
}