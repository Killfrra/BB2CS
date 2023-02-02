#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickReviveAllySelf : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "yorick_ult_revive_tar.troy", "", },
            BuffName = "YorickOmenPreDeath",
            BuffTextureName = "YorickOmenOfDeath.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        bool isKarthus;
        bool isKogMaw;
        Particle particle3;
        Particle particle4;
        Particle particle;
        public override void OnActivate()
        {
            TeamId teamID;
            this.isKarthus = false;
            this.isKogMaw = false;
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out this.particle3, out this.particle4, "yorick_ult_01_teamID_green.troy", "yorick_ult_01_teamID_red.troy", teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.particle, out this.particle, "yorick_ult_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 500, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DeathDefied)) > 0)
            {
                this.isKarthus = true;
                SpellBuffRemove(owner, nameof(Buffs.DeathDefied), (ObjAIBase)owner, 0);
            }
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.KogMawIcathianSurpriseReady)) > 0)
            {
                this.isKogMaw = true;
                SpellBuffRemove(owner, nameof(Buffs.KogMawIcathianSurpriseReady), (ObjAIBase)owner, 0);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle3);
            SpellEffectRemove(this.particle4);
            if(expired)
            {
                if(this.isKarthus)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.DeathDefied(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
                if(this.isKogMaw)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.KogMawIcathianSurpriseReady(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
            else
            {
                if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickRADelayLich)) == 0)
                {
                    if(GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickRADelayKogMaw)) == 0)
                    {
                        if(this.isKarthus)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.DeathDefied(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                        if(this.isKogMaw)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.KogMawIcathianSurpriseReady(), 1, 1, 30000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                    }
                }
            }
        }
        public override void OnDeath()
        {
            if(owner is Champion)
            {
                bool becomeZombie; // UNUSED
                becomeZombie = true;
            }
        }
        public override void OnZombie()
        {
            TeamId teamID;
            AddBuff((ObjAIBase)owner, attacker, new Buffs.YorickUltPetActive(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffRemoveType(owner, BuffType.POISON);
            SpellBuffRemoveType(owner, BuffType.COMBAT_DEHANCER);
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.INVISIBILITY);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.DAMAGE);
            SpellBuffRemoveType(owner, BuffType.SPELL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.PHYSICAL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.INVULNERABILITY);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.COMBAT_ENCHANCER);
            if(this.isKarthus)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.YorickRADelayLich(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else if(this.isKogMaw)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.YorickRADelayKogMaw(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.YorickRADelay(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            teamID = GetTeamID(owner);
            if(teamID == TeamId.TEAM_BLUE)
            {
                foreach(Champion unit in GetChampions(TeamId.TEAM_BLUE, nameof(Buffs.YorickRARemovePet), true))
                {
                    SpellBuffClear(unit, nameof(Buffs.YorickRARemovePet));
                }
            }
            else
            {
                foreach(Champion unit in GetChampions(TeamId.TEAM_PURPLE, nameof(Buffs.YorickRARemovePet), true))
                {
                    SpellBuffClear(unit, nameof(Buffs.YorickRARemovePet));
                }
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRADelay)) > 0)
            {
                damageAmount = 0;
            }
        }
    }
}