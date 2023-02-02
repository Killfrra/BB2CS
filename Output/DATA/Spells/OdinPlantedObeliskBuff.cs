#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinPlantedObeliskBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "pelvis", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "Ferocious Howl",
            BuffTextureName = "Minotaur_FerociousHowl.dds",
        };
        float nextAttackTime;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            this.nextAttackTime = GetGameTime();
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                float currentTime;
                bool foundTarget;
                int count;
                currentTime = GetGameTime();
                foundTarget = false;
                count = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppression));
                if(count >= 1)
                {
                    this.nextAttackTime = currentTime + 1;
                }
                if(this.nextAttackTime <= currentTime)
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, nameof(Buffs.OdinGuardianBuff), true))
                    {
                        if(!foundTarget)
                        {
                            if(count <= 0)
                            {
                                int _1; // UNITIALIZED
                                SpellCast((ObjAIBase)owner, unit, default, default, 0, SpellSlotType.SpellSlots, 1 + _1, true, false, false, false, false, false);
                                this.nextAttackTime = currentTime + 1.25f;
                                foundTarget = true;
                            }
                        }
                    }
                }
            }
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            TeamId myTeamID;
            if(attacker is not Champion)
            {
                float myMaxHealth;
                float healthToDecreaseBy;
                myMaxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                myTeamID = GetTeamID(owner);
                if(myTeamID == TeamId.TEAM_NEUTRAL)
                {
                    healthToDecreaseBy = 0.015f * myMaxHealth;
                }
                else
                {
                    healthToDecreaseBy = 0.02f * myMaxHealth;
                }
                ApplyDamage(attacker, owner, healthToDecreaseBy, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, (ObjAIBase)owner);
            }
            if(attacker is not Champion)
            {
                myTeamID = GetTeamID(owner);
                if(myTeamID == TeamId.TEAM_NEUTRAL)
                {
                    float healthPercent;
                    float attackerMaxHealth;
                    float damageReturn;
                    healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                    if(healthPercent > 0.99f)
                    {
                        ApplyDamage(attacker, owner, 10000000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, attacker);
                    }
                    attackerMaxHealth = GetMaxHealth(attacker, PrimaryAbilityResourceType.MANA);
                    damageReturn = 0.151f * attackerMaxHealth;
                    ApplyDamage((ObjAIBase)owner, attacker, damageReturn, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_RAW, 1, 0, 0, false, false, (ObjAIBase)owner);
                }
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            damageAmount *= 0.75f;
        }
    }
}