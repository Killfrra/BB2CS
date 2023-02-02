#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class NocturneParanoia2 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        Particle greenDash;
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove;
            bool canCast;
            canMove = GetCanMove(owner);
            canCast = GetCanCast(owner);
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
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamOfOwner;
            teamOfOwner = GetTeamID(owner);
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.NocturneParanoia)) > 0)
            {
                Vector3 ownerPos; // UNUSED
                Vector3 targetPos;
                float distance;
                int nextBuffVars_dashSpeed;
                Vector3 nextBuffVars_TargetPos;
                float nextBuffVars_Distance; // UNUSED
                Particle nextBuffVars_GreenDash;
                if(teamOfOwner == TeamId.TEAM_BLUE)
                {
                    foreach(Champion unit in GetChampions(TeamId.TEAM_PURPLE, default, true))
                    {
                        SpellBuffRemove(unit, nameof(Buffs.NocturneParanoiaTargeting), attacker);
                    }
                }
                else
                {
                    foreach(Champion unit in GetChampions(TeamId.TEAM_BLUE, default, true))
                    {
                        SpellBuffRemove(unit, nameof(Buffs.NocturneParanoiaTargeting), attacker);
                    }
                }
                ownerPos = GetUnitPosition(attacker);
                targetPos = GetUnitPosition(target);
                distance = DistanceBetweenObjects("Owner", "Target");
                nextBuffVars_dashSpeed = 1800;
                nextBuffVars_TargetPos = targetPos;
                nextBuffVars_Distance = distance;
                AddBuff((ObjAIBase)owner, owner, new Buffs.UnstoppableForceMarker(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if(teamOfOwner == TeamId.TEAM_BLUE)
                {
                    SpellEffectCreate(out this.greenDash, out _, "NocturneParanoiaTeamTarget.troy", default, TeamId.TEAM_PURPLE, 0, 0, TeamId.TEAM_BLUE, default, default, false, target, default, default, target, default, default, false, default, default, false);
                }
                else
                {
                    SpellEffectCreate(out this.greenDash, out _, "NocturneParanoiaTeamTarget.troy", default, TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_PURPLE, default, default, false, target, default, default, target, default, default, false, default, default, false);
                }
                nextBuffVars_GreenDash = this.greenDash;
                AddBuff((ObjAIBase)target, owner, new Buffs.NocturneParanoiaDash(nextBuffVars_dashSpeed, nextBuffVars_TargetPos, nextBuffVars_GreenDash), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false, true);
                SpellBuffRemove(attacker, nameof(Buffs.NocturneParanoia), attacker);
            }
        }
    }
}