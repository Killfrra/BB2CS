#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class InfernalGuardianBurning : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Infernal Guardian",
            BuffTextureName = "Annie_GuardianIncinerate.dds",
        };
        float armorAmount;
        float mRAmount;
        float healthAmount;
        float finalDamage;
        Particle a;
        float lastTimeExecuted;
        public InfernalGuardianBurning(float armorAmount = default, float mRAmount = default, float healthAmount = default, float finalDamage = default)
        {
            this.armorAmount = armorAmount;
            this.mRAmount = mRAmount;
            this.healthAmount = healthAmount;
            this.finalDamage = finalDamage;
        }
        public override void OnActivate()
        {
            int annieSkinID;
            //RequireVar(this.finalDamage);
            //RequireVar(this.healthAmount);
            //RequireVar(this.armorAmount);
            //RequireVar(this.mRAmount);
            annieSkinID = GetSkinID(owner);
            if(annieSkinID == 5)
            {
                SpellEffectCreate(out this.a, out _, "SunfireCape_Aura_frost.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.a, out _, "SunfireCapeAura_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 0, false, false, attacker);
            SpellEffectRemove(this.a);
            SpellBuffRemove(attacker, nameof(Buffs.InfernalGuardianTimer), attacker, 0);
        }
        public override void OnUpdateStats()
        {
            IncMaxHealth(owner, this.healthAmount, true);
            IncFlatArmorMod(owner, this.armorAmount);
            IncFlatSpellBlockMod(owner, this.mRAmount);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, this.finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                }
            }
            if(attacker.IsDead)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}