#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class H28GEvolutionTurret : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {1, 1, 2, 2, 2};
        int[] effect1 = {0, 0, 0, 125, 125};
        int[] effect2 = {30, 38, 46, 54, 62};
        int[] effect3 = {0, 0, 0, 0, 0};
        public override bool CanCast()
        {
            bool returnValue = true;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.HeimerdingerTurretReady)) > 0)
            {
                returnValue = true;
            }
            else
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            int maxStacks;
            float level4BonusHP;
            float numFound;
            float minDuration;
            AttackableUnit other2;
            TeamId teamID;
            Vector3 targetPos;
            float abilityPower;
            float abilityPowerBonus;
            float baseDamage;
            float bonusDamage;
            float nextBuffVars_BonusDamage;
            int ownerLevel;
            Minion other3;
            float nextBuffVars_BonusHealthPreLevel4;
            float nextBuffVars_BonusHealth;
            float nextBuffVars_BonusStats;
            float nextBuffVars_BonusArmor;
            SpellBuffRemove(owner, nameof(Buffs.HeimerdingerTurretReady), (ObjAIBase)owner, 0);
            maxStacks = this.effect0[level];
            level4BonusHP = this.effect1[level];
            numFound = 0;
            minDuration = 25000;
            other2 = SetUnit(owner);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions, nameof(Buffs.H28GEvolutionTurret), true))
            {
                float durationRemaining;
                numFound++;
                durationRemaining = GetBuffRemainingDuration(unit, nameof(Buffs.H28GEvolutionTurret));
                if(durationRemaining < minDuration)
                {
                    minDuration = durationRemaining;
                    InvalidateUnit(other2);
                    other2 = SetUnit(unit);
                }
            }
            if(numFound >= maxStacks)
            {
                if(owner != other2)
                {
                    ApplyDamage((ObjAIBase)other2, other2, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, (ObjAIBase)other2);
                }
            }
            teamID = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            abilityPower = GetFlatMagicDamageMod(owner);
            abilityPowerBonus = abilityPower * 0.2f;
            baseDamage = this.effect2[level];
            bonusDamage = baseDamage + abilityPowerBonus;
            nextBuffVars_BonusDamage = bonusDamage;
            ownerLevel = GetLevel(owner);
            nextBuffVars_BonusHealthPreLevel4 = ownerLevel * 15;
            nextBuffVars_BonusHealth = nextBuffVars_BonusHealthPreLevel4 + level4BonusHP;
            nextBuffVars_BonusStats = ownerLevel * 1;
            nextBuffVars_BonusArmor = this.effect3[level];
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UpgradeBuff)) > 0)
            {
                float remainingDuration;
                other3 = SpawnMinion("H-28G Evolution Turret", "HeimerTBlue", "Minion.lua", targetPos, teamID ?? TeamId.TEAM_UNKNOWN, false, false, true, false, false, false, 0, false, false, (Champion)owner);
                remainingDuration = GetBuffRemainingDuration(owner, nameof(Buffs.UpgradeBuff));
                AddBuff(attacker, other3, new Buffs.UpgradeSlow(), 1, 1, remainingDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            if(level == 5)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UpgradeBuff)) == 0)
                {
                    other3 = SpawnMinion("H-28G Evolution Turret", "HeimerTRed", "Minion.lua", targetPos, teamID ?? TeamId.TEAM_UNKNOWN, false, false, true, false, false, false, 0, false, false, (Champion)owner);
                }
                AddBuff((ObjAIBase)owner, other3, new Buffs.ExplosiveCartridges(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            else if(level >= 2)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UpgradeBuff)) == 0)
                {
                    other3 = SpawnMinion("H-28G Evolution Turret", "HeimerTGreen", "Minion.lua", targetPos, teamID ?? TeamId.TEAM_UNKNOWN, false, false, true, false, false, false, 0, false, false, (Champion)owner);
                }
                AddBuff((ObjAIBase)owner, other3, new Buffs.UrAniumRounds(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            else
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UpgradeBuff)) == 0)
                {
                    other3 = SpawnMinion("H-28G Evolution Turret", "HeimerTYellow", "Minion.lua", targetPos, teamID ?? TeamId.TEAM_UNKNOWN, false, false, true, false, false, false, 0, false, false, (Champion)owner);
                }
            }
            AddBuff((ObjAIBase)owner, other3, new Buffs.UPGRADE___Proof(), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, other3, new Buffs.H28GEvolutionTurret(nextBuffVars_BonusDamage, nextBuffVars_BonusHealth, nextBuffVars_BonusArmor, nextBuffVars_BonusStats), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class H28GEvolutionTurret : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "H28GEvolutionTurret",
            BuffTextureName = "Heimerdinger_H28GEvolutionTurret.dds",
        };
        float bonusDamage;
        float bonusHealth;
        float bonusArmor;
        float bonusStats;
        float lastTimeExecuted;
        public H28GEvolutionTurret(float bonusDamage = default, float bonusHealth = default, float bonusArmor = default, float bonusStats = default)
        {
            this.bonusDamage = bonusDamage;
            this.bonusHealth = bonusHealth;
            this.bonusArmor = bonusArmor;
            this.bonusStats = bonusStats;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(scriptName == nameof(Buffs.GlobalWallPush))
                {
                    returnValue = false;
                }
                else if(type == BuffType.STUN)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            else
            {
                if(maxStack == 76)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Particle poofin; // UNUSED
            CancelAutoAttack(owner, true);
            teamID = GetTeamID(attacker);
            //RequireVar(this.bonusDamage);
            //RequireVar(this.bonusHealth);
            //RequireVar(this.bonusArmor);
            //RequireVar(this.bonusStats);
            SetCanMove(owner, false);
            SpellEffectCreate(out poofin, out _, "heimerdinger_turret_birth.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            foreach(AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, 1, default, true))
            {
                if(unit is Champion)
                {
                    AddBuff((ObjAIBase)unit, owner, new Buffs.H28GEvolutionTurretSpell2(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else
                {
                    AddBuff((ObjAIBase)unit, owner, new Buffs.H28GEvolutionTurretSpell3(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if(GetBuffCountFromCaster(attacker, owner, nameof(Buffs.HeimerdingerTurretMaximum)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.HeimerdingerTurretMaximum), (ObjAIBase)owner, 0);
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, this.bonusHealth);
            IncFlatPhysicalDamageMod(owner, this.bonusDamage);
            SetCanMove(owner, false);
            IncFlatArmorMod(owner, this.bonusStats);
            IncFlatSpellBlockMod(owner, this.bonusStats);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                if(GetBuffCountFromCaster(owner, default, nameof(Buffs.H28GEvolutionTurretSpell1)) == 0)
                {
                    if(GetBuffCountFromCaster(owner, default, nameof(Buffs.H28GEvolutionTurretSpell2)) == 0)
                    {
                        if(GetBuffCountFromCaster(owner, default, nameof(Buffs.H28GEvolutionTurretSpell3)) == 0)
                        {
                            int unitFound;
                            unitFound = 0;
                            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.UpgradeSlow)) > 0)
                            {
                                foreach(AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                                {
                                    unitFound = 1;
                                    AddBuff((ObjAIBase)unit, owner, new Buffs.H28GEvolutionTurretSpell2(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                }
                            }
                            if(unitFound == 0)
                            {
                                foreach(AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, 1, default, true))
                                {
                                    if(unit is Champion)
                                    {
                                        AddBuff((ObjAIBase)unit, owner, new Buffs.H28GEvolutionTurretSpell2(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                    }
                                    else
                                    {
                                        AddBuff((ObjAIBase)unit, owner, new Buffs.H28GEvolutionTurretSpell3(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if(GetBuffCountFromCaster(owner, default, nameof(Buffs.UpgradeSlow)) > 0)
                            {
                                foreach(AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position, 425, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                                {
                                    SpellBuffClear(owner, nameof(Buffs.H28GEvolutionTurretSpell3));
                                    AddBuff((ObjAIBase)unit, owner, new Buffs.H28GEvolutionTurretSpell2(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void OnSpellHit()
        {
            this.bonusArmor += 0.143f;
        }
    }
}