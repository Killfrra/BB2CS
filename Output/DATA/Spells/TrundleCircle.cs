#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TrundleCircle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundleCircle",
            BuffTextureName = "XinZhao_CrescentSweep.dds",
        };
        Particle particle;
        Particle particle2;
        float[] effect0 = {-0.25f, -0.3f, -0.35f, -0.4f, -0.45f};
        public override void OnActivate()
        {
            TeamId teamID;
            float offsetAngle;
            Vector3 targetPos;
            Vector3 nextBuffVars_TargetPos;
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out this.particle, out this.particle2, "trundle_PlagueBlock_green.troy", "trundle_PlagueBlock_red.troy", teamID, 10, 0, TeamId.TEAM_BLUE, default, default, false, owner, default, default, owner, default, default, false, false, false, false, false);
            //RequireVar(this.iD);
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhostProof(owner, true);
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 180, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.GlobalWallPush), false))
            {
                ApplyAssistMarker(attacker, unit, 10);
                offsetAngle = GetOffsetAngle(owner, unit.Position);
                targetPos = GetPointByUnitFacingOffset(owner, 200, offsetAngle);
                nextBuffVars_TargetPos = targetPos;
                AddBuff(attacker, unit, new Buffs.GlobalWallPush(nextBuffVars_TargetPos), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if(attacker.Team != unit.Team)
                {
                    ApplyDamage(attacker, unit, 0, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 0, 0, 1, false, false, attacker);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, (ObjAIBase)owner);
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
        public override void OnUpdateActions()
        {
            int level;
            float nextBuffVars_MoveSpeedMod;
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 360, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
        }
    }
}
namespace Spells
{
    public class TrundleCircle : BBSpellScript
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
            Vector3 targetPos;
            TeamId teamID;
            Minion other3;
            int nextBuffVars_ID;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Crystallize)) > 0)
            {
            }
            else
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                targetPos = GetCastSpellTargetPos();
                teamID = GetTeamID(owner);
                other3 = SpawnMinion("PlagueBlock", "TrundleWall", "idle.lua", targetPos, teamID, true, true, true, true, false, true, 0, false, false);
                nextBuffVars_ID = 1;
                AddBuff((ObjAIBase)owner, other3, new Buffs.TrundleCircle(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                FaceDirection(other3, owner.Position);
            }
        }
    }
}