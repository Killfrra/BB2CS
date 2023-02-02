#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VolibearE : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        Particle partname; // UNUSED
        float[] effect0 = {-0.3f, -0.35f, -0.4f, -0.45f, -0.5f};
        int[] effect1 = {2, 2, 2, 2, 2};
        int[] effect2 = {60, 105, 150, 195, 240};
        int[] effect3 = {3, 3, 3, 3, 3};
        public override void SelfExecute()
        {
            TeamId teamID;
            float nextBuffVars_VolibearESlow;
            float damageToDeal;
            int nextBuffVars_VolibearEExtender; // UNUSED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.partname, out _, "volibear_E_aoe_indicator.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 350, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.partname, out _, "volibear_E_aoe_indicator_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 350, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "Volibear_E_cas_blast.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, default, default, default, true, false, false, false, false);
            SpellEffectCreate(out _, out _, "Volibear_E_cas.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, default, default, default, true, false, false, false, false);
            nextBuffVars_VolibearESlow = this.effect0[level];
            nextBuffVars_VolibearEExtender = this.effect1[level];
            damageToDeal = this.effect2[level];
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, attacker.Position, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                Particle targetParticle; // UNUSED
                SpellEffectCreate(out targetParticle, out _, "volibear_E_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.VolibearE(nextBuffVars_VolibearESlow), 1, 1, this.effect3[level], BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 0, false, false, attacker);
                if(unit is not Champion)
                {
                    if(!unit.IsDead)
                    {
                        ApplyFear(owner, unit, 2);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class VolibearE : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.TROY", "", },
            BuffName = "VolibearE",
            BuffTextureName = "VolibearE.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float volibearESlow;
        public VolibearE(float volibearESlow = default)
        {
            this.volibearESlow = volibearESlow;
        }
        public override void OnActivate()
        {
            //RequireVar(this.volibearEExtender);
            //RequireVar(this.volibearESlow);
            IncPercentMovementSpeedMod(owner, this.volibearESlow);
            if(target is not Champion)
            {
                IncPercentMovementSpeedMod(owner, -0.5f);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.volibearESlow);
            if(target is not Champion)
            {
                IncPercentMovementSpeedMod(owner, -0.5f);
            }
        }
    }
}