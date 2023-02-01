#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SwainBeamDamage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SwainBeamDamage",
            BuffTextureName = "SwainDecrepify.dds",
        };
        float damagePerHalfSecond;
        int casterID; // UNUSED
        float lastTimeExecuted;
        public SwainBeamDamage(float damagePerHalfSecond = default)
        {
            this.damagePerHalfSecond = damagePerHalfSecond;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerHalfSecond);
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.SwainMetamorphism)) == 0)
            {
                this.casterID = PushCharacterData("SwainNoBird", attacker, false);
            }
            ApplyDamage(attacker, owner, this.damagePerHalfSecond, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.3f, 1, false, false, attacker);
        }
        public override void OnDeactivate(bool expired)
        {
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.SwainMetamorphism)) == 0)
            {
                PopAllCharacterData(attacker);
            }
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.SwainBeamTransition)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.SwainBeamTransition), attacker, 0);
            }
            SpellBuffRemove(attacker, nameof(Buffs.SwainBeamSelf), attacker, 0);
        }
        public override void OnUpdateActions()
        {
            if(owner.IsDead)
            {
                SpellBuffRemoveCurrent(owner);
            }
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                ApplyDamage(attacker, owner, this.damagePerHalfSecond, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.3f, 1, false, false, attacker);
            }
        }
    }
}