#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AkaliShadowSwipeHealingParticle : BBBuffScript
    {
        public override void OnActivate()
        {
            Particle healParticle; // UNUSED
            SpellEffectCreate(out healParticle, out _, "akali_shadowSwipe_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
        }
    }
}