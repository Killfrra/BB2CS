#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MordekaiserSyphonOfDestruction : BBSpellScript
    {
        int[] effect0 = {24, 36, 48, 60, 72};
        public override void SelfExecute()
        {
            Vector3 castPos; // UNUSED
            float healthCost;
            float temp1;
            castPos = GetCastSpellTargetPos();
            healthCost = this.effect0[level];
            temp1 = GetHealth(owner, PrimaryAbilityResourceType.Shield);
            if(healthCost >= temp1)
            {
                healthCost = temp1 - 1;
            }
            healthCost *= -1;
            IncHealth(owner, healthCost, owner);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            Particle asdf; // UNUSED
            Particle asdf1; // UNUSED
            teamID = GetTeamID(owner);
            AddBuff((ObjAIBase)target, owner, new Buffs.MordekaiserSyphonDmg(), 100, 1, 0.001f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.MordekaiserSyphonParticle(), 1, 1, 0.2f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellEffectCreate(out asdf, out _, "mordakaiser_siphonOfDestruction_tar_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out asdf1, out _, "mordakaiser_siphonOfDestruction_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
        }
    }
}