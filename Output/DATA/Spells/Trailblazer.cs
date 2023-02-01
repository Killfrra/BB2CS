#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Trailblazer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Eagle Eye",
            BuffTextureName = "Teemo_EagleEye.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTooltip;
        float lastTimeExecuted;
        int[] effect0 = {6, 6, 6, 8, 8, 8, 10, 10, 10, 12, 12, 12, 14, 14, 14, 16, 16, 16};
        public override void OnActivate()
        {
            this.lastTooltip = 0;
        }
        public override void OnUpdateActions()
        {
            Vector3 curPos;
            TeamId teamID;
            Minion other3;
            float moveSpeed;
            float moveSpeedMod;
            float nextBuffVars_MoveSpeedMod;
            int level;
            float tooltipAmount;
            curPos = GetPointByUnitFacingOffset(owner, 30, 180);
            teamID = GetTeamID(attacker);
            other3 = SpawnMinion("AcidTrail", "TestCube", "idle.lua", curPos, teamID, true, false, false, true, false, true, 0, default, default, (Champion)attacker);
            moveSpeed = GetMovementSpeed(attacker);
            moveSpeedMod = moveSpeed / 2500;
            nextBuffVars_MoveSpeedMod = moveSpeedMod;
            AddBuff((ObjAIBase)owner, other3, new Buffs.TrailblazerApplicator(nextBuffVars_MoveSpeedMod), 1, 1, charVars.TrailDuration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, true))
            {
                level = GetLevel(owner);
                tooltipAmount = this.effect0[level];
                if(tooltipAmount > this.lastTooltip)
                {
                    this.lastTooltip = tooltipAmount;
                    SetBuffToolTipVar(1, tooltipAmount);
                }
            }
        }
    }
}