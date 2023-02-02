#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShyvanaFireballMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {80, 125, 170, 215, 260};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID; // UNUSED
            float spellBaseDamage;
            bool isStealthed;
            teamID = GetTeamID(attacker);
            spellBaseDamage = this.effect0[level];
            isStealthed = GetStealthed(target);
            if(!isStealthed)
            {
                DestroyMissile(missileNetworkID);
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.ShyvanaFireballParticle(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                ApplyDamage(attacker, target, spellBaseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 0, false, false, attacker);
                AddBuff(attacker, target, new Buffs.ShyvanaFireballMissile(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
            else
            {
                if(target is Champion)
                {
                    DestroyMissile(missileNetworkID);
                    BreakSpellShields(target);
                    AddBuff(attacker, target, new Buffs.ShyvanaFireballParticle(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    ApplyDamage(attacker, target, spellBaseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 0, false, false, attacker);
                    AddBuff(attacker, target, new Buffs.ShyvanaFireballMissile(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
                else
                {
                    bool canSee;
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        DestroyMissile(missileNetworkID);
                        BreakSpellShields(target);
                        AddBuff(attacker, target, new Buffs.ShyvanaFireballParticle(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        ApplyDamage(attacker, target, spellBaseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0.6f, 0, false, false, attacker);
                        AddBuff(attacker, target, new Buffs.ShyvanaFireballMissile(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class ShyvanaFireballMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "ShyvanaFlameBreathDebuff",
            BuffTextureName = "ShyvanaFlameBreath.dds",
        };
        Particle a;
        Particle b; // UNUSED
        Particle c;
        int[] effect0 = {12, 19, 26, 32, 39};
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out this.a, out _, "shyvana_flameBreath_dragon_burn.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.b, out _, "shyvana_flameBreath_tar_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.c, out _, "shyvana_flameBreath_indicator.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.a);
            SpellEffectRemove(this.c);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorMod(owner, -0.15f);
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            if(caster == attacker)
            {
                int level;
                float procDamage;
                TeamId teamID;
                Particle a; // UNUSED
                level = GetSlotSpellLevel(caster, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                procDamage = this.effect0[level];
                ApplyDamage(caster, target, procDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.09f, 0, false, false, caster);
                teamID = GetTeamID(caster);
                SpellEffectCreate(out a, out _, "shyvana_flameBreath_reignite.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
        }
    }
}