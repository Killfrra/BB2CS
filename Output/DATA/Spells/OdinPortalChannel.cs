#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinPortalChannel : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShrineAura",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        Vector3 startPosition;
        Particle particleID;
        int isCancelled;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            this.startPosition = GetUnitPosition(owner);
            SpellEffectCreate(out this.particleID, out _, "TeleportHome.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            this.isCancelled = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleID);
            if(this.isCancelled == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinPortalTeleport(), 1, 1, 0.35f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            float _0_25; // UNITIALIZED
            if(ExecutePeriodically(0, ref this.lastTimeExecuted, false, _0_25))
            {
                float distance;
                distance = DistanceBetweenObjectAndPoint(owner, this.startPosition);
                if(distance > 5)
                {
                    this.isCancelled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}