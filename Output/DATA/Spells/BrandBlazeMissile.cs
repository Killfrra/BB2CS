#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BrandBlazeMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {80, 120, 160, 200, 240};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID; // UNUSED
            float spellBaseDamage;
            bool isStealthed;
            bool canSee;
            teamID = GetTeamID(attacker);
            spellBaseDamage = this.effect0[level];
            isStealthed = GetStealthed(target);
            if(!isStealthed)
            {
                DestroyMissile(missileNetworkID);
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.BrandSearParticle(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                if(GetBuffCountFromCaster(target, owner, nameof(Buffs.BrandAblaze)) > 0)
                {
                    ApplyStun(attacker, target, 2);
                }
                ApplyDamage(attacker, target, spellBaseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.65f, 0, false, false, attacker);
                AddBuff(attacker, target, new Buffs.BrandAblaze(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            else
            {
                if(target is Champion)
                {
                    DestroyMissile(missileNetworkID);
                    BreakSpellShields(target);
                    AddBuff(attacker, target, new Buffs.BrandSearParticle(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    if(GetBuffCountFromCaster(target, owner, nameof(Buffs.BrandAblaze)) > 0)
                    {
                        ApplyStun(attacker, target, 2);
                    }
                    ApplyDamage(attacker, target, spellBaseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.65f, 0, false, false, attacker);
                    AddBuff(attacker, target, new Buffs.BrandAblaze(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
                else
                {
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        DestroyMissile(missileNetworkID);
                        BreakSpellShields(target);
                        AddBuff(attacker, target, new Buffs.BrandSearParticle(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        if(GetBuffCountFromCaster(target, owner, nameof(Buffs.BrandAblaze)) > 0)
                        {
                            ApplyStun(attacker, target, 2);
                        }
                        ApplyDamage(attacker, target, spellBaseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0.65f, 0, false, false, attacker);
                        AddBuff(attacker, target, new Buffs.BrandAblaze(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}