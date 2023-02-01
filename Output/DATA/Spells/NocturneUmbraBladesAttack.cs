#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NocturneUmbraBladesAttack : BBBuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            SpellBuffRemove(owner, nameof(Buffs.NocturneUmbraBlades), (ObjAIBase)owner);
        }
    }
}
namespace Spells
{
    public class NocturneUmbraBladesAttack : BBSpellScript
    {
        int[] effect0 = {15, 15, 15, 15, 15, 15, 20, 20, 20, 20, 20, 20, 25, 25, 25, 25, 25, 25};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float heal;
            float dmg;
            float mainDmg;
            Particle fadeParticle; // UNUSED
            Vector3 unit; // UNITIALIZED
            bool isStealthed;
            bool canSee;
            teamID = GetTeamID(owner);
            level = GetLevel(owner);
            heal = this.effect0[level];
            dmg = GetTotalAttackDamage(owner);
            if(hitResult == HitResult.HIT_Critical)
            {
                mainDmg = dmg * 1.1f;
            }
            else
            {
                mainDmg = dmg * 1.2f;
            }
            ApplyDamage(attacker, target, mainDmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            IncHealth(owner, heal, owner);
            dmg *= 1.2f;
            SpellEffectCreate(out fadeParticle, out _, "NocturneUmbraBlades_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, unit, target, default, default, true, default, default, false);
            AddBuff(attacker, target, new Buffs.IfHasBuffCheck(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, attacker.Position, 360, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.IfHasBuffCheck)) == 0)
                {
                    isStealthed = GetStealthed(unit);
                    if(!isStealthed)
                    {
                        SpellEffectCreate(out fadeParticle, out _, "NocturneUmbraBlades_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, unit.Position, unit, default, default, true, default, default, false);
                        ApplyDamage(attacker, unit, dmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, true, attacker);
                        IncHealth(owner, heal, owner);
                    }
                    else
                    {
                        if(unit is Champion)
                        {
                            SpellEffectCreate(out fadeParticle, out _, "NocturneUmbraBlades_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, unit.Position, unit, default, default, true, default, default, false);
                            ApplyDamage(attacker, unit, dmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, true, attacker);
                            IncHealth(owner, heal, owner);
                        }
                        else
                        {
                            canSee = CanSeeTarget(owner, unit);
                            if(canSee)
                            {
                                SpellEffectCreate(out fadeParticle, out _, "NocturneUmbraBlades_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, unit.Position, unit, default, default, true, default, default, false);
                                ApplyDamage(attacker, unit, dmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, true, attacker);
                                IncHealth(owner, heal, owner);
                            }
                        }
                    }
                }
            }
        }
        public override void SelfExecute()
        {
            AddBuff(attacker, attacker, new Buffs.NocturneUmbraBladesAttack(), 1, 1, 0.01f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}