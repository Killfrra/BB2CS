#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlindMonkQOne : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "global_Watched.troy", },
            BuffName = "BlindMonkSonicWave",
            BuffTextureName = "BlindMonkQOne.dds",
        };
        Region bubbleID;
        Region bubbleID2;
        Particle slow;
        public override void OnActivate()
        {
            TeamId teamID;
            Particle hit1; // UNUSED
            Particle blood; // UNUSED
            teamID = GetTeamID(attacker);
            this.bubbleID = AddUnitPerceptionBubble(teamID, 400, owner, 20, default, default, false);
            this.bubbleID2 = AddUnitPerceptionBubble(teamID, 50, owner, 20, default, default, true);
            ApplyAssistMarker(attacker, owner, 10);
            SpellEffectCreate(out hit1, out _, "blindMonk_Q_resonatingStrike_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out blood, out _, "blindMonk_Q_resonatingStrike_tar_blood.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out this.slow, out _, "blindMonk_Q_tar_indicator.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
            RemovePerceptionBubble(this.bubbleID2);
            SpellEffectRemove(this.slow);
            if(GetBuffCountFromCaster(attacker, owner, nameof(Buffs.BlindMonkQManager)) > 0)
            {
                SpellBuffRemove(attacker, nameof(Buffs.BlindMonkQManager), (ObjAIBase)owner, 0);
            }
        }
    }
}
namespace Spells
{
    public class BlindMonkQOne : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {50, 80, 110, 140, 170};
        int[] effect1 = {0, 0, 0, 0, 0};
        int[] effect2 = {50, 80, 110, 140, 170};
        int[] effect3 = {0, 0, 0, 0, 0};
        int[] effect4 = {50, 80, 110, 140, 170};
        int[] effect5 = {0, 0, 0, 0, 0};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            bool isStealthed;
            TeamId teamID;
            float baseDamage;
            float bonusAD;
            float damageToDeal;
            Particle hit; // UNUSED
            bool canSee;
            isStealthed = GetStealthed(target);
            if(!isStealthed)
            {
                teamID = GetTeamID(attacker);
                baseDamage = this.effect0[level];
                bonusAD = GetFlatPhysicalDamageMod(owner);
                bonusAD *= 0.9f;
                damageToDeal = bonusAD + baseDamage;
                if(teamID == TeamId.TEAM_BLUE)
                {
                    AddBuff(attacker, target, new Buffs.BlindMonkQOne(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
                else
                {
                    AddBuff(attacker, target, new Buffs.BlindMonkQOneChaos(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
                ApplyDamage(attacker, target, damageToDeal + this.effect1[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
                SpellEffectCreate(out hit, out _, "blindMonk_Q_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                DestroyMissile(missileNetworkID);
                if(!target.IsDead)
                {
                    AddBuff((ObjAIBase)target, owner, new Buffs.BlindMonkQManager(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            else
            {
                if(target is Champion)
                {
                    teamID = GetTeamID(attacker);
                    baseDamage = this.effect2[level];
                    bonusAD = GetFlatPhysicalDamageMod(owner);
                    bonusAD *= 0.9f;
                    damageToDeal = bonusAD + baseDamage;
                    if(teamID == TeamId.TEAM_BLUE)
                    {
                        AddBuff(attacker, target, new Buffs.BlindMonkQOne(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    }
                    else
                    {
                        AddBuff(attacker, target, new Buffs.BlindMonkQOneChaos(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    }
                    ApplyDamage(attacker, target, damageToDeal + this.effect3[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
                    SpellEffectCreate(out hit, out _, "blindMonk_Q_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                    DestroyMissile(missileNetworkID);
                    if(!target.IsDead)
                    {
                        AddBuff((ObjAIBase)target, owner, new Buffs.BlindMonkQManager(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                else
                {
                    canSee = CanSeeTarget(owner, target);
                    if(canSee)
                    {
                        teamID = GetTeamID(attacker);
                        baseDamage = this.effect4[level];
                        bonusAD = GetFlatPhysicalDamageMod(owner);
                        bonusAD *= 0.9f;
                        damageToDeal = bonusAD + baseDamage;
                        if(teamID == TeamId.TEAM_BLUE)
                        {
                            AddBuff(attacker, target, new Buffs.BlindMonkQOne(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        }
                        else
                        {
                            AddBuff(attacker, target, new Buffs.BlindMonkQOneChaos(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                        }
                        ApplyDamage(attacker, target, damageToDeal + this.effect5[level], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
                        SpellEffectCreate(out hit, out _, "blindMonk_Q_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                        DestroyMissile(missileNetworkID);
                        if(!target.IsDead)
                        {
                            AddBuff((ObjAIBase)target, owner, new Buffs.BlindMonkQManager(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}