#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BrandWildfireMissile3 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {150, 250, 350, 400, 350};
        int[] effect1 = {0, 0, 0, 0, 0};
        int[] effect2 = {0, 0, 0, 0, 0};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            bool doOnce;
            int count;
            int brandSkinID;
            TeamId teamID; // UNUSED
            Particle ablazeHitEffect; // UNUSED
            AddBuff((ObjAIBase)owner, owner, new Buffs.BrandWildfire(), 5, 1, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
            baseDamage = this.effect0[level];
            doOnce = false;
            count = GetBuffCountFromAll(owner, nameof(Buffs.BrandWildfire));
            if(count <= 4)
            {
                foreach(AttackableUnit unit in GetRandomUnitsInArea(attacker, target.Position, 600, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 10, nameof(Buffs.BrandWildfireMissile), false))
                {
                    if(!doOnce)
                    {
                        if(unit != target)
                        {
                            bool isStealthed;
                            Vector3 attackerPos;
                            isStealthed = GetStealthed(unit);
                            if(!isStealthed)
                            {
                                attackerPos = GetUnitPosition(target);
                                level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                                if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.BrandAblaze)) > 0)
                                {
                                    SpellCast(attacker, unit, default, default, 6, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, attackerPos);
                                }
                                else
                                {
                                    SpellCast(attacker, unit, default, default, 5, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, attackerPos);
                                }
                                doOnce = true;
                            }
                            else
                            {
                                bool canSee;
                                canSee = CanSeeTarget(attacker, unit);
                                if(canSee)
                                {
                                    attackerPos = GetUnitPosition(target);
                                    level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                                    if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.BrandAblaze)) > 0)
                                    {
                                        SpellCast(attacker, unit, default, default, 6, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, attackerPos);
                                    }
                                    else
                                    {
                                        SpellCast(attacker, unit, default, default, 5, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, attackerPos);
                                    }
                                    doOnce = true;
                                }
                            }
                        }
                    }
                }
            }
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.BrandAblaze)) > 0)
            {
                brandSkinID = GetSkinID(attacker);
                teamID = GetTeamID(attacker);
                if(brandSkinID == 3)
                {
                    SpellEffectCreate(out ablazeHitEffect, out _, "BrandConflagration_tar_frost.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out ablazeHitEffect, out _, "BrandConflagration_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.BrandAblaze(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                ApplyDamage(attacker, target, baseDamage + this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 0, false, false, attacker);
            }
            else
            {
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.BrandAblaze(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                ApplyDamage(attacker, target, baseDamage + this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 0, false, false, attacker);
                brandSkinID = GetSkinID(attacker);
                teamID = GetTeamID(attacker);
                if(brandSkinID == 3)
                {
                    SpellEffectCreate(out ablazeHitEffect, out _, "BrandConflagration_tar_frost.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out ablazeHitEffect, out _, "BrandConflagration_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
            }
        }
    }
}