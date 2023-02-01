#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinPortalTeleport : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShrineAura",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        Vector3 startPosition;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            this.startPosition = GetUnitPosition(owner);
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 currentPos;
            currentPos = GetUnitPosition(owner);
            SetCameraPosition((Champion)owner, currentPos);
        }
        public override void OnUpdateActions()
        {
            float _0_25; // UNITIALIZED
            float distance;
            if(ExecutePeriodically(0, ref this.lastTimeExecuted, false, _0_25))
            {
                distance = DistanceBetweenObjectAndPoint(owner, this.startPosition);
                if(distance > 10)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}