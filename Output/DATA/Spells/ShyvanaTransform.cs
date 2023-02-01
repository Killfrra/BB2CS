#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShyvanaTransform : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", "", },
            BuffName = "ShyvanaTransform",
            BuffTextureName = "ShyvanaDragonsDescent.dds",
        };
        int casterID;
        float lastTimeExecuted;
        float[] effect0 = {0.3f, 0.35f, 0.4f, 0.45f, 0.5f};
        int[] effect1 = {25, 40, 55, 70, 85};
        int[] effect2 = {10, 9, 8, 7, 6};
        float[] effect3 = {0.3f, 0.35f, 0.4f, 0.45f, 0.5f};
        int[] effect4 = {25, 40, 55, 70, 85};
        int[] effect5 = {10, 9, 8, 7, 6};
        public override void OnActivate()
        {
            float remainingDuration;
            int level;
            float nextBuffVars_MovementSpeed;
            int nextBuffVars_DamagePerTick;
            int nextBuffVars_SpellCooldown;
            float cooldown;
            float cooldown2;
            float cooldown3;
            this.casterID = PushCharacterData("ShyvanaDragon", owner, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaImmolationAura)) > 0)
            {
                remainingDuration = GetBuffRemainingDuration(owner, nameof(Buffs.ShyvanaImmolationAura));
                SpellBuffRemove(owner, nameof(Buffs.ShyvanaImmolationAura), (ObjAIBase)owner, 0);
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_MovementSpeed = this.effect0[level];
                nextBuffVars_DamagePerTick = this.effect1[level];
                AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaImmolateDragon(nextBuffVars_DamagePerTick, nextBuffVars_MovementSpeed), 1, 1, remainingDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaDoubleAttack)) > 0)
            {
                remainingDuration = GetBuffRemainingDuration(owner, nameof(Buffs.ShyvanaDoubleAttack));
                SpellBuffRemove(owner, nameof(Buffs.ShyvanaDoubleAttack), (ObjAIBase)owner, 0);
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_SpellCooldown = this.effect2[level];
                AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaDoubleAttackDragon(nextBuffVars_SpellCooldown), 1, 1, remainingDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ShyvanaDoubleAttackDragon));
            SetSlotSpellCooldownTimeVer2(cooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ShyvanaImmolateDragon));
            SetSlotSpellCooldownTimeVer2(cooldown2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cooldown3 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ShyvanaFireballDragon2));
            SetSlotSpellCooldownTimeVer2(cooldown3, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaDragonScales(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            int nextBuffVars_CasterID;
            float nextBuffVars_MovementSpeed;
            float nextBuffVars_DamagePerTick;
            int nextBuffVars_SpellCooldown;
            float remainingDuration;
            int level;
            float cooldown;
            float cooldown2;
            float cooldown3;
            TeamId teamID;
            Particle a; // UNUSED
            if(owner.IsDead)
            {
                nextBuffVars_CasterID = this.casterID;
                AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaTransformDeath(nextBuffVars_CasterID), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                PopCharacterData(owner, this.casterID);
            }
            IncPAR(owner, -100, PrimaryAbilityResourceType.Other);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaImmolateDragon)) > 0)
            {
                remainingDuration = GetBuffRemainingDuration(owner, nameof(Buffs.ShyvanaImmolateDragon));
                SpellBuffRemove(owner, nameof(Buffs.ShyvanaImmolateDragon), (ObjAIBase)owner, 0);
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_MovementSpeed = this.effect3[level];
                nextBuffVars_DamagePerTick = this.effect4[level];
                AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaImmolationAura(nextBuffVars_DamagePerTick, nextBuffVars_MovementSpeed), 1, 1, remainingDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaDoubleAttackDragon)) > 0)
            {
                remainingDuration = GetBuffRemainingDuration(owner, nameof(Buffs.ShyvanaDoubleAttackDragon));
                SpellBuffRemove(owner, nameof(Buffs.ShyvanaDoubleAttackDragon), (ObjAIBase)owner, 0);
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_SpellCooldown = this.effect5[level];
                AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaDoubleAttack(nextBuffVars_SpellCooldown), 1, 1, remainingDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ShyvanaDoubleAttack));
            SetSlotSpellCooldownTimeVer2(cooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ShyvanaImmolationAura));
            SetSlotSpellCooldownTimeVer2(cooldown2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            cooldown3 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.ShyvanaFireball));
            SetSlotSpellCooldownTimeVer2(cooldown3, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out a, out _, "shyvana_ult_transform_end.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ShyvanaDragonScales(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnUpdateActions()
        {
            float furyRemaining;
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                IncPAR(owner, -3, PrimaryAbilityResourceType.Other);
                furyRemaining = GetPAR(owner, PrimaryAbilityResourceType.Other);
                if(furyRemaining <= 0)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            float furyRemaining;
            spellName = GetSpellName();
            if(spellName == nameof(Spells.ShyvanaFireballDragon))
            {
                furyRemaining = GetPAR(owner, PrimaryAbilityResourceType.Other);
                if(furyRemaining <= 3)
                {
                    IncPAR(owner, 3, PrimaryAbilityResourceType.Other);
                }
            }
        }
    }
}