#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class EzrealMysticShotMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {35, 55, 75, 95, 115};
        int[] effect1 = {0, 0, 0, 0, 0};
        int[] effect2 = {0, 0, 0, 0, 0};
        int[] effect3 = {0, 0, 0, 0, 0};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float cooldown;
            float cooldown1;
            float cooldown2;
            float cooldown3;
            float spellBaseDamage;
            float baseDamage;
            float attackDamage;
            float damageVar;
            float aP;
            float finalAP;
            float finalDamage;
            bool isStealthed;
            float newCooldown;
            float newCooldown1;
            float newCooldown2;
            float newCooldown3;
            Particle gragas; // UNUSED
            bool canSee;
            teamID = GetTeamID(attacker);
            cooldown = GetSlotSpellCooldownTime(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cooldown1 = GetSlotSpellCooldownTime(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cooldown2 = GetSlotSpellCooldownTime(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cooldown3 = GetSlotSpellCooldownTime(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            spellBaseDamage = this.effect0[level];
            baseDamage = GetTotalAttackDamage(owner);
            attackDamage = 1 * baseDamage;
            damageVar = spellBaseDamage + attackDamage;
            aP = GetFlatMagicDamageMod(owner);
            finalAP = aP * 0.2f;
            finalDamage = damageVar + finalAP;
            isStealthed = GetStealthed(target);
            if(!isStealthed)
            {
                if(cooldown > 0)
                {
                    newCooldown = cooldown - 1;
                    SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                }
                if(cooldown1 > 0)
                {
                    newCooldown1 = cooldown1 - 1;
                    SetSlotSpellCooldownTimeVer2(newCooldown1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                }
                if(cooldown2 > 0)
                {
                    newCooldown2 = cooldown2 - 1;
                    SetSlotSpellCooldownTimeVer2(newCooldown2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                }
                if(cooldown3 > 0)
                {
                    newCooldown3 = cooldown3 - 1;
                    SetSlotSpellCooldownTimeVer2(newCooldown3, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                }
                BreakSpellShields(target);
                ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                SpellEffectCreate(out gragas, out _, "Ezreal_mysticshot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                DestroyMissile(missileNetworkID);
                AddBuff(attacker, attacker, new Buffs.EzrealRisingSpellForce(), 5, 1, 6 + this.effect1[level], BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            else
            {
                if(target is Champion)
                {
                    if(cooldown > 0)
                    {
                        newCooldown = cooldown - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                    }
                    if(cooldown1 > 0)
                    {
                        newCooldown1 = cooldown1 - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                    }
                    if(cooldown2 > 0)
                    {
                        newCooldown2 = cooldown2 - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                    }
                    if(cooldown3 > 0)
                    {
                        newCooldown3 = cooldown3 - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown3, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                    }
                    BreakSpellShields(target);
                    ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                    SpellEffectCreate(out gragas, out _, "Ezreal_mysticshot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                    DestroyMissile(missileNetworkID);
                    AddBuff(attacker, attacker, new Buffs.EzrealRisingSpellForce(), 5, 1, 6 + this.effect2[level], BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
                else
                {
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        if(cooldown > 0)
                        {
                            newCooldown = cooldown - 1;
                            SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                        }
                        if(cooldown1 > 0)
                        {
                            newCooldown1 = cooldown1 - 1;
                            SetSlotSpellCooldownTimeVer2(newCooldown1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                        }
                        if(cooldown2 > 0)
                        {
                            newCooldown2 = cooldown2 - 1;
                            SetSlotSpellCooldownTimeVer2(newCooldown2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                        }
                        if(cooldown3 > 0)
                        {
                            newCooldown3 = cooldown3 - 1;
                            SetSlotSpellCooldownTimeVer2(newCooldown3, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
                        }
                        BreakSpellShields(target);
                        ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
                        SpellEffectCreate(out gragas, out _, "Ezreal_mysticshot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                        DestroyMissile(missileNetworkID);
                        AddBuff(attacker, attacker, new Buffs.EzrealRisingSpellForce(), 5, 1, 6 + this.effect3[level], BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}