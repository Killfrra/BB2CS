#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KogMawIcathianSurprise : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "C_Mouth_d", },
            AutoBuffActivateEffect = new[]{ "KogMawIcathianSurprise_foam.troy", "KogMawIcathianSurprise_splats.troy", },
            BuffName = "KogMawIcathianSurprise",
            BuffTextureName = "KogMaw_IcathianSurprise.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float moveSpeedMod;
        int casterID; // UNUSED
        object other3;
        float lastTimeExecuted2;
        float lastTimeExecuted;
        public KogMawIcathianSurprise(object other3 = default)
        {
            this.other3 = other3;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type != BuffType.INTERNAL)
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            Particle particle; // UNUSED
            float currentCooldown2;
            float currentCooldown;
            AddBuff((ObjAIBase)owner, owner, new Buffs.KogMawIcathianSurpriseSound(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            SpellEffectCreate(out particle, out _, "KogMawDeathProc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle, out _, "KogMawDeathBackBeam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_Chest", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle, out _, "KogMawDeathBackBeam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_Waist", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle, out _, "KogMawDeathBackBeam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_Root", default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle, out _, "KogMawDeathBackBeam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_Tail", default, target, default, default, false, false, false, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            this.moveSpeedMod = 0;
            PlayAnimation("death", 0.75f, owner, false, false, true);
            ShowHealthBar(owner, false);
            currentCooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            currentCooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if(currentCooldown <= 5)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 5);
            }
            if(currentCooldown2 <= 5)
            {
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots, 5);
            }
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetSilenced(owner, true);
            SetInvulnerable(owner, true);
            SetTargetable(owner, false);
            SetForceRenderParticles(owner, true);
            SetCanMove(owner, false);
            OverrideAnimation("Run", "RunDead", owner);
            AddBuff((ObjAIBase)owner, owner, new Buffs.Untargetable(), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            SpellBuffRemoveType(owner, BuffType.COMBAT_DEHANCER);
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.INVISIBILITY);
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
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffRemoveType(owner, BuffType.POISON);
        }
        public override void OnDeactivate(bool expired)
        {
            int levelDamage;
            float bonusDamage;
            float totalDamage;
            object other3; // UNUSED
            Particle e; // UNUSED
            levelDamage = GetLevel(owner);
            bonusDamage = levelDamage * 25;
            totalDamage = bonusDamage + 100;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage((ObjAIBase)owner, unit, totalDamage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
            }
            ForceDead(owner);
            SetTargetable(owner, true);
            SetInvulnerable(owner, false);
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetSilenced(owner, false);
            SetCanMove(owner, true);
            SetForceRenderParticles(owner, false);
            SetTargetable(owner, true);
            StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.Die);
            PopAllCharacterData(owner);
            ClearOverrideAnimation("Run", owner);
            this.casterID = PushCharacterData("KogMawDead", owner, false);
            other3 = this.other3;
            SpellEffectCreate(out e, out _, "KogMawDeath_nova.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out e, out _, "KogMawDead_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "intestines_1", default, owner, default, default, false, false, false, false, false);
            SetInvulnerable(owner, false);
            AddBuff(attacker, owner, new Buffs.KogMawIcathianSurprise(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            ShowHealthBar(owner, true);
        }
        public override void OnUpdateStats()
        {
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted2, false))
            {
                this.moveSpeedMod += 0.025f;
            }
            IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedMod);
            IncFlatAttackRangeMod(owner, -500);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetTargetable(owner, false);
            SetForceRenderParticles(owner, true);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                SetCanMove(owner, true);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            damageAmount = 0;
        }
    }
}