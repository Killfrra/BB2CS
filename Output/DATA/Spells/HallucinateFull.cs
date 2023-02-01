#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HallucinateFull : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Hallucinate",
            BuffTextureName = "Jester_HallucinogenBomb.dds",
        };
        float damageAmount;
        float damageDealt;
        float damageTaken;
        Particle particle;
        public HallucinateFull(float damageAmount = default, float damageDealt = default, float damageTaken = default)
        {
            this.damageAmount = damageAmount;
            this.damageDealt = damageDealt;
            this.damageTaken = damageTaken;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particle, out _, "Jester_Copy.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_BLUE, default, default, true, owner, "root", default, target, "root", default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "Jester_Copy.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_PURPLE, default, default, true, owner, "root", default, target, "root", default, false, false, false, false, false);
            }
            //RequireVar(this.damageAmount);
            //RequireVar(this.damageDealt);
            //RequireVar(this.damageTaken);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            Particle hi; // UNUSED
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out hi, out _, "Hallucinate_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, owner, default, default, true, false, false, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage(attacker, unit, this.damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
            }
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
            SpellEffectRemove(this.particle);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            TeamId teamID;
            Champion caster;
            float totalDamage;
            teamID = GetTeamID(owner);
            caster = GetChampionBySkinName("Shaco", teamID);
            totalDamage = damageAmount * this.damageDealt;
            if(target is BaseTurret)
            {
                totalDamage *= 0.5f;
            }
            damageAmount = 0;
            ApplyDamage(caster, target, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 1, 0, false, false, caster);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            damageAmount *= this.damageTaken;
        }
    }
}
namespace Spells
{
    public class HallucinateFull : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {300, 450, 600};
        float[] effect1 = {0.75f, 0.75f, 0.75f};
        float[] effect2 = {1.5f, 1.5f, 1.5f};
        float[] effect3 = {0.85f, 0.85f, 0.85f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nextBuffVars_DamageAmount;
            float nextBuffVars_DamageDealt;
            float nextBuffVars_DamageTaken;
            float nextBuffVars_shacoDamageTaken;
            DestroyMissileForTarget(owner);
            nextBuffVars_DamageAmount = this.effect0[level];
            nextBuffVars_DamageDealt = this.effect1[level];
            nextBuffVars_DamageTaken = this.effect2[level];
            nextBuffVars_shacoDamageTaken = this.effect3[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.HallucinateApplicator(nextBuffVars_DamageAmount, nextBuffVars_DamageDealt, nextBuffVars_DamageTaken, nextBuffVars_shacoDamageTaken), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}