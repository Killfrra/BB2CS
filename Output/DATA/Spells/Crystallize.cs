#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Crystallize : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int meltingTime; // UNUSED
        int[] effect0 = {4, 5, 6, 7, 8};
        int[] effect1 = {400, 500, 600, 700, 800};
        int[] effect2 = {200, 250, 300, 350, 400};
        public override void SelfExecute()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Crystallize)) > 0)
            {
            }
            else
            {
                Vector3 targetPos;
                Vector3 ownerPos;
                float distance;
                TeamId teamID;
                int iter;
                float lineWidth;
                int halfLength; // UNUSED
                bool foundFirstPos; // UNUSED
                Vector3 facingPoint;
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                this.meltingTime = 5;
                targetPos = GetCastSpellTargetPos();
                ownerPos = GetUnitPosition(owner);
                distance = DistanceBetweenPoints(ownerPos, targetPos);
                teamID = GetTeamID(owner);
                iter = this.effect0[level];
                lineWidth = this.effect1[level];
                halfLength = this.effect2[level];
                foundFirstPos = false;
                facingPoint = GetPointByUnitFacingOffset(owner, 9999, 0);
                foreach(Vector3 pos in GetPointsOnLine(ownerPos, targetPos, lineWidth, distance, iter))
                {
                    Minion other2;
                    other2 = SpawnMinion("IceBlock", "AniviaIceblock", "idle.lua", pos, teamID ?? TeamId.TEAM_UNKNOWN, true, true, true, true, false, true, 0, false, false);
                    FaceDirection(other2, facingPoint);
                    AddBuff((ObjAIBase)owner, other2, new Buffs.Crystallize(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SetGhostProof(other2, true);
                }
            }
        }
    }
}
namespace Buffs
{
    public class Crystallize : BBBuffScript
    {
        public override void OnActivate()
        {
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 100, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                bool ghosted;
                float pushDistance;
                Vector3 targetPos;
                Vector3 nextBuffVars_TargetPos;
                ghosted = GetGhosted(unit);
                if(unit is Champion)
                {
                    pushDistance = 120;
                }
                else
                {
                    pushDistance = 250;
                }
                if(IsBehind(owner, unit))
                {
                    targetPos = GetPointByUnitFacingOffset(owner, pushDistance, 180);
                }
                else
                {
                    targetPos = GetPointByUnitFacingOffset(owner, pushDistance, 0);
                }
                nextBuffVars_TargetPos = targetPos;
                if(attacker.Team != unit.Team)
                {
                    ApplyDamage(attacker, unit, 0, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 0, 0, 1, false, false, attacker);
                }
                if(!ghosted)
                {
                    AddBuff(attacker, unit, new Buffs.GlobalWallPush(nextBuffVars_TargetPos), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, target, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
        }
    }
}