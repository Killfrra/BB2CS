#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class NocturneParanoia : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {160, 130, 100, 0, 0};
        int[] effect1 = {-300, -300, -300};
        int[] effect2 = {1, 2, 3};
        public override void SelfExecute()
        {
            TeamId teamOfOwner;
            teamOfOwner = GetTeamID(owner);
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                FadeInColorFadeEffect(0, 0, 75, 1, 0.3f, TeamId.TEAM_BLUE);
                FadeOutColorFadeEffect(1, TeamId.TEAM_PURPLE);
                FadeInColorFadeEffect(75, 0, 0, 1, 0.3f, TeamId.TEAM_PURPLE);
            }
            else
            {
                FadeInColorFadeEffect(0, 0, 75, 1, 0.3f, TeamId.TEAM_PURPLE);
                FadeOutColorFadeEffect(1, TeamId.TEAM_BLUE);
                FadeInColorFadeEffect(75, 0, 0, 1, 0.3f, TeamId.TEAM_BLUE);
            }
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.NocturneParanoia)) > 0)
            {
            }
            else
            {
                float nextBuffVars_NewCd;
                Particle asdf; // UNUSED
                int nextBuffVars_SightReduction;
                int nextBuffVars_SpellLevel; // UNUSED
                AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                PlayAnimation("Spell4b", 1, owner, false, false, true);
                nextBuffVars_NewCd = this.effect0[level];
                AddBuff((ObjAIBase)owner, owner, new Buffs.NocturneParanoia(nextBuffVars_NewCd), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.NocturneParanoiaParticle(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                SetSlotSpellCooldownTime(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.25f);
                nextBuffVars_SightReduction = this.effect1[level];
                nextBuffVars_SpellLevel = this.effect2[level];
                if(teamOfOwner == TeamId.TEAM_BLUE)
                {
                    foreach(Champion unit in GetChampions(TeamId.TEAM_BLUE, default, true))
                    {
                        SpellEffectCreate(out asdf, out _, "NocturneParanoiaStartOrderFriend.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, unit, true, unit, "root", default, unit, default, default, false, default, default, false);
                    }
                    foreach(Champion unit in GetChampions(TeamId.TEAM_PURPLE, default, true))
                    {
                        SpellEffectCreate(out asdf, out _, "NocturneParanoiaStartOrderFoe.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, unit, true, unit, "root", default, unit, default, default, false, default, default, false);
                        AddBuff(attacker, unit, new Buffs.NocturneParanoiaTargeting(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, true);
                        BreakSpellShields(unit);
                        AddBuff(attacker, unit, new Buffs.NocturneParanoiaTarget(nextBuffVars_SightReduction), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, true);
                        ApplyNearSight(attacker, unit, 4);
                    }
                    foreach(Champion unit in GetChampions(TeamId.TEAM_UNKNOWN, default, true))
                    {
                        AddBuff(attacker, unit, new Buffs.NocturneParanoiaTargetOrder(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, true);
                    }
                }
                else
                {
                    foreach(Champion unit in GetChampions(TeamId.TEAM_PURPLE, default, true))
                    {
                        SpellEffectCreate(out asdf, out _, "NocturneParanoiaStartChaosFriend.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, unit, true, unit, "root", default, unit, default, default, false, default, default, false);
                    }
                    foreach(Champion unit in GetChampions(TeamId.TEAM_BLUE, default, true))
                    {
                        SpellEffectCreate(out asdf, out _, "NocturneParanoiaStartChaosFoe.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, unit, true, unit, "root", default, unit, default, default, false, default, default, false);
                        AddBuff(attacker, unit, new Buffs.NocturneParanoiaTargeting(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, true);
                        BreakSpellShields(unit);
                        AddBuff(attacker, unit, new Buffs.NocturneParanoiaTarget(nextBuffVars_SightReduction), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, true);
                        ApplyNearSight(attacker, unit, 4);
                    }
                    foreach(Champion unit in GetChampions(TeamId.TEAM_UNKNOWN, default, true))
                    {
                        AddBuff(attacker, unit, new Buffs.NocturneParanoiaTargetChaos(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, true);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class NocturneParanoia : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "NocturneParanoia",
            BuffTextureName = "Nocturne_Paranoia.dds",
            SpellToggleSlot = 4,
        };
        float newCd;
        bool vOSoundCreated;
        float lastTimeExecuted;
        public NocturneParanoia(float newCd = default)
        {
            this.newCd = newCd;
        }
        public override void OnActivate()
        {
            //RequireVar(this.newCd);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Target, owner);
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, -100, PrimaryAbilityResourceType.MANA);
            this.vOSoundCreated = false;
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat;
            float multiplier;
            float newCooldown;
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.SelfAOE, owner);
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * this.newCd;
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.NocturneParanoia));
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.1f, ref this.lastTimeExecuted, false))
            {
                if(!this.vOSoundCreated)
                {
                    TeamId teamOfOwner;
                    this.vOSoundCreated = true;
                    AddBuff(attacker, attacker, new Buffs.NocturneParanoiaVO(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, true);
                    teamOfOwner = GetTeamID(owner);
                    if(teamOfOwner == TeamId.TEAM_BLUE)
                    {
                        foreach(Champion unit in GetChampions(TeamId.TEAM_UNKNOWN, default, true))
                        {
                            AddBuff(attacker, unit, new Buffs.NocturneParanoiaTargetOrderVO(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, true);
                        }
                    }
                    else
                    {
                        foreach(Champion unit in GetChampions(TeamId.TEAM_UNKNOWN, default, true))
                        {
                            AddBuff(attacker, unit, new Buffs.NocturneParanoiaTargetChaosVO(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, true);
                        }
                    }
                }
            }
        }
    }
}