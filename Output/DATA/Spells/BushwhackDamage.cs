#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BushwhackDamage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "global_Watched.troy", },
            BuffName = "BushwhackDamage",
            BuffTextureName = "NIdalee_Bushwhack.dds",
        };
        float dOTCounter;
        float damagePerTick;
        int dotCounter; // UNUSED
        Region bubbleID;
        Region bubbleID2;
        float lastTimeExecuted;
        public BushwhackDamage(float dOTCounter = default, float damagePerTick = default)
        {
            this.dOTCounter = dOTCounter;
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            TeamId team;
            ApplyAssistMarker(attacker, owner, 10);
            this.dotCounter = 4;
            team = GetTeamID(attacker);
            this.bubbleID = AddUnitPerceptionBubble(team, 400, owner, 20, default, default, false);
            this.bubbleID2 = AddUnitPerceptionBubble(team, 50, owner, 20, default, default, true);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                if(this.dOTCounter < 4)
                {
                    this.dOTCounter++;
                    ApplyDamage(attacker, owner, this.damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.1f, 1, false, false, attacker);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
            RemovePerceptionBubble(this.bubbleID2);
        }
    }
}