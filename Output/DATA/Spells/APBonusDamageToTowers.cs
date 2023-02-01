#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class APBonusDamageToTowers : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "APBonusDamageToTowers",
            BuffTextureName = "Minotaur_ColossalStrength.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnDisconnect()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.DisconnectTimer(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            float cCreduction;
            if(avatarVars.MasteryJuggernaut)
            {
                cCreduction = 0.9f;
                if(owner.Team != attacker.Team)
                {
                    if(type == BuffType.SNARE)
                    {
                        duration *= cCreduction;
                    }
                    if(type == BuffType.SLOW)
                    {
                        duration *= cCreduction;
                    }
                    if(type == BuffType.FEAR)
                    {
                        duration *= cCreduction;
                    }
                    if(type == BuffType.CHARM)
                    {
                        duration *= cCreduction;
                    }
                    if(type == BuffType.SLEEP)
                    {
                        duration *= cCreduction;
                    }
                    if(type == BuffType.STUN)
                    {
                        duration *= cCreduction;
                    }
                    if(type == BuffType.TAUNT)
                    {
                        duration *= cCreduction;
                    }
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            string foritfyCheck;
            string foritfyCheck2;
            foritfyCheck = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(foritfyCheck == nameof(Spells.SummonerFortify))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.FortifyCheck(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 3, true, false, false);
            }
            foritfyCheck2 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(foritfyCheck2 == nameof(Spells.SummonerFortify))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.FortifyCheck(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 3, true, false, false);
            }
        }
        public override void OnUpdateStats()
        {
            float healthPERC;
            healthPERC = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if(avatarVars.MasteryInitiate)
            {
                if(healthPERC > 0.7f)
                {
                    IncPercentMovementSpeedMod(owner, avatarVars.MasteryInitiateAmt);
                }
            }
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(2, ref this.lastTimeExecuted, false))
            {
                if(avatarVars.MasterySeigeCommander)
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectTurrets, default, true))
                    {
                        AddBuff(attacker, unit, new Buffs.MasterySiegeCommanderDebuff(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnKill()
        {
            float masteryBountyAmt;
            if(avatarVars.MasteryScholar)
            {
                if(target is Champion)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0)
                    {
                        IncExp(owner, 20);
                    }
                    else
                    {
                        IncExp(owner, 40);
                    }
                }
            }
            if(avatarVars.MasteryBounty)
            {
                if(target is Champion)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0)
                    {
                        masteryBountyAmt = avatarVars.MasteryBountyAmt / 2;
                        IncGold(owner, masteryBountyAmt);
                    }
                    else
                    {
                        IncGold(owner, avatarVars.MasteryBountyAmt);
                    }
                }
            }
        }
        public override void OnAssist()
        {
            float masteryBountyAmt;
            if(avatarVars.MasteryScholar)
            {
                if(target is Champion)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0)
                    {
                        IncExp(owner, 20);
                    }
                    else
                    {
                        IncExp(owner, 40);
                    }
                }
            }
            if(avatarVars.MasteryBounty)
            {
                if(target is Champion)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0)
                    {
                        masteryBountyAmt = avatarVars.MasteryBountyAmt / 2;
                        IncGold(owner, masteryBountyAmt);
                    }
                    else
                    {
                        IncGold(owner, avatarVars.MasteryBountyAmt);
                    }
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float abilityPower;
            float abilityDamageToAdd;
            float bonusAttackPower;
            if(target is BaseTurret)
            {
                abilityPower = GetFlatMagicDamageMod(owner);
                abilityDamageToAdd = abilityPower / 2.5f;
                bonusAttackPower = GetFlatPhysicalDamageMod(owner);
                if(bonusAttackPower <= abilityDamageToAdd)
                {
                    damageAmount -= bonusAttackPower;
                    damageAmount += abilityDamageToAdd;
                }
                if(avatarVars.MasteryDemolitionist)
                {
                    damageAmount += avatarVars.MasteryDemolitionistAmt;
                }
            }
            else
            {
                if(target is not ObjAIBase)
                {
                    abilityPower = GetFlatMagicDamageMod(owner);
                    abilityDamageToAdd = abilityPower / 2.5f;
                    bonusAttackPower = GetFlatPhysicalDamageMod(owner);
                    if(bonusAttackPower <= abilityDamageToAdd)
                    {
                        damageAmount -= bonusAttackPower;
                        damageAmount += abilityDamageToAdd;
                    }
                    if(avatarVars.MasteryDemolitionist)
                    {
                        damageAmount += avatarVars.MasteryDemolitionistAmt;
                    }
                }
            }
            if(avatarVars.MasteryButcher)
            {
                if(target is ObjAIBase)
                {
                    if(target is BaseTurret)
                    {
                    }
                    else
                    {
                        if(target is Champion)
                        {
                        }
                        else
                        {
                            damageAmount += avatarVars.MasteryButcherAmt;
                        }
                    }
                }
            }
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(avatarVars.MasteryBladedArmor)
            {
                if(attacker is ObjAIBase)
                {
                    if(attacker is BaseTurret)
                    {
                    }
                    else
                    {
                        if(attacker is Champion)
                        {
                        }
                        else
                        {
                            ApplyDamage((ObjAIBase)owner, attacker, avatarVars.MasteryBladedArmorAmt, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_REACTIVE, 1, 0, 0, false, false, (ObjAIBase)owner);
                        }
                    }
                }
            }
        }
    }
}