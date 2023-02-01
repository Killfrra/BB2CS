#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SwainTorment : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SwainTormentDoT",
            BuffTextureName = "SwainTorment.dds",
        };
        float doTDamage;
        Particle swainTormentEffect;
        Particle swainDoTEffect;
        Particle swainDoTEffect2;
        int damageTaken; // UNUSED
        float lastTimeExecuted;
        public SwainTorment(float doTDamage = default)
        {
            this.doTDamage = doTDamage;
        }
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            //RequireVar(this.damageAmpPerc);
            //RequireVar(this.doTDamage);
            //RequireVar(this.swainMultiplier);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.swainTormentEffect, out _, "swain_torment_tar.troy", default, TeamId.TEAM_UNKNOWN, 100, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            SpellEffectCreate(out this.swainDoTEffect, out _, "swain_torment_marker.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            SpellEffectCreate(out this.swainDoTEffect2, out _, "swain_torment_dot.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true);
            this.damageTaken = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.swainTormentEffect);
            SpellEffectRemove(this.swainDoTEffect);
            SpellEffectRemove(this.swainDoTEffect2);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, this.doTDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.2f, 1, false, false, attacker);
            }
        }
    }
}
namespace Spells
{
    public class SwainTorment : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {18.75f, 28.75f, 38.75f, 48.75f, 58.75f};
        float[] effect1 = {1.08f, 1.11f, 1.14f, 1.17f, 1.2f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_DoTDamage;
            float nextBuffVars_SwainMultiplier;
            nextBuffVars_DoTDamage = this.effect0[level];
            nextBuffVars_SwainMultiplier = this.effect1[level];
            AddBuff(attacker, target, new Buffs.SwainTorment(nextBuffVars_DoTDamage), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false);
        }
    }
}