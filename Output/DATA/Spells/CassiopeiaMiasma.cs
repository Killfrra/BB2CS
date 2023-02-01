#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CassiopeiaMiasma : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "AcidTrail_buf.troy", },
        };
        float damagePerTick;
        float moveSpeedMod;
        float areaRadius;
        Particle particle2;
        Particle particle;
        Region bubbleID;
        public CassiopeiaMiasma(float damagePerTick = default, float moveSpeedMod = default)
        {
            this.damagePerTick = damagePerTick;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            float nextBuffVars_DamagePerTick;
            float nextBuffVars_MoveSpeedMod;
            //RequireVar(this.damagePerTick);
            //RequireVar(this.moveSpeedMod);
            IncPercentBubbleRadiusMod(owner, -0.6f);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetNoRender(owner, true);
            this.areaRadius = 185;
            teamOfOwner = GetTeamID(attacker);
            SpellEffectCreate(out this.particle2, out this.particle, "CassMiasma_tar_green.troy", "CassMiasma_tar_red.troy", teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            this.bubbleID = AddUnitPerceptionBubble(teamOfOwner, 250, owner, 7, default, default, false);
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, this.areaRadius, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                nextBuffVars_DamagePerTick = this.damagePerTick;
                AddBuff(attacker, unit, new Buffs.CassiopeiaMiasmaPoison(nextBuffVars_DamagePerTick), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.POISON, 1, true, false, false);
                nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, attacker);
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            RemovePerceptionBubble(this.bubbleID);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.6f);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_DamagePerTick;
            float nextBuffVars_MoveSpeedMod;
            this.areaRadius += 4;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, this.areaRadius, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                nextBuffVars_DamagePerTick = this.damagePerTick;
                AddBuff(attacker, unit, new Buffs.CassiopeiaMiasmaPoison(nextBuffVars_DamagePerTick), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.POISON, 1, true, false, false);
                nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
            }
        }
    }
}
namespace Spells
{
    public class CassiopeiaMiasma : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {25, 35, 45, 55, 65};
        float[] effect1 = {-0.15f, -0.2f, -0.25f, -0.3f, -0.35f};
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            TeamId teamID;
            Minion other3;
            int nextBuffVars_DamagePerTick;
            float nextBuffVars_MoveSpeedMod;
            teamID = GetTeamID(owner);
            other3 = SpawnMinion("Test", "TestCubeRender", "idle.lua", missileEndPosition, teamID, false, true, false, true, true, true, 0, false, true);
            SetGhosted(other3, true);
            level = GetCastSpellLevelPlusOne();
            nextBuffVars_DamagePerTick = this.effect0[level];
            nextBuffVars_MoveSpeedMod = this.effect1[level];
            AddBuff(attacker, other3, new Buffs.CassiopeiaMiasma(nextBuffVars_DamagePerTick, nextBuffVars_MoveSpeedMod), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.ExpirationTimer(), 1, 1, 9, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}