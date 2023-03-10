#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LuxMaliceCannonBall : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
        };
        Particle particle;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.particle, out _, "LuxMaliceCannon_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "top", default, target, default, default, false);
            SetNoRender(owner, true);
            SetInvulnerable(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, (ObjAIBase)owner);
        }
    }
}