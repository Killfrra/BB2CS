#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ViktorChaosStormAOE : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Infernal Guardian",
            BuffTextureName = "Annie_GuardianIncinerate.dds",
        };
        Particle b;
        Particle c;
        bool soundClear;
        float cDMOD;
        Particle partThing;
        float lastTimeExecuted;
        Particle particleID; // UNUSED
        int[] effect0 = {10, 15, 20};
        public override void OnActivate()
        {
            ObjAIBase caster; // UNUSED
            TeamId ownerTeam;
            caster = SetBuffCasterUnit();
            ownerTeam = GetTeamID(owner);
            SpellEffectCreate(out this.b, out this.c, "Viktor_ChaosStorm_green.troy", "Viktor_ChaosStorm_red.troy", ownerTeam ?? TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            this.soundClear = true;
            this.cDMOD = GetPercentCooldownMod(attacker);
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 centerPos;
            float nEWCD;
            centerPos = GetUnitPosition(owner);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, centerPos, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectUntargetable, nameof(Buffs.ViktorChaosStormGuide), true))
            {
                if(unit is Champion)
                {
                    SpellBuffClear(unit, nameof(Buffs.ViktorChaosStormGuide));
                    SpellBuffRemove(unit, nameof(Buffs.ViktorChaosStormGuide), (ObjAIBase)owner, 0);
                }
                else
                {
                    SetInvulnerable(unit, false);
                    SetTargetable(unit, true);
                    SpellBuffClear(unit, nameof(Buffs.ViktorChaosStormGuide));
                    SpellBuffRemove(unit, nameof(Buffs.ViktorChaosStormGuide), (ObjAIBase)owner, 0);
                    ApplyDamage((ObjAIBase)unit, unit, 25000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, (ObjAIBase)unit);
                }
            }
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 0, false, false, (ObjAIBase)owner);
            SpellBuffRemove(attacker, nameof(Buffs.ViktorChaosStormTimer), attacker, 0);
            SetSpell(attacker, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ViktorChaosStorm));
            nEWCD = 120 * this.cDMOD;
            nEWCD += 120;
            SetSlotSpellCooldownTimeVer2(nEWCD, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker, false);
            SpellEffectRemove(this.b);
            SpellEffectRemove(this.c);
            if(!this.soundClear)
            {
                SpellEffectRemove(this.partThing);
            }
        }
        public override void OnUpdateStats()
        {
            ObjAIBase caster; // UNUSED
            float grandDistance;
            float minDistanceCheck;
            float maxDistanceCheck;
            float distanceVariation;
            float maxSpeed;
            float minSpeed;
            float speedVariation;
            caster = SetBuffCasterUnit();
            grandDistance = DistanceBetweenObjects("Owner", "Caster");
            minDistanceCheck = 350;
            maxDistanceCheck = 950;
            distanceVariation = maxDistanceCheck - minDistanceCheck;
            maxSpeed = 450;
            minSpeed = 175;
            speedVariation = maxSpeed - minSpeed;
            if(grandDistance <= minDistanceCheck)
            {
                IncMoveSpeedFloorMod(owner, maxSpeed);
                charVars.CurrSpeed = maxSpeed;
            }
            else if(grandDistance >= 1500 + maxDistanceCheck)
            {
                IncMoveSpeedFloorMod(owner, minSpeed);
                charVars.CurrSpeed = minSpeed;
            }
            else
            {
                float offsetValue;
                float percOverMinDist;
                float speedToReduce;
                float adjustedSpeed;
                offsetValue = grandDistance - minDistanceCheck;
                percOverMinDist = offsetValue / distanceVariation;
                speedToReduce = percOverMinDist * speedVariation;
                adjustedSpeed = maxSpeed - speedToReduce;
                IncMoveSpeedFloorMod(owner, adjustedSpeed);
                charVars.CurrSpeed = adjustedSpeed;
            }
        }
        public override void OnUpdateActions()
        {
            ObjAIBase caster;
            int level;
            float damageAmount;
            TeamId ownerTeamID; // UNUSED
            float aPPreMod;
            float aPPostMod;
            float finalDamage;
            caster = SetBuffCasterUnit();
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            damageAmount = this.effect0[level];
            ownerTeamID = GetTeamID(caster);
            aPPreMod = GetFlatMagicDamageMod(caster);
            aPPostMod = 0.06f * aPPreMod;
            finalDamage = damageAmount + aPPostMod;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    Particle hi; // UNUSED
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                    SpellEffectCreate(out this.particleID, out _, "Viktor_ChaosStorm_beam.troy", default, TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, owner, "head", default, unit, "spine", default, true, false, false, false, false);
                    SpellEffectCreate(out hi, out _, "Viktor_ChaosStorm_hit.troy", default, TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "root", default, unit, default, default, true, false, false, false, false);
                    if(this.soundClear)
                    {
                        SpellEffectCreate(out this.partThing, out _, "viktor_chaosstorm_damage_sound.troy", default, TeamId.TEAM_NEUTRAL, 10, 0, TeamId.TEAM_UNKNOWN, default, unit, false, owner, default, default, owner, default, default, true, false, false, false, false);
                        this.soundClear = false;
                    }
                }
            }
            if(caster.IsDead)
            {
                SpellBuffRemoveCurrent(owner);
            }
            foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 2500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                float distance;
                distance = DistanceBetweenObjects("Owner", "Unit");
                if(!this.soundClear)
                {
                    if(distance > 350)
                    {
                        this.soundClear = true;
                        SpellEffectRemove(this.partThing);
                    }
                    else
                    {
                    }
                }
            }
        }
    }
}