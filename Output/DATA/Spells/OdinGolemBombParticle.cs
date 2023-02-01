#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinGolemBombParticle : BBBuffScript
    {
        Particle sCP;
        Particle agony;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.sCP, out _, "OdinGolemPlaceHolder.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            SpellEffectCreate(out this.agony, out _, "OdinGolemPlaceholder2.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.sCP);
            SpellEffectRemove(this.agony);
        }
    }
}