#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeblancMIFull : BBBuffScript
    {
        Particle particle; // UNUSED
        float lastTimeExecuted;
        public override void OnActivate()
        {
            TeamId teamID;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeblancPassive)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.LeblancPassive), (ObjAIBase)owner, 0);
            }
            teamID = GetTeamID(attacker);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particle, out _, "LeblancImage.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_BLUE, default, default, true, owner, "root", default, target, "root", default, false, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "LeblancImage.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_PURPLE, default, default, true, owner, "root", default, target, "root", default, false, default, default, false, false);
            }
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            Particle hi; // UNUSED
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out hi, out _, "leblanc_mirrorimage_death.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, owner, default, default, true, default, default, false, false);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, (ObjAIBase)owner);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                if(attacker.IsDead)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            damageAmount *= 0;
        }
    }
}