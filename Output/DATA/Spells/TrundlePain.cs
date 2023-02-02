#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class TrundlePain : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = false,
        };
        int[] effect0 = {100, 175, 250};
        float[] effect1 = {0.15f, 0.2f, 0.25f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_DamageDealt;
            float nextBuffVars_Survivability;
            BreakSpellShields(target);
            nextBuffVars_DamageDealt = this.effect0[level];
            nextBuffVars_Survivability = this.effect1[level];
            AddBuff(attacker, target, new Buffs.TrundlePain(nextBuffVars_DamageDealt), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.TrundlePainShred(nextBuffVars_Survivability), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class TrundlePain : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "TrundlePain",
            BuffTextureName = "Trundle_Agony.dds",
        };
        float damageDealt;
        float moddedDamage;
        float damageSecond;
        Particle asdf;
        float lastTimeExecuted;
        public TrundlePain(float damageDealt = default)
        {
            this.damageDealt = damageDealt;
        }
        public override void OnActivate()
        {
            float aPStat;
            float aPRatio;
            //RequireVar(this.damageDealt);
            aPStat = GetFlatMagicDamageMod(attacker);
            aPRatio = aPStat * 0.6f;
            this.moddedDamage = aPRatio + this.damageDealt;
            this.damageSecond = this.moddedDamage / 6;
            ApplyDamage(attacker, owner, this.moddedDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
            IncHealth(attacker, this.moddedDamage, attacker);
            SpellEffectCreate(out this.asdf, out _, "TrundleUltParticle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.asdf);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, this.damageSecond, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 1, false, false, attacker);
                IncHealth(attacker, this.damageSecond, attacker);
            }
        }
    }
}