#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingKillCloneW : BBBuffScript
    {
        bool doOnce;
        Particle a;
        Particle b;
        Particle c;
        Particle d;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            TeamId teamID;
            this.doOnce = false;
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.a, out _, "monkeyKing_W_cas_team_ID_green.troy", default, teamID, 10, 0, TeamId.TEAM_BLUE, default, owner, false, owner, "L_hand", default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.b, out _, "monkeyKing_W_cas_team_ID_green.troy", default, teamID, 10, 0, TeamId.TEAM_BLUE, default, owner, false, owner, "R_hand", default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.c, out _, "monkeyKing_W_cas_team_ID_red.troy", default, teamID, 10, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "L_hand", default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.d, out _, "monkeyKing_W_cas_team_ID_red.troy", default, teamID, 10, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "R_hand", default, owner, default, default, false, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.a, out _, "monkeyKing_W_cas_team_ID_green.troy", default, teamID, 10, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "L_hand", default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.b, out _, "monkeyKing_W_cas_team_ID_green.troy", default, teamID, 10, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "R_hand", default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.c, out _, "monkeyKing_W_cas_team_ID_red.troy", default, teamID, 10, 0, TeamId.TEAM_BLUE, default, owner, false, owner, "L_hand", default, owner, default, default, false, default, default, false, false);
                SpellEffectCreate(out this.d, out _, "monkeyKing_W_cas_team_ID_red.troy", default, teamID, 10, 0, TeamId.TEAM_BLUE, default, owner, false, owner, "R_hand", default, owner, default, default, false, default, default, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.a);
            SpellEffectRemove(this.b);
            SpellEffectRemove(this.c);
            SpellEffectRemove(this.d);
            SetInvulnerable(owner, false);
            SetTargetable(owner, true);
            SetGhosted(owner, false);
            SetNoRender(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            Vector3 ownerPos;
            TeamId teamID;
            Particle aa; // UNUSED
            Particle bb; // UNUSED
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                SetNoRender(owner, true);
                if(!this.doOnce)
                {
                    ownerPos = GetUnitPosition(owner);
                    teamID = GetTeamID(owner);
                    if(teamID == TeamId.TEAM_BLUE)
                    {
                        SpellEffectCreate(out aa, out _, "MonkeyKing_W_death_team_ID_green.troy", default, teamID, 10, 0, TeamId.TEAM_BLUE, default, owner, false, default, default, ownerPos, owner, default, ownerPos, true, default, default, false, false);
                        SpellEffectCreate(out bb, out _, "MonkeyKing_W_death_team_ID_red.troy", default, teamID, 10, 0, TeamId.TEAM_PURPLE, default, owner, false, default, default, ownerPos, owner, default, ownerPos, true, default, default, false, false);
                    }
                    else
                    {
                        SpellEffectCreate(out aa, out _, "MonkeyKing_W_death_team_ID_green.troy", default, teamID, 10, 0, TeamId.TEAM_PURPLE, default, owner, false, default, default, ownerPos, owner, default, ownerPos, true, default, default, false, false);
                        SpellEffectCreate(out bb, out _, "MonkeyKing_W_death_team_ID_red.troy", default, teamID, 10, 0, TeamId.TEAM_BLUE, default, owner, false, default, default, ownerPos, owner, default, ownerPos, true, default, default, false, false);
                    }
                    this.doOnce = true;
                }
            }
        }
    }
}