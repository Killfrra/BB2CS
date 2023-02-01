#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TurretLifeSpan : BBBuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            Particle poofout; // UNUSED
            ApplyDamage((ObjAIBase)owner, owner, 1500, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1);
            SpellEffectCreate(out poofout, out _, "jackintheboxpoof.troy", default, default, default, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target);
        }
    }
}