#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SummonerFortify : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override float AdjustCooldown()
        {
            float returnValue = 0;
            if(avatarVars.SummonerCooldownBonus != 0)
            {
                float cooldownMultiplier;
                float baseCooldown;
                cooldownMultiplier = 1 - avatarVars.SummonerCooldownBonus;
                baseCooldown = 300 * cooldownMultiplier;
            }
            returnValue = baseCooldown;
            return returnValue;
        }
        public override void SelfExecute()
        {
            Particle ar; // UNUSED
            SpellEffectCreate(out ar, out _, "summoner_cast.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.FortifyBuff)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.FortifyBuff), (ObjAIBase)owner);
            }
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            bool nextBuffVars_Splash;
            if(avatarVars.FortifySplashDamage == 1)
            {
                nextBuffVars_Splash = true;
            }
            else
            {
                nextBuffVars_Splash = false;
            }
            if(target is BaseTurret)
            {
                AddBuff(attacker, target, new Buffs.SummonerFortify(nextBuffVars_Splash), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.INVULNERABILITY, 0, true);
            }
        }
    }
}
namespace Buffs
{
    public class SummonerFortify : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Invulnerability.troy", },
            BuffName = "Fortify",
            BuffTextureName = "Summoner_fortify.dds",
        };
        bool splash;
        public SummonerFortify(bool splash = default)
        {
            this.splash = splash;
        }
        public override void OnActivate()
        {
            SetPhysicalImmune(owner, true);
            SetMagicImmune(owner, true);
            //RequireVar(this.splash);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetPhysicalImmune(owner, false);
            SetMagicImmune(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetPhysicalImmune(owner, true);
            SetMagicImmune(owner, true);
            IncPercentAttackSpeedMod(owner, 1);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(this.splash)
            {
                float newDamage;
                newDamage = damageAmount * 0.5f;
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if(unit != target)
                    {
                        ApplyDamage(attacker, unit, newDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 1, 1, false, false);
                    }
                }
            }
        }
    }
}