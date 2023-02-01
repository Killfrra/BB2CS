#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PhosphorusBombBlind : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "global_Watched.troy", },
            BuffName = "PhosphorusBomb",
            BuffTextureName = "Corki_PhosphorusBomb.dds",
        };
        Region bubbleID;
        public override void OnActivate()
        {
            TeamId team;
            team = GetTeamID(attacker);
            this.bubbleID = AddUnitPerceptionBubble(team, 400, owner, 6, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
        }
    }
}