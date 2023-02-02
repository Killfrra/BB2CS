#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XerathArcanopulseBall : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
        };
        Particle particle;
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.particle, out _, "Xerath_beam_cas.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 550, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "top", default, target, default, default, true, false, false, false, false);
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