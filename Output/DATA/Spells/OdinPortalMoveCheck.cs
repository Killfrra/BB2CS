#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinPortalMoveCheck : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShrineAura",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        Vector3 startPosition;
        int isCancelled;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            this.startPosition = GetUnitPosition(owner);
            this.isCancelled = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            if(this.isCancelled == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinPortalChannel(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            float _0_25; // UNITIALIZED
            if(ExecutePeriodically(0, ref this.lastTimeExecuted, false, _0_25))
            {
                float distance;
                distance = DistanceBetweenObjectAndPoint(owner, this.startPosition);
                if(distance > 10)
                {
                    this.isCancelled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}