#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Pantheon_GrandSkyfall_Fall : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            ChannelDuration = 1.5f,
            DoesntBreakShields = true,
        };
        int[] effect0 = {400, 700, 1000};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 nextBuffVars_TargetPos;
            int nextBuffVars_DamageRank; // UNUSED
            targetPos = GetCastSpellTargetPos();
            SetCameraPosition((Champion)owner, targetPos);
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_DamageRank = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_GrandSkyfall_Fall(nextBuffVars_TargetPos), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 2, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.Pantheon_GrandSkyfall_Fall), (ObjAIBase)owner, 0);
        }
    }
}
namespace Buffs
{
    public class Pantheon_GrandSkyfall_Fall : BBBuffScript
    {
        Vector3 targetPos;
        int _1ce;
        public Pantheon_GrandSkyfall_Fall(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            //RequireVar(this.damageRank);
            SetCanAttack(owner, false);
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetNoRender(owner, true);
            this._1ce = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 targetPos;
            TeamId teamID;
            Particle a; // UNUSED
            int nextBuffVars_AttackSpeedMod; // UNUSED
            Vector3 ownerPos; // UNUSED
            float nextBuffVars_MoveSpeedMod; // UNUSED
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_GS_ParticleRed)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Pantheon_GS_ParticleRed), (ObjAIBase)owner, 0);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_GS_Particle)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Pantheon_GS_Particle), (ObjAIBase)owner, 0);
            }
            targetPos = charVars.TargetPos;
            targetPos = GetUnitPosition(target);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out a, out _, "pantheon_grandskyfall_land.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, default, default, targetPos, target, default, targetPos, true, default, default, false, false);
            targetPos = this.targetPos;
            nextBuffVars_AttackSpeedMod = 0;
            nextBuffVars_MoveSpeedMod = -0.35f;
            ownerPos = GetUnitPosition(owner);
            SetCanAttack(owner, true);
            SetTargetable(owner, true);
            SetInvulnerable(owner, false);
            SetNoRender(owner, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_GrandSkyfall_FallD(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            if(this._1ce == 0)
            {
                Vector3 targetPos;
                targetPos = this.targetPos;
                TeleportToPosition(owner, targetPos);
                this._1ce = 1;
            }
        }
    }
}