#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BlindMonkEOne : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        Particle partname; // UNUSED
        int[] effect0 = {60, 95, 130, 165, 200};
        float[] effect1 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        public override void SelfExecute()
        {
            bool hasHitTarget;
            TeamId casterID;
            float baseDamage;
            float bonusAD;
            float damageToDeal;
            float nextBuffVars_MoveSpeedMod; // UNUSED
            hasHitTarget = false;
            casterID = GetTeamID(owner);
            baseDamage = this.effect0[level];
            bonusAD = GetFlatPhysicalDamageMod(owner);
            damageToDeal = bonusAD + baseDamage;
            nextBuffVars_MoveSpeedMod = this.effect1[level];
            SpellEffectCreate(out this.partname, out _, "blindMonk_thunderCrash_impact_cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", owner.Position, target, default, default, true, default, default, false, false);
            SpellEffectCreate(out this.partname, out _, "blindMonk_thunderCrash_impact_02.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", owner.Position, target, default, default, true, default, default, false, false);
            SpellEffectCreate(out this.partname, out _, "blindMonk_E_cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", owner.Position, target, default, default, true, default, default, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.SharedWardBuff), false))
            {
                Particle aoehit; // UNUSED
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                AddBuff(attacker, unit, new Buffs.BlindMonkEOne(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                SpellEffectCreate(out aoehit, out _, "blindMonk_thunderCrash_impact_unit_tar.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                SpellEffectCreate(out aoehit, out _, "blindMonk_E_thunderCrash_tar.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                SpellEffectCreate(out aoehit, out _, "blindMonk_E_thunderCrash_unit_tar_blood.troy", default, casterID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                hasHitTarget = true;
            }
            if(hasHitTarget)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.BlindMonkEManager(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class BlindMonkEOne : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "global_Watched.troy", },
            BuffName = "BlindMonkTempest",
            BuffTextureName = "BlindMonkEOne.dds",
            SpellFXOverrideSkins = new[]{ "ReefMalphite", },
        };
        Region bubbleID;
        Region bubbleID2;
        public override void OnActivate()
        {
            TeamId team;
            team = GetTeamID(attacker);
            this.bubbleID = AddUnitPerceptionBubble(team, 400, owner, 20, default, default, false);
            this.bubbleID2 = AddUnitPerceptionBubble(team, 50, owner, 20, default, default, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
            RemovePerceptionBubble(this.bubbleID2);
        }
    }
}