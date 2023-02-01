#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinRecallImproved : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Recall",
            BuffTextureName = "RecallHome.dds",
        };
        bool willRemove;
        public OdinRecallImproved(bool willRemove = default)
        {
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            //RequireVar(this.willRemove);
        }
        public override void OnUpdateActions()
        {
            if(this.willRemove)
            {
                StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageSource != default)
            {
                this.willRemove = true;
            }
        }
    }
}
namespace Spells
{
    public class OdinRecallImproved : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 4f,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        Particle particleID;
        public override void ChannelingStart()
        {
            bool nextBuffVars_WillRemove;
            nextBuffVars_WillRemove = false;
            SpellEffectCreate(out this.particleID, out _, "TeleportHome_shortImproved.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinRecallImproved(nextBuffVars_WillRemove), 1, 1, 4.4f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            TeamId teamID;
            Vector3 camPos;
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                TeleportToKeyLocation(attacker, SpawnType.SPAWN_LOCATION, TeamId.TEAM_BLUE);
            }
            else if(true)
            {
                TeleportToKeyLocation(attacker, SpawnType.SPAWN_LOCATION, TeamId.TEAM_PURPLE);
            }
            camPos = GetUnitPosition(owner);
            SetCameraPosition((Champion)owner, camPos);
            SpellEffectCreate(out _, out _, "teleportarrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void ChannelingCancelStop()
        {
            SpellEffectRemove(this.particleID);
            SpellBuffRemove(owner, nameof(Buffs.OdinRecallImproved), (ObjAIBase)owner, 0);
        }
    }
}