#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AkaliShadowDance : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "AkaliShadowDance",
            BuffTextureName = "AkaliShadowDance.dds",
            PersistsThroughDeath = true,
        };
        public override void OnUpdateAmmo()
        {
            int count;
            count = GetBuffCountFromAll(owner, nameof(Buffs.AkaliShadowDance));
            if(count >= 3)
            {
                AddBuff(attacker, owner, new Buffs.AkaliShadowDance(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
            else
            {
                AddBuff(attacker, owner, new Buffs.AkaliShadowDance(), 4, 1, charVars.DanceTimerCooldown, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
        }
    }
}
namespace Spells
{
    public class AkaliShadowDance : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChainMissileParameters = new()
            {
                CanHitCaster = 0,
                CanHitEnemies = 1,
                CanHitFriends = 0,
                CanHitSameTarget = 0,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 10, 10, 10, 10, 10, },
            },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {100, 175, 250, 0, 0};
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove;
            bool canCast;
            int count;
            canMove = GetCanMove(owner);
            canCast = GetCanCast(owner);
            count = GetBuffCountFromAll(owner, nameof(Buffs.AkaliShadowDance));
            if(count <= 1)
            {
                returnValue = false;
            }
            else
            {
                if(!canMove)
                {
                    returnValue = false;
                }
                else if(!canCast)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int count;
            Particle smokeBomb; // UNUSED
            Vector3 ownerPos;
            Particle p3; // UNUSED
            Vector3 targetPos;
            float moveSpeed;
            float dashSpeed;
            float distance;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_Distance;
            float nextBuffVars_dashSpeed;
            int nextBuffVars_DamageVar;
            count = GetBuffCountFromAll(owner, nameof(Buffs.AkaliShadowDance));
            if(count > 3)
            {
                SpellBuffRemove(owner, nameof(Buffs.AkaliShadowDance), (ObjAIBase)owner, charVars.DanceTimerCooldown);
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.AkaliShadowDance), (ObjAIBase)owner, 0);
            }
            SpellEffectCreate(out smokeBomb, out _, "akali_shadowDance_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            ownerPos = GetUnitPosition(owner);
            SpellEffectCreate(out p3, out _, "akali_shadowDance_return_02.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out p3, out _, "akali_shadowDance_return.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
            targetPos = GetCastSpellTargetPos();
            moveSpeed = GetMovementSpeed(owner);
            dashSpeed = moveSpeed + 1600;
            distance = DistanceBetweenObjects("Owner", "Target");
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_Distance = distance;
            nextBuffVars_dashSpeed = dashSpeed;
            nextBuffVars_DamageVar = this.effect0[level];
            AddBuff((ObjAIBase)target, owner, new Buffs.AkaliShadowDanceKick(nextBuffVars_TargetPos, nextBuffVars_Distance, nextBuffVars_dashSpeed, nextBuffVars_DamageVar), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
        }
    }
}