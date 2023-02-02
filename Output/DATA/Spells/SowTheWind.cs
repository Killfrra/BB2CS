#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SowTheWind : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        float[] effect0 = {-0.24f, -0.3f, -0.36f, -0.42f, -0.48f};
        int[] effect1 = {60, 115, 170, 225, 280};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float nextBuffVars_AttackSpeedMod;
            Particle asdf; // UNUSED
            float nextBuffVars_MoveSpeedMod;
            teamID = GetTeamID(attacker);
            BreakSpellShields(target);
            nextBuffVars_AttackSpeedMod = 0;
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            ApplyDamage(attacker, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
            SpellEffectCreate(out asdf, out _, "SowTheWind_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class SowTheWind : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Zephyr",
            BuffTextureName = "Janna_Zephyr.dds",
        };
        Particle particle;
        int sowCast;
        float[] effect0 = {0.08f, 0.1f, 0.12f, 0.14f, 0.16f};
        public override void OnActivate()
        {
            TeamId teamID;
            Particle part; // UNUSED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out part, out _, "SowTheWind_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            SpellEffectCreate(out this.particle, out _, "SowTheWind_buf.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, "head", default, false);
            SetGhosted(owner, true);
            this.sowCast = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SetGhosted(owner, false);
            SpellEffectRemove(this.particle);
        }
        public override void OnUpdateStats()
        {
            int level;
            float movementSpeedMod;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            movementSpeedMod = this.effect0[level];
            IncPercentMovementSpeedMod(owner, movementSpeedMod);
            SetGhosted(owner, true);
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            spellName = GetSpellName();
            if(spellName == nameof(Spells.SowTheWind))
            {
                this.sowCast = 1;
            }
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            if(this.sowCast == 1)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}