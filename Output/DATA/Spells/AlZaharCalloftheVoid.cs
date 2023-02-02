#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class AlZaharCalloftheVoid : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            Vector3 nextBuffVars_TargetPos;
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            TeamId teamID;
            bool foundFirstPos;
            Vector3 firstPos;
            Vector3 lastPos;
            Minion other1;
            float lineWidth; // UNITIALIZED
            Minion other2;
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.IfHasBuffCheck)) == 0)
            {
                AddBuff(attacker, attacker, new Buffs.AlZaharVoidlingCount(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            targetPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            teamID = GetTeamID(owner);
            foundFirstPos = false;
            foreach(Vector3 pos in GetPointsOnLine(ownerPos, targetPos, 750, distance, 15))
            {
                if(!foundFirstPos)
                {
                    firstPos = pos;
                    foundFirstPos = true;
                }
                lastPos = pos;
            }
            other1 = SpawnMinion("Portal to the Void", "TestCubeRender", "idle.lua", firstPos, teamID ?? TeamId.TEAM_CASTER, false, true, false, true, false, true, 300, false, false, (Champion)owner);
            other2 = SpawnMinion("Portal to the Void", "TestCubeRender", "idle.lua", lastPos, teamID ?? TeamId.TEAM_CASTER, false, true, false, true, false, true, 300 + lineWidth, false, false, (Champion)owner);
            FaceDirection(other1, targetPos);
            FaceDirection(other2, targetPos);
            nextBuffVars_TargetPos = targetPos;
            AddBuff((ObjAIBase)owner, other1, new Buffs.AlZaharCalloftheVoid(nextBuffVars_TargetPos), 1, 1, 0.4f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, other2, new Buffs.AlZaharCalloftheVoid(nextBuffVars_TargetPos), 1, 1, 0.4f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class AlZaharCalloftheVoid : BBBuffScript
    {
        Vector3 targetPos;
        Particle particle3;
        Particle particle2;
        public AlZaharCalloftheVoid(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            Particle particle; // UNUSED
            //RequireVar(this.targetPos);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetForceRenderParticles(attacker, true);
            teamOfOwner = GetTeamID(attacker);
            SpellEffectCreate(out particle, out _, "voidflash.troy", default, TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
            SpellEffectCreate(out this.particle3, out this.particle2, "voidportal_green.troy", "voidportal_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, true, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 targetPos;
            int level;
            Particle nextBuffVars_Particle2;
            Particle nextBuffVars_Particle3;
            targetPos = this.targetPos;
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SpellCast(attacker, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, true, owner.Position);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, (ObjAIBase)owner);
            nextBuffVars_Particle2 = this.particle2;
            nextBuffVars_Particle3 = this.particle3;
            AddBuff(attacker, attacker, new Buffs.AlZaharCallR(nextBuffVars_Particle2, nextBuffVars_Particle3), 2, 1, 0.75f, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            SpellEffectRemove(this.particle2);
            SpellEffectRemove(this.particle3);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
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