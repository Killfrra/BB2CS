#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CaitlynHeadshot : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "R_BUFFBONE_GLB_HAND_LOC", "L_BUFFBONE_GLB_HAND_LOC", },
            AutoBuffActivateEffect = new[]{ "caitlyn_headshot_rdy_indicator.troy", "caitlyn_headshot_rdy_indicator.troy", },
            BuffName = "CaitlynHeadshotReady",
            BuffTextureName = "Caitlyn_Headshot2.dds",
        };
        public override void OnActivate()
        {
            SetDodgePiercing(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetDodgePiercing(owner, false);
        }
        public override void OnPreAttack()
        {
            RemoveOverrideAutoAttack(owner, false);
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, true);
                }
            }
        }
    }
}