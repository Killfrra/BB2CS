#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class PickACard : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float rnd1; // UNITIALIZED
            float nextBuffVars_Counter;
            bool nextBuffVars_WillRemove; // UNUSED
            if(rnd1 < 0.34f)
            {
                nextBuffVars_Counter = 0;
                SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.BlueCardLock));
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.005f);
            }
            else if(rnd1 < 0.67f)
            {
                nextBuffVars_Counter = 2;
                SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.RedCardLock));
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.005f);
            }
            else
            {
                nextBuffVars_Counter = 4;
                SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.GoldCardLock));
                SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.005f);
            }
            nextBuffVars_WillRemove = false;
            AddBuff((ObjAIBase)owner, target, new Buffs.PickACard_tracker(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, target, new Buffs.PickACard(nextBuffVars_Counter), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class PickACard : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Pick A Card",
            BuffTextureName = "CardMaster_FatesGambit.dds",
        };
        float counter;
        Particle effectID;
        int frozen;
        int removeParticle;
        public PickACard(float counter = default)
        {
            this.counter = counter;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Particle sparks; // UNUSED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out sparks, out _, "AnnieSparks.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            //RequireVar(this.counter);
            //RequireVar(this.willRemove);
            if(this.counter < 2)
            {
                SpellEffectCreate(out this.effectID, out _, "Card_Blue.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, target, default, default, false, false, false, false, false);
            }
            else if(this.counter < 4)
            {
                SpellEffectCreate(out this.effectID, out _, "Card_Red.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, target, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.effectID, out _, "Card_Yellow.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, target, default, default, false, false, false, false, false);
            }
            this.frozen = 0;
            this.removeParticle = 1;
        }
        public override void OnDeactivate(bool expired)
        {
            float baseCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.PickACard));
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            baseCooldown = 6;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * baseCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            if(this.removeParticle != 2)
            {
                SpellEffectRemove(this.effectID);
            }
            SpellBuffRemove(owner, nameof(Buffs.GoldCardPreAttack), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.RedCardPreAttack), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.BlueCardPreattack), (ObjAIBase)owner, 0);
            SetAutoAcquireTargets(owner, true);
        }
        public override void OnUpdateActions()
        {
            if(this.frozen == 0)
            {
                TeamId teamID;
                teamID = GetTeamID(owner);
                this.counter++;
                if(this.counter == 2)
                {
                    SpellEffectRemove(this.effectID);
                    SpellEffectCreate(out this.effectID, out _, "Card_Red.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                    SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.RedCardLock));
                }
                else if(this.counter == 4)
                {
                    SpellEffectRemove(this.effectID);
                    SpellEffectCreate(out this.effectID, out _, "Card_Yellow.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, target, default, default, false, false, false, false, false);
                    SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.GoldCardLock));
                }
                else if(this.counter >= 6)
                {
                    SpellEffectRemove(this.effectID);
                    SpellEffectCreate(out this.effectID, out _, "Card_Blue.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 600, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position, target, default, default, false, false, false, false, false);
                    SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.BlueCardLock));
                    this.counter = 0;
                }
            }
            if(this.removeParticle == 0)
            {
                SpellEffectRemove(this.effectID);
                this.removeParticle = 2;
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            float displayDuration;
            spellName = GetSpellName();
            if(spellName == nameof(Spells.PickACardLock))
            {
                if(this.frozen != 1)
                {
                    SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
                    this.frozen = 1;
                }
            }
            else if(spellName == nameof(Spells.RedCardLock))
            {
                if(this.frozen != 1)
                {
                    displayDuration = GetBuffRemainingDuration(owner, nameof(Buffs.PickACard));
                    SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
                    this.frozen = 1;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RedCardPreAttack(), 1, 1, displayDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    this.removeParticle = false;
                    SpellBuffRemove(owner, nameof(Buffs.PickACard_tracker), (ObjAIBase)owner, 0);
                    SetAutoAcquireTargets(owner, false);
                }
            }
            else if(spellName == nameof(Spells.GoldCardLock))
            {
                if(this.frozen != 1)
                {
                    displayDuration = GetBuffRemainingDuration(owner, nameof(Buffs.PickACard));
                    SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
                    this.frozen = 1;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.GoldCardPreAttack(), 1, 1, displayDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    this.removeParticle = false;
                    SpellBuffRemove(owner, nameof(Buffs.PickACard_tracker), (ObjAIBase)owner, 0);
                    SetAutoAcquireTargets(owner, false);
                }
            }
            else if(spellName == nameof(Spells.BlueCardLock))
            {
                if(this.frozen != 1)
                {
                    displayDuration = GetBuffRemainingDuration(owner, nameof(Buffs.PickACard));
                    SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
                    this.frozen = 1;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.BlueCardPreattack(), 1, 1, displayDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    this.removeParticle = false;
                    SpellBuffRemove(owner, nameof(Buffs.PickACard_tracker), (ObjAIBase)owner, 0);
                    SetAutoAcquireTargets(owner, false);
                }
            }
        }
        public override void OnPreAttack()
        {
            if(target is ObjAIBase)
            {
                if(this.frozen == 1)
                {
                    int level;
                    SkipNextAutoAttack(owner);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(this.counter <= 1)
                    {
                        SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 5, SpellSlotType.ExtraSlots, level, true, false, false, true, true, false);
                    }
                    else if(this.counter <= 3)
                    {
                        SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 6, SpellSlotType.ExtraSlots, level, true, false, false, true, true, false);
                    }
                    else
                    {
                        SpellCast((ObjAIBase)owner, target, target.Position, target.Position, 1, SpellSlotType.ExtraSlots, level, true, false, false, true, true, false);
                    }
                }
            }
        }
    }
}