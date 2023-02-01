#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShyvanaDoubleAttackDragon : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "R_Hand", "L_Hand", },
            AutoBuffActivateEffect = new[]{ "shyvana_doubleAttack_buf.troy", "shyvana_doubleAttack_buf.troy", },
            BuffName = "ShyvanaDoubleAttackDragon",
            BuffTextureName = "ShyvanaTwinBite.dds",
            IsDeathRecapSource = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float spellCooldown;
        float[] effect0 = {0.8f, 0.85f, 0.9f, 0.95f, 1};
        int[] effect1 = {15, 25, 35, 45, 55};
        int[] effect2 = {15, 25, 35, 45, 55};
        public ShyvanaDoubleAttackDragon(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.spellCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            OverrideAutoAttack(3, SpellSlotType.ExtraSlots, owner, 1, true);
            SetDodgePiercing(owner, true);
            CancelAutoAttack(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            float spellCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            spellCooldown = this.spellCooldown;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            RemoveOverrideAutoAttack(owner, true);
            SetDodgePiercing(owner, false);
        }
        public override void OnUpdateStats()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            TeamId teamID;
            float baseAttackDamage;
            int level;
            float distance;
            float procDamage;
            Particle a; // UNUSED
            teamID = GetTeamID(owner);
            baseAttackDamage = GetBaseAttackDamage(owner);
            SpellBuffRemove(owner, nameof(Buffs.ShyvanaDoubleAttackDragon), (ObjAIBase)owner, 0);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                distance = DistanceBetweenObjects("Attacker", "Unit");
                if(target == unit)
                {
                }
                else
                {
                    if(distance < 250)
                    {
                        if(IsInFront(owner, unit))
                        {
                            BreakSpellShields(unit);
                            ApplyDamage(attacker, unit, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
                            ApplyDamage(attacker, unit, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, this.effect0[level], 0, 1, false, false, attacker);
                            if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.ShyvanaFireballMissile)) > 0)
                            {
                                level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                                procDamage = this.effect1[level];
                                ApplyDamage(attacker, unit, procDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.1f, 0, false, false, attacker);
                                teamID = GetTeamID(attacker);
                                SpellEffectCreate(out a, out _, "shyvana_flameBreath_reignite.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
                            }
                            if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.ShyvanaFireballMissileMinion)) > 0)
                            {
                                level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                                procDamage = this.effect2[level];
                                ApplyDamage(attacker, unit, procDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.1f, 0, false, false, attacker);
                                teamID = GetTeamID(attacker);
                                SpellEffectCreate(out a, out _, "shyvana_flameBreath_reignite.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
                            }
                            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            if(level == 1)
                            {
                                IncPAR(owner, 2, PrimaryAbilityResourceType.Other);
                            }
                            else if(level == 2)
                            {
                                IncPAR(owner, 3, PrimaryAbilityResourceType.Other);
                            }
                            else if(level == 3)
                            {
                                IncPAR(owner, 4, PrimaryAbilityResourceType.Other);
                            }
                            if(target is ObjAIBase)
                            {
                                SpellEffectCreate(out a, out _, "shyvana_doubleAttack_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class ShyvanaDoubleAttackDragon : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
        };
        int[] effect0 = {10, 9, 8, 7, 6};
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown;
            nextBuffVars_SpellCooldown = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaDoubleAttackDragon(nextBuffVars_SpellCooldown), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
        }
    }
}