#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptSejuani : BBCharScript
    {
        float lastTimeExecuted;
        float mS; // UNUSED
        int[] effect0 = {12, 20, 28, 36, 44};
        float[] effect1 = {0.01f, 0.0125f, 0.015f, 0.0175f, 0.02f};
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(5, ref this.lastTimeExecuted, true))
            {
                float damagePerTick;
                float maxHPPercent;
                float frostBonus;
                float temp1;
                float percentDamage;
                float abilityPowerMod;
                float abilityPowerBonus;
                float damagePerTickFrost;
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                level = Math.Max(level, 1);
                damagePerTick = this.effect0[level];
                maxHPPercent = this.effect1[level];
                frostBonus = 1.5f;
                temp1 = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                percentDamage = temp1 * maxHPPercent;
                damagePerTick += percentDamage;
                abilityPowerMod = GetFlatMagicDamageMod(owner);
                abilityPowerBonus = abilityPowerMod * 0.1f;
                damagePerTick += abilityPowerBonus;
                damagePerTickFrost = frostBonus * damagePerTick;
                SetSpellToolTipVar(percentDamage, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
                SetSpellToolTipVar(damagePerTickFrost, 2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (Champion)owner);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(target is ObjAIBase)
                    {
                        SpellCast((ObjAIBase)owner, target, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                    }
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(spellVars.DoesntTriggerSpellCasts)
            {
            }
            else
            {
                int slot;
                slot = GetSpellSlot();
                if(slot == 2)
                {
                    TeamId teamID;
                    float duration;
                    teamID = GetTeamID(owner);
                    if(teamID == TeamId.TEAM_BLUE)
                    {
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                        {
                            if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrost)) > 0)
                            {
                                duration = GetBuffRemainingDuration(unit, nameof(Buffs.SejuaniFrost));
                                duration += 0.5f;
                                SpellBuffRenew(unit, nameof(Buffs.SejuaniFrost), duration);
                            }
                            if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrostResist)) > 0)
                            {
                                duration = GetBuffRemainingDuration(unit, nameof(Buffs.SejuaniFrostResist));
                                duration += 0.5f;
                                SpellBuffRenew(unit, nameof(Buffs.SejuaniFrostResist), duration);
                            }
                        }
                    }
                    else
                    {
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                        {
                            if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrostChaos)) > 0)
                            {
                                duration = GetBuffRemainingDuration(unit, nameof(Buffs.SejuaniFrostChaos));
                                duration += 0.5f;
                                SpellBuffRenew(unit, nameof(Buffs.SejuaniFrostChaos), duration);
                            }
                            if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrostResistChaos)) > 0)
                            {
                                duration = GetBuffRemainingDuration(unit, nameof(Buffs.SejuaniFrostResistChaos));
                                duration += 0.5f;
                                SpellBuffRenew(unit, nameof(Buffs.SejuaniFrostResistChaos), duration);
                            }
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.FrostDuration = 3;
            this.mS = GetMovementSpeed(owner);
            AddBuff((ObjAIBase)owner, owner, new Buffs.SejuaniRunSpeed(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            UnlockAnimation(owner, true);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}