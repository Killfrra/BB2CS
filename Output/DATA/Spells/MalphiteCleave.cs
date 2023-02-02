#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MalphiteCleave : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MalphiteCleave",
            BuffTextureName = "Malphite_BrutalStrikes.dds",
            IsDeathRecapSource = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float[] effect0 = {0.3f, 0.38f, 0.46f, 0.54f, 0.62f};
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is not BaseTurret)
            {
                int level;
                float cleavePerc;
                float damageAmount2;
                Particle a; // UNUSED
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                cleavePerc = this.effect0[level];
                damageAmount2 = damageAmount * cleavePerc;
                if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.ObduracyBuff)) > 0)
                {
                    SpellEffectCreate(out a, out _, "MalphiteCleaveEnragedHit.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out a, out _, "MalphiteCleaveHit.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    if(unit != target)
                    {
                        float distance;
                        distance = DistanceBetweenObjects("Attacker", "Unit");
                        if(distance < 200)
                        {
                            if(IsInFront(owner, unit))
                            {
                                if(damageType == DamageType.DAMAGE_TYPE_MAGICAL)
                                {
                                    ApplyDamage(attacker, unit, damageAmount2, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
                                    SpellEffectCreate(out a, out _, "globalhit_physical.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                    if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.ObduracyBuff)) > 0)
                                    {
                                        SpellEffectCreate(out a, out _, "MalphiteCleaveEnragedHit.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                    }
                                    else
                                    {
                                        SpellEffectCreate(out a, out _, "MalphiteCleaveHit.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                    }
                                }
                                else if(damageType == DamageType.DAMAGE_TYPE_PHYSICAL)
                                {
                                    ApplyDamage(attacker, unit, damageAmount2, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                                    SpellEffectCreate(out a, out _, "globalhit_physical.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                    if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.ObduracyBuff)) > 0)
                                    {
                                        SpellEffectCreate(out a, out _, "MalphiteCleaveEnragedHit.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                    }
                                    else
                                    {
                                        SpellEffectCreate(out a, out _, "MalphiteCleaveHit.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                    }
                                }
                                else
                                {
                                    ApplyDamage(attacker, unit, damageAmount2, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
                                    SpellEffectCreate(out a, out _, "globalhit_physical.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                    if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.ObduracyBuff)) > 0)
                                    {
                                        SpellEffectCreate(out a, out _, "MalphiteCleaveEnragedHit.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                    }
                                    else
                                    {
                                        SpellEffectCreate(out a, out _, "MalphiteCleaveHit.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}