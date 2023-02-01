#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BantamTrapTargetSlow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.TROY", },
            BuffName = "Noxious Trap Target",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float damagePerTick;
        float moveSpeedMod;
        Region bubbleID;
        float lastTimeExecuted;
        public BantamTrapTargetSlow(float damagePerTick = default, float moveSpeedMod = default)
        {
            this.damagePerTick = damagePerTick;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            //RequireVar(this.damagePerTick);
            //RequireVar(this.moveSpeedMod);
            ApplyDamage(attacker, owner, this.damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.16f, 1, false, false, attacker);
            teamID = GetTeamID(attacker);
            this.bubbleID = AddPosPerceptionBubble(teamID, 400, owner.Position, 5, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedMod);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                ApplyDamage(attacker, owner, this.damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.16f, 1, false, false, attacker);
            }
        }
    }
}