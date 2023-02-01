#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickRAPetBuff2 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "YorickOmenRevenant",
            BuffTextureName = "YorickOmenOfDeath.dds",
            IsPetDurationBuff = true,
        };
        Particle particle5;
        Particle particle;
        Particle particle3;
        Particle particle4;
        float aDRatio;
        float[] effect0 = {0.45f, 0.6f, 0.75f};
        public override void OnActivate()
        {
            TeamId teamID;
            ObjAIBase caster;
            int level;
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out this.particle5, out this.particle5, "yorick_ult_revive_tar.troy", default, teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.particle, out this.particle, "yorick_ult_02.troy", default, teamID, 500, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out this.particle3, out this.particle4, "yorick_revive_skin_teamID_green.troy", "yorick_revive_skin_teamID_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SpellBuffRemoveType(owner, BuffType.COMBAT_ENCHANCER);
            SpellBuffRemoveType(owner, BuffType.COMBAT_DEHANCER);
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.DAMAGE);
            SpellBuffRemoveType(owner, BuffType.HEAL);
            SpellBuffRemoveType(owner, BuffType.HASTE);
            SpellBuffRemoveType(owner, BuffType.SPELL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.PHYSICAL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.INVULNERABILITY);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffRemoveType(owner, BuffType.POISON);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            SetGhosted(owner, true);
            IncPermanentPercentHPRegenMod(owner, -1);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeblancPassive)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.LeblancPassive), (ObjAIBase)owner, 0);
            }
            caster = SetBuffCasterUnit();
            level = GetSlotSpellLevel(caster, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.aDRatio = this.effect0[level];
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            Particle a; // UNUSED
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                foreach(Champion unit in GetChampions(TeamId.TEAM_BLUE, nameof(Buffs.YorickReviveAllySelf), true))
                {
                    SpellBuffClear(unit, nameof(Buffs.YorickReviveAllySelf));
                }
            }
            else
            {
                foreach(Champion unit in GetChampions(TeamId.TEAM_PURPLE, nameof(Buffs.YorickReviveAllySelf), true))
                {
                    SpellBuffClear(unit, nameof(Buffs.YorickReviveAllySelf));
                }
            }
            if(owner.IsDead)
            {
                SpellEffectCreate(out a, out _, "YorickRevenantDeathSound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            }
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, (ObjAIBase)owner);
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle3);
            SpellEffectRemove(this.particle4);
            SpellEffectRemove(this.particle5);
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.YorickRASelf)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.YorickRASelf), attacker, 0);
            }
        }
        public override void OnUpdateStats()
        {
            ObjAIBase caster;
            float casMovespeedMod;
            float ownMovespeedMod;
            float movespeedDiff;
            caster = SetBuffCasterUnit();
            casMovespeedMod = GetFlatMovementSpeedMod(caster);
            ownMovespeedMod = GetFlatMovementSpeedMod(owner);
            movespeedDiff = casMovespeedMod - ownMovespeedMod;
            IncFlatMovementSpeedMod(owner, movespeedDiff);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.KogMawIcathianSurprise)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.KogMawIcathianSurprise), (ObjAIBase)owner, 0);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.KogMawIcathianSurpriseReady)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.KogMawIcathianSurpriseReady), (ObjAIBase)owner, 0);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            ObjAIBase caster;
            damageAmount *= this.aDRatio;
            caster = SetBuffCasterUnit();
            ApplyDamage(caster, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, true, true, caster);
            damageAmount = 0;
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            float effectiveHeal;
            if(health >= 0)
            {
                effectiveHeal = health * 0;
                returnValue = effectiveHeal;
            }
            return returnValue;
        }
    }
}