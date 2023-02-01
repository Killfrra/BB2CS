#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DragonApplicator : BBBuffScript
    {
        public override void OnDeath()
        {
            TeamId teamID;
            float newDuration;
            teamID = GetTeamID(attacker);
            if(teamID == TeamId.TEAM_BLUE)
            {
                foreach(Champion unit in GetChampions(TeamId.TEAM_BLUE, default, true))
                {
                    newDuration = 120;
                    if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonsterBuffs)) > 0)
                    {
                        newDuration *= 1.15f;
                    }
                    else
                    {
                        if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.Monsterbuffs2)) > 0)
                        {
                            newDuration *= 1.3f;
                        }
                    }
                    if(!unit.IsDead)
                    {
                        AddBuff(unit, unit, new Buffs.CrestofCrushingWrath(), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                    }
                }
            }
            else if(teamID == TeamId.TEAM_PURPLE)
            {
                foreach(Champion unit in GetChampions(TeamId.TEAM_PURPLE, default, true))
                {
                    newDuration = 120;
                    if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonsterBuffs)) > 0)
                    {
                        newDuration *= 1.15f;
                    }
                    else
                    {
                        if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.Monsterbuffs2)) > 0)
                        {
                            newDuration *= 1.3f;
                        }
                    }
                    if(!unit.IsDead)
                    {
                        AddBuff(unit, unit, new Buffs.CrestofCrushingWrath(), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
                    }
                }
            }
        }
    }
}