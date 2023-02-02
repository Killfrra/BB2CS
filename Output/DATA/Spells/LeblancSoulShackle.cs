#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class LeblancSoulShackle : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 22f, 20f, 18f, 16f, 14f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {40, 65, 90, 115, 140};
        float[] effect1 = {1, 1.3f, 1.6f, 1.9f, 2.2f};
        float[] effect2 = {-0.25f, -0.25f, -0.25f, -0.25f, -0.25f};
        int[] effect3 = {22, 44, 66, 88, 110};
        int[] effect4 = {25, 50, 75, 100, 125};
        int[] effect5 = {28, 56, 84, 112, 140};
        int[] effect6 = {20, 40, 60, 80, 100};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_BreakDamage;
            float nextBuffVars_BreakStun;
            bool nextBuffVars_Broken;
            float nextBuffVars_MoveSpeedMod;
            nextBuffVars_BreakDamage = this.effect0[level];
            nextBuffVars_BreakStun = this.effect1[level];
            nextBuffVars_Broken = false;
            AddBuff(attacker, target, new Buffs.LeblancSoulShackle(nextBuffVars_BreakDamage, nextBuffVars_BreakStun, nextBuffVars_Broken), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            ApplyDamage(attacker, target, nextBuffVars_BreakDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.5f, 1, false, false, attacker);
            nextBuffVars_MoveSpeedMod = this.effect2[level];
            AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            DestroyMissile(missileNetworkID);
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.LeblancChaosOrbM)) > 0)
            {
                ApplySilence(attacker, target, 2);
                SpellBuffRemove(target, nameof(Buffs.LeblancChaosOrbM), (ObjAIBase)owner, 0);
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level == 1)
                {
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, target, this.effect3[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.33f, 1, false, false, attacker);
                }
                else if(level == 2)
                {
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, target, this.effect4[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.375f, 1, false, false, attacker);
                }
                else
                {
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, target, this.effect5[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.42f, 1, false, false, attacker);
                }
            }
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.LeblancChaosOrb)) > 0)
            {
                ApplySilence(attacker, target, 2);
                SpellBuffRemove(target, nameof(Buffs.LeblancChaosOrb), (ObjAIBase)owner, 0);
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                ApplyDamage(attacker, target, this.effect6[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.3f, 1, false, false, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class LeblancSoulShackle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeblancShackleBeam",
            BuffTextureName = "LeblancConjureChains.dds",
            PopupMessage = new[]{ "game_floatingtext_Snared", },
        };
        float breakDamage;
        float breakStun;
        bool broken;
        Particle particleID;
        Particle soulShackleIdle;
        Particle soulShackleTarget;
        Particle soulShackleTarget_blood;
        int leblancVisionBubble;
        Region a;
        float lastTimeExecuted;
        int[] effect0 = {22, 44, 66, 88, 110};
        int[] effect1 = {25, 50, 75, 100, 125};
        int[] effect2 = {28, 56, 84, 112, 140};
        int[] effect3 = {20, 40, 60, 80, 100};
        public LeblancSoulShackle(float breakDamage = default, float breakStun = default, bool broken = default)
        {
            this.breakDamage = breakDamage;
            this.breakStun = breakStun;
            this.broken = broken;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            //RequireVar(this.breakDamage);
            //RequireVar(this.breakStun);
            //RequireVar(this.broken);
            SpellEffectCreate(out this.particleID, out _, "leBlanc_shackle_chain_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "root", default, owner, "spine", default, false, false, false, false, false);
            SpellEffectCreate(out this.soulShackleIdle, out _, "leBlanc_shackle_self_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "C_BUFFBONE_GLB_CENTER_LOC", default, attacker, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.soulShackleTarget, out _, "leBlanc_shackle_target_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.soulShackleTarget_blood, out _, "leBlanc_shackle_tar_blood.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            this.leblancVisionBubble = 0;
            teamOfOwner = GetTeamID(owner);
            if(teamOfOwner == TeamId.TEAM_BLUE)
            {
                TeamId teamOrderID;
                teamOrderID = TeamId.TEAM_BLUE;
                this.a = AddUnitPerceptionBubble(teamOrderID, 10, attacker, 2, default, default, false);
                this.leblancVisionBubble = 1;
            }
            if(teamOfOwner == TeamId.TEAM_PURPLE)
            {
                TeamId teamChaosID;
                teamChaosID = TeamId.TEAM_PURPLE;
                this.a = AddUnitPerceptionBubble(teamChaosID, 10, attacker, 2, default, default, false);
                this.leblancVisionBubble = 1;
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleID);
            SpellEffectRemove(this.soulShackleIdle);
            SpellEffectRemove(this.soulShackleTarget);
            SpellEffectRemove(this.soulShackleTarget_blood);
            if(this.leblancVisionBubble == 1)
            {
                RemovePerceptionBubble(this.a);
            }
            if(!this.broken)
            {
                int level;
                if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.LeblancChaosOrbM)) > 0)
                {
                    ApplySilence(attacker, owner, 2);
                    SpellBuffRemove(owner, nameof(Buffs.LeblancChaosOrbM), attacker, 0);
                    level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(level == 1)
                    {
                        level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, owner, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.33f, 1, false, false, attacker);
                    }
                    else if(level == 2)
                    {
                        level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, owner, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.375f, 1, false, false, attacker);
                    }
                    else
                    {
                        level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, owner, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.42f, 1, false, false, attacker);
                    }
                }
                if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.LeblancChaosOrb)) > 0)
                {
                    ApplySilence(attacker, owner, 2);
                    SpellBuffRemove(owner, nameof(Buffs.LeblancChaosOrb), attacker, 0);
                    level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, owner, this.effect3[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.3f, 1, false, false, attacker);
                }
                ApplyDamage(attacker, owner, this.breakDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.5f, 1, false, false, attacker);
                AddBuff(attacker, owner, new Buffs.LeblancSoulShackleNet(), 1, 1, this.breakStun, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                if(attacker.IsDead)
                {
                    this.broken = true;
                    SpellBuffRemove(owner, nameof(Buffs.Slow), attacker, 0);
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    if(owner.IsDead)
                    {
                        this.broken = true;
                        SpellBuffRemove(owner, nameof(Buffs.Slow), attacker, 0);
                        SpellBuffRemoveCurrent(owner);
                    }
                    else
                    {
                        float distance;
                        distance = DistanceBetweenObjects("Owner", "Attacker");
                        if(distance > 865)
                        {
                            this.broken = true;
                            SpellBuffRemove(owner, nameof(Buffs.Slow), attacker, 0);
                            SpellBuffRemoveCurrent(owner);
                        }
                    }
                }
            }
        }
    }
}