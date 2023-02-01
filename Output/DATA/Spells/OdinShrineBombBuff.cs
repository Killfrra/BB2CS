#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinShrineBombBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "root", },
            AutoBuffActivateEffect = new[]{ null, "dr_mundo_burning_agony_cas_02.troy", },
            BuffName = "OdinShrineBombBuff",
            BuffTextureName = "DrMundo_BurningAgony.dds",
            NonDispellable = false,
        };
        TeamId chaosTeam;
        TeamId orderTeam;
        Region orderBubble;
        Region chaosBubble;
        public override void OnActivate()
        {
            TeamId orderTeam;
            TeamId chaosTeam;
            orderTeam = TeamId.TEAM_BLUE;
            chaosTeam = TeamId.TEAM_PURPLE;
            this.chaosTeam = chaosTeam;
            this.orderTeam = orderTeam;
            this.orderBubble = AddUnitPerceptionBubble(this.orderTeam, 400, owner, 70, default, default, false);
            this.chaosBubble = AddUnitPerceptionBubble(this.chaosTeam, 400, owner, 70, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.orderBubble);
            RemovePerceptionBubble(this.chaosBubble);
        }
    }
}