#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class XerathArcaneBarrage : BBBuffScript
    {
        Particle particle1;
        Particle particle;
        public override void OnActivate()
        {
            TeamId teamID;
            //RequireVar(this.level);
            //RequireVar(this.damageAmount);
            //RequireVar(this.slowAmount);
            //RequireVar(this.distance);
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out this.particle1, out this.particle, "Xerath_E_cas_green.troy", "Xerath_E_cas_red.troy", teamID, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, false, false, false, false);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle1);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
    }
}
namespace Spells
{
    public class XerathArcaneBarrage : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        int[] effect1 = {150, 200, 250, 0, 0};
        public override void SelfExecute()
        {
            TeamId teamOfOwner;
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            float nextBuffVars_Distance;
            float nextBuffVars_SlowAmount;
            int nextBuffVars_DamageAmount;
            int nextBuffVars_Level;
            Region nextBuffVars_Bubble;
            Particle a; // UNUSED
            Minion other3;
            teamOfOwner = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(targetPos, ownerPos);
            nextBuffVars_Distance = distance;
            SpellEffectCreate(out a, out _, "Xerath_E_cas.troy", default, TeamId.TEAM_BLUE, 100, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "chest", default, attacker, default, default, true, false, false, false, false);
            other3 = SpawnMinion("HiddenMinion", "XerathArcaneBarrageLauncher", "idle.lua", targetPos, teamOfOwner, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            nextBuffVars_SlowAmount = this.effect0[level];
            nextBuffVars_DamageAmount = this.effect1[level];
            nextBuffVars_Level = level;
            AddBuff(attacker, other3, new Buffs.XerathArcaneBarrage(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            nextBuffVars_Bubble = AddPosPerceptionBubble(teamOfOwner, 600, targetPos, 4, default, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.XerathArcaneBarrageVision(nextBuffVars_Bubble), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast(other3, owner, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, true, false, false);
        }
    }
}