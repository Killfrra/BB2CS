#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinBombDetonator : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "pelvis", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "Ferocious Howl",
            BuffTextureName = "Minotaur_FerociousHowl.dds",
        };
        float timePassed;
        float previousGameTime;
        float lastTimeExecuted;
        float lastTimeExecuted2;
        public override void OnActivate()
        {
            this.timePassed = 0;
            this.previousGameTime = GetGameTime();
        }
        public override void OnUpdateActions()
        {
            int count;
            float currentGameTime;
            float currentTimePassed;
            float timeRemaining;
            float toPrint;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                count = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppression));
                currentGameTime = GetGameTime();
                if(count <= 0)
                {
                    currentTimePassed = currentGameTime - this.previousGameTime;
                    this.timePassed += currentTimePassed;
                }
                this.previousGameTime = currentGameTime;
            }
            if(ExecutePeriodically(1, ref this.lastTimeExecuted2, true))
            {
                timeRemaining = 10 - this.timePassed;
                toPrint = MathF.Floor(timeRemaining);
                if(timeRemaining <= 0)
                {
                    Say(owner, "KaBoom");
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, nameof(Buffs.OdinGuardianBuff), true))
                    {
                        AddBuff(attacker, unit, new Buffs.OdinBombDetonation(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    AddBuff(attacker, owner, new Buffs.OdinBombDetonation(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SpellEffectCreate(out _, out _, "CrashBoom.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, default, default, false);
                }
                else
                {
                    count = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppression));
                    if(count > 0)
                    {
                        Say(owner, "Defusing - ", toPrint);
                    }
                    else
                    {
                        Say(owner, "  ", toPrint);
                    }
                }
            }
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float myMaxHealth;
            TeamId myTeamID;
            float healthToDecreaseBy;
            float healthPercent;
            float attackerMaxHealth;
            float damageReturn;
            if(attacker is not Champion)
            {
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