#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SkarnerImpale : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SkarnerImpale",
            BuffTextureName = "SkarnerImpale.dds",
            PopupMessage = new[]{ "game_floatingtext_Suppressed", },
        };
        int numHitsRemaining;
        Particle chainPartID;
        Particle zParticle;
        Particle cParticle;
        Particle crystalineParticle;
        Region victimBubble;
        float lastTimeExecuted;
        int[] effect0 = {100, 150, 200, 0, 0};
        public override void OnActivate()
        {
            TeamId ownerTeamID;
            int level;
            string flashCheck;
            ownerTeamID = GetTeamID(attacker);
            SetCanAttack(attacker, false);
            SetStunned(owner, true);
            SetSuppressed(owner, true);
            PauseAnimation(owner, true);
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                SealSpellSlot(2, SpellSlotType.SpellSlots, attacker, true, SpellbookType.SPELLBOOK_CHAMPION);
            }
            flashCheck = GetSlotSpellName(attacker, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, attacker, true, SpellbookType.SPELLBOOK_SUMMONER);
            }
            flashCheck = GetSlotSpellName(attacker, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(1, SpellSlotType.SpellSlots, attacker, true, SpellbookType.SPELLBOOK_SUMMONER);
            }
            OverrideAnimation("Run", "Spell4_Backstep", attacker);
            OverrideAnimation("Idle1", "Spell4_Idleback", attacker);
            OverrideAnimation("Idle2", "Spell4_Idleback", attacker);
            OverrideAnimation("Idle3", "Spell4_Idleback", attacker);
            OverrideAnimation("Idle4", "Spell4_Idleback", attacker);
            OverrideAnimation("Spell2", "spell4_W", attacker);
            OverrideAnimation("Spell1", "spell4_Q", attacker);
            this.numHitsRemaining = 4;
            SpellEffectCreate(out this.chainPartID, out _, "skarner_ult_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, owner, "spine", default, attacker, "tail_t", default, false, false, false, false, false);
            SpellEffectCreate(out this.zParticle, out _, "skarner_ult_tail_tip.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "tail_t", default, attacker, "Bird_head", default, false, false, false, false, false);
            SpellEffectCreate(out this.cParticle, out _, "skarner_ult_tar_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, owner, "spine", default, attacker, "Bird_head", default, false, false, false, false, false);
            SpellEffectCreate(out this.crystalineParticle, out _, "skarner_ult_tar_04.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, owner, "spine", default, attacker, "Bird_head", default, false, false, false, false, false);
            this.victimBubble = AddUnitPerceptionBubble(ownerTeamID, 10, owner, 2, default, owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            string flashCheck;
            float damagePerTick;
            float duration;
            TeamId teamID;
            Particle motaExplosion; // UNUSED
            float hP;
            Vector3 pos;
            SpellEffectRemove(this.chainPartID);
            SpellEffectRemove(this.cParticle);
            SpellEffectRemove(this.zParticle);
            SpellEffectRemove(this.crystalineParticle);
            PauseAnimation(owner, false);
            SetCanAttack(attacker, true);
            SetStunned(owner, false);
            SetSuppressed(owner, false);
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, attacker, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                SealSpellSlot(1, SpellSlotType.SpellSlots, attacker, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(level > 0)
            {
                SealSpellSlot(2, SpellSlotType.SpellSlots, attacker, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            flashCheck = GetSlotSpellName(attacker, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, attacker, false, SpellbookType.SPELLBOOK_SUMMONER);
            }
            flashCheck = GetSlotSpellName(attacker, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(1, SpellSlotType.SpellSlots, attacker, false, SpellbookType.SPELLBOOK_SUMMONER);
            }
            ClearOverrideAnimation("Run", attacker);
            ClearOverrideAnimation("Idle1", attacker);
            ClearOverrideAnimation("Idle2", attacker);
            ClearOverrideAnimation("Idle3", attacker);
            ClearOverrideAnimation("Idle4", attacker);
            ClearOverrideAnimation("Spell2", attacker);
            ClearOverrideAnimation("Spell1", attacker);
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            damagePerTick = this.effect0[level];
            duration = GetBuffRemainingDuration(owner, nameof(Buffs.SkarnerImpale));
            if(duration <= 0)
            {
                ApplyDamage(attacker, owner, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 0, false, false, attacker);
                teamID = GetTeamID(attacker);
                SpellEffectCreate(out motaExplosion, out _, "skarner_ult_tar_03.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
            RemovePerceptionBubble(this.victimBubble);
            SpellBuffClear(attacker, nameof(Buffs.SkarnerImpaleBuff));
            hP = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if(hP > 0)
            {
                pos = GetPointByUnitFacingOffset(attacker, 100, 180);
                FaceDirection(attacker, pos);
                PlayAnimation("Run", 0, attacker, false, false, false);
            }
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(attacker, false);
            SetStunned(owner, true);
            SetSuppressed(owner, true);
        }
        public override void OnUpdateActions()
        {
            Vector3 pos;
            float distance;
            float mS;
            float damageTime; // UNITIALIZED
            TeamId teamID; // UNUSED
            string flashCheck;
            PauseAnimation(owner, true);
            pos = GetPointByUnitFacingOffset(attacker, -75, 0);
            distance = DistanceBetweenObjectAndPoint(owner, pos);
            mS = distance * 2.6f;
            Move(owner, pos, mS, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true, damageTime))
            {
                teamID = GetTeamID(attacker);
                if(this.numHitsRemaining <= 0)
                {
                    SpellBuffRemoveCurrent(attacker);
                }
                if(owner.IsDead)
                {
                    SpellBuffRemoveCurrent(attacker);
                }
            }
            flashCheck = GetSlotSpellName(attacker, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, attacker, true, SpellbookType.SPELLBOOK_SUMMONER);
            }
            flashCheck = GetSlotSpellName(attacker, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(flashCheck == nameof(Spells.SummonerFlash))
            {
                SealSpellSlot(1, SpellSlotType.SpellSlots, attacker, true, SpellbookType.SPELLBOOK_SUMMONER);
            }
        }
    }
}
namespace Spells
{
    public class SkarnerImpale : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {100, 150, 200, 0, 0};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float suppressionDuration;
            AttackableUnit nextBuffVars_Victim;
            float damagePerTick;
            float hP;
            Vector3 pos;
            suppressionDuration = 1.75f;
            nextBuffVars_Victim = target;
            AddBuff(attacker, target, new Buffs.SkarnerImpale(), 1, 1, suppressionDuration, BuffAddType.REPLACE_EXISTING, BuffType.SUPPRESSION, 0, true, false, false);
            AddBuff((ObjAIBase)target, owner, new Buffs.SkarnerImpaleBuff(), 1, 1, suppressionDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            damagePerTick = this.effect0[level];
            ApplyDamage(attacker, target, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 0, false, false, attacker);
            hP = GetHealth(target, PrimaryAbilityResourceType.MANA);
            if(hP > 0)
            {
                IssueOrder(owner, OrderType.Hold, default, owner);
                pos = GetPointByUnitFacingOffset(owner, 100, 180);
                FaceDirection(owner, pos);
                PlayAnimation("Spell4_Idleback", 0, owner, false, false, false);
            }
        }
    }
}