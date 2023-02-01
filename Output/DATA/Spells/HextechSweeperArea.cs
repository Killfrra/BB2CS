#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HextechSweeperArea : BBBuffScript
    {
        Vector3 targetPos;
        Particle particle;
        Region bubbleID;
        float lastTimeExecuted;
        public HextechSweeperArea(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            TeamId casterID;
            Vector3 targetPos;
            //RequireVar(this.targetPos);
            casterID = GetTeamID(attacker);
            targetPos = this.targetPos;
            if(casterID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particle, out _, "Odin_HextechSweeper_tar_green.troy", default, TeamId.TEAM_BLUE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "Odin_HextechSweeper_tar_green.troy", default, TeamId.TEAM_PURPLE, 250, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, false, false, false, false);
            }
            this.bubbleID = AddPosPerceptionBubble(casterID, 550, targetPos, 6, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
            SpellEffectRemove(this.particle);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, this.targetPos, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff(attacker, unit, new Buffs.OdinLightbringer(), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
        }
    }
}