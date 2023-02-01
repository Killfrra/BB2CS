#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinGuardianBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "pelvis", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "Ferocious Howl",
            BuffTextureName = "Minotaur_FerociousHowl.dds",
        };
        TeamId myTeam;
        TeamId orderTeam;
        TeamId chaosTeam;
        Region bubbleID;
        Region bubbleID2;
        Particle particle;
        Particle particle2;
        Particle platformParticle;
        Particle platformParticle2;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            TeamId teamID;
            float health;
            float damage;
            SetGhostProof(owner, true);
            teamID = GetTeamID(owner);
            this.myTeam = GetTeamID(owner);
            this.orderTeam = TeamId.TEAM_BLUE;
            this.chaosTeam = TeamId.TEAM_PURPLE;
            this.bubbleID = AddUnitPerceptionBubble(this.orderTeam, 800, owner, 25000, default, default, true);
            this.bubbleID2 = AddUnitPerceptionBubble(this.chaosTeam, 800, owner, 25000, default, default, true);
            if(teamID == TeamId.TEAM_NEUTRAL)
            {
                health = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
                damage = health * -0.5f;
                IncPAR(owner, damage, PrimaryAbilityResourceType.MANA);
            }
            if(teamID == TeamId.TEAM_BLUE)
            {
                SpellEffectCreate(out this.particle, out this.particle2, "OdinNeutralGuardian_Green.troy", "OdinNeutralGuardian_Red.troy", TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, "crystal", default, owner, default, default, false, true, false, false, false);
                SpellEffectCreate(out this.platformParticle, out _, "blank.troy", default, TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.platformParticle2, out _, "blank.troy", default, TeamId.TEAM_PURPLE, 0, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else if(teamID == TeamId.TEAM_PURPLE)
            {
                SpellEffectCreate(out this.particle, out this.particle2, "OdinNeutralGuardian_Green.troy", "OdinNeutralGuardian_Red.troy", TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "crystal", default, owner, default, default, false, true, false, false, false);
                SpellEffectCreate(out this.platformParticle, out _, "blank.troy", default, TeamId.TEAM_PURPLE, 0, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.platformParticle2, out _, "blank.troy", default, TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "OdinNeutralGuardian_Stone.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, "crystal", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.particle2, out _, "OdinNeutralGuardian_Stone.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "crystal", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.platformParticle, out _, "blank.troy", default, TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out this.platformParticle2, out _, "blank.troy", default, TeamId.TEAM_PURPLE, 0, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            RemovePerceptionBubble(this.bubbleID);
            if(teamID == TeamId.TEAM_NEUTRAL)
            {
                RemovePerceptionBubble(this.bubbleID2);
            }
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            SpellEffectRemove(this.platformParticle);
            SpellEffectRemove(this.platformParticle2);
        }
        public override void OnUpdateStats()
        {
            int nextBuffVars_MagicResistBuff;
            int nextBuffVars_ArmorBuff;
            TeamId currentTeam;
            Particle asdf; // UNUSED
            TeamId teamID;
            SetInvulnerable(owner, true);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 900, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectNotPet | SpellDataFlags.NotAffectSelf, default, true))
            {
                if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.OdinSuperMinion)) > 0)
                {
                    nextBuffVars_MagicResistBuff = 0;
                    nextBuffVars_ArmorBuff = 0;
                    AddBuff((ObjAIBase)owner, unit, new Buffs.OdinMinionTaunt(nextBuffVars_MagicResistBuff, nextBuffVars_ArmorBuff), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                }
                if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.OdinMinion)) > 0)
                {
                    nextBuffVars_MagicResistBuff = 0;
                    nextBuffVars_ArmorBuff = 0;
                    AddBuff((ObjAIBase)owner, unit, new Buffs.OdinMinionTaunt(nextBuffVars_MagicResistBuff, nextBuffVars_ArmorBuff), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
            currentTeam = GetTeamID(owner);
            if(currentTeam != this.myTeam)
            {
                SpellEffectCreate(out asdf, out _, "GoldAquisition_glb.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, false, false, false, false);
                RemovePerceptionBubble(this.bubbleID);
                if(this.myTeam == TeamId.TEAM_NEUTRAL)
                {
                    RemovePerceptionBubble(this.bubbleID2);
                }
                teamID = GetTeamID(owner);
                this.myTeam = currentTeam;
                SpellEffectRemove(this.particle);
                SpellEffectRemove(this.particle2);
                SpellEffectRemove(this.platformParticle);
                SpellEffectRemove(this.platformParticle2);
                if(this.myTeam == TeamId.TEAM_BLUE)
                {
                    PlayAnimation("Activate", 0, owner, false, true, false);
                    OverrideAnimation("Idle1", "Floating", owner);
                    SpellEffectCreate(out this.particle, out this.particle2, "OdinNeutralGuardian_Green.troy", "OdinNeutralGuardian_Red.troy", TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, "crystal", default, owner, default, default, false, true, false, false, false);
                    SpellEffectCreate(out this.platformParticle, out _, "blank.troy", default, TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    SpellEffectCreate(out this.platformParticle2, out _, "blank.troy", default, TeamId.TEAM_PURPLE, 0, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    this.bubbleID = AddUnitPerceptionBubble(teamID, 800, owner, 25000, default, default, true);
                }
                else if(this.myTeam == TeamId.TEAM_PURPLE)
                {
                    PlayAnimation("Activate", 0, owner, false, true, false);
                    OverrideAnimation("Idle1", "Floating", owner);
                    SpellEffectCreate(out this.particle, out this.particle2, "OdinNeutralGuardian_Green.troy", "OdinNeutralGuardian_Red.troy", TeamId.TEAM_PURPLE, 0, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "crystal", default, owner, default, default, false, true, false, false, false);
                    SpellEffectCreate(out this.platformParticle, out _, "blank.troy", default, TeamId.TEAM_PURPLE, 0, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    SpellEffectCreate(out this.platformParticle2, out _, "blank.troy", default, TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    this.bubbleID = AddUnitPerceptionBubble(teamID, 800, owner, 25000, default, default, true);
                }
                else
                {
                    PlayAnimation("Deactivate", 0, owner, false, false, false);
                    ClearOverrideAnimation("Idle1", owner);
                    SpellEffectCreate(out this.particle, out _, "OdinNeutralGuardian_Stone.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, "crystal", default, owner, default, default, false, false, false, false, false);
                    SpellEffectCreate(out this.particle2, out _, "OdinNeutralGuardian_Stone.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, "crystal", default, owner, default, default, false, false, false, false, false);
                    SpellEffectCreate(out this.platformParticle, out _, "blank.troy", default, TeamId.TEAM_BLUE, 0, 0, TeamId.TEAM_BLUE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    SpellEffectCreate(out this.platformParticle2, out _, "blank.troy", default, TeamId.TEAM_PURPLE, 0, 0, TeamId.TEAM_PURPLE, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    this.bubbleID = AddUnitPerceptionBubble(this.orderTeam, 800, owner, 25000, default, default, true);
                    this.bubbleID2 = AddUnitPerceptionBubble(this.chaosTeam, 800, owner, 25000, default, default, true);
                }
            }
            IncPercentArmorPenetrationMod(owner, 0.65f);
        }
        public override void OnUpdateActions()
        {
            int count1;
            int count2;
            float count;
            TeamId teamID;
            float healAmount;
            float maxHealth;
            float halfHealth;
            float curHealth;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                count1 = GetBuffCountFromAll(owner, nameof(Buffs.OdinGuardianSuppression));
                count2 = GetBuffCountFromAll(owner, nameof(Buffs.OdinMinionSpellAttack));
                count = count1 + count2;
                if(count == 0)
                {
                    teamID = GetTeamID(owner);
                    healAmount = 300;
                    if(teamID == TeamId.TEAM_NEUTRAL)
                    {
                        maxHealth = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
                        halfHealth = maxHealth * 0.5f;
                        curHealth = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                        healAmount = halfHealth - curHealth;
                        healAmount = Math.Min(healAmount, 150);
                        healAmount = Math.Max(healAmount, -150);
                    }
                    IncPAR(owner, healAmount, PrimaryAbilityResourceType.MANA);
                }
            }
        }
    }
}