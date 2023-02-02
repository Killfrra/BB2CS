#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MordekaiserCOTGPetBuff2 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MordekaiserCOTGPet",
            BuffTextureName = "Mordekaiser_COTG.dds",
            IsPetDurationBuff = true,
        };
        Particle particle;
        Particle particle2;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            TeamId teamID;
            float petDamage;
            float petAP;
            float nextBuffVars_PetDamage;
            float nextBuffVars_PetAP;
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out this.particle, out _, "mordekeiser_cotg_skin.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.particle2, out _, "mordekaiser_cotg_ring.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 500, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
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
            SpellBuffRemoveType(owner, BuffType.SHRED);
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            SetGhosted(owner, true);
            petDamage = GetTotalAttackDamage(owner);
            petDamage *= 0.2f;
            petAP = GetFlatMagicDamageMod(owner);
            petAP *= 0.2f;
            nextBuffVars_PetDamage = petDamage;
            nextBuffVars_PetAP = petAP;
            AddBuff(attacker, attacker, new Buffs.MordekaiserCOTGSelf(nextBuffVars_PetDamage, nextBuffVars_PetAP), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(attacker, owner, new Buffs.MordekaiserCOTGPetBuff(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeblancPassive)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.LeblancPassive), (ObjAIBase)owner, 0);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, (ObjAIBase)owner);
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.MordekaiserCOTGSelf)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.MordekaiserCOTGSelf), attacker, 0);
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
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                if(attacker.IsDead)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            ObjAIBase caster;
            float nextBuffVars_DamageToDeal;
            if(target is ObjAIBase)
            {
                caster = SetBuffCasterUnit();
                nextBuffVars_DamageToDeal = damageAmount;
                damageAmount -= damageAmount;
                AddBuff((ObjAIBase)target, caster, new Buffs.MordekaiserCOTGPetDmg(nextBuffVars_DamageToDeal), 1, 1, 0.001f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                caster = SetBuffCasterUnit();
                AddBuff(caster, caster, new Buffs.MordekaiserCOTGPetDmg(nextBuffVars_DamageToDeal), 1, 1, 0.001f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                ApplyDamage(caster, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, true, true, caster);
                damageAmount = 0;
            }
        }
    }
}