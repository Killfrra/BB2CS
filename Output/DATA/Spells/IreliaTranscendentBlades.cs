#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class IreliaTranscendentBlades : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", "", },
            BuffName = "IreliaTranscendentBlades",
            BuffTextureName = "Irelia_TranscendentBladesReady.dds",
            NonDispellable = true,
        };
        float blades;
        float newCd;
        Particle ultMagicParticle;
        Particle particle1;
        Particle particle2;
        Particle particle3;
        Particle particle4;
        public IreliaTranscendentBlades(float blades = default, float newCd = default)
        {
            this.blades = blades;
            this.newCd = newCd;
        }
        public override void OnActivate()
        {
            TeamId ireliaTeamID;
            //RequireVar(this.blades);
            //RequireVar(this.newCd);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Location, owner);
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, -100, PrimaryAbilityResourceType.MANA);
            SpellBuffRemove(owner, nameof(Buffs.IreliaIdleParticle), (ObjAIBase)owner);
            ireliaTeamID = GetTeamID(owner);
            SpellEffectCreate(out this.ultMagicParticle, out _, "irelia_ult_magic_resist.troy", default, ireliaTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            SpellEffectCreate(out this.particle1, out _, "Irelia_ult_dagger_active_04.troy", default, ireliaTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_DAGGER1", default, target, default, default, false, default, default, false);
            SpellEffectCreate(out this.particle2, out _, "Irelia_ult_dagger_active_04.troy", default, ireliaTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_DAGGER2", default, target, default, default, false, default, default, false);
            SpellEffectCreate(out this.particle3, out _, "Irelia_ult_dagger_active_04.troy", default, ireliaTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_DAGGER4", default, target, default, default, false, default, default, false);
            SpellEffectCreate(out this.particle4, out _, "Irelia_ult_dagger_active_04.troy", default, ireliaTeamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_DAGGER5", default, target, default, default, false, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat;
            float multiplier;
            float newCooldown;
            SpellEffectRemove(this.ultMagicParticle);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.SelfAOE, owner);
            if(this.blades == 1)
            {
                SpellEffectRemove(this.particle1);
            }
            if(this.blades == 2)
            {
                SpellEffectRemove(this.particle1);
                SpellEffectRemove(this.particle3);
            }
            if(this.blades == 3)
            {
                SpellEffectRemove(this.particle1);
                SpellEffectRemove(this.particle2);
                SpellEffectRemove(this.particle3);
            }
            if(this.blades == 4)
            {
                SpellEffectRemove(this.particle1);
                SpellEffectRemove(this.particle2);
                SpellEffectRemove(this.particle3);
                SpellEffectRemove(this.particle4);
            }
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            AddBuff((ObjAIBase)owner, owner, new Buffs.IreliaIdleParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * this.newCd;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            int count;
            spellName = GetSpellName();
            if(spellName == nameof(Spells.IreliaTranscendentBlades))
            {
                this.blades += -1;
                count = GetBuffCountFromAll(owner, nameof(Buffs.IreliaTranscendentBladesSpell));
                if(count == 4)
                {
                    SpellEffectRemove(this.particle4);
                }
                if(count == 3)
                {
                    SpellEffectRemove(this.particle2);
                }
                if(count == 2)
                {
                    SpellEffectRemove(this.particle3);
                }
                if(count == 1)
                {
                    SpellEffectRemove(this.particle1);
                }
            }
        }
    }
}
namespace Spells
{
    public class IreliaTranscendentBlades : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {60, 50, 40, 0, 0};
        int[] effect1 = {10, 10, 10};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance; // UNUSED
            int count;
            float nextBuffVars_NewCd;
            float nextBuffVars_Blades;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.IreliaTranscendentBladesSpell)) > 0)
            {
                level = GetCastSpellLevelPlusOne();
                targetPos = GetCastSpellTargetPos();
                ownerPos = GetUnitPosition(owner);
                distance = DistanceBetweenPoints(ownerPos, targetPos);
                count = GetBuffCountFromAll(owner, nameof(Buffs.IreliaTranscendentBladesSpell));
                SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.5f);
                SpellBuffRemove(owner, nameof(Buffs.IreliaTranscendentBladesSpell), (ObjAIBase)owner);
                if(count <= 1)
                {
                    SpellBuffRemove(owner, nameof(Buffs.IreliaTranscendentBlades), (ObjAIBase)owner);
                }
            }
            else
            {
                nextBuffVars_NewCd = this.effect0[level];
                nextBuffVars_Blades = 4;
                AddBuff((ObjAIBase)owner, owner, new Buffs.IreliaTranscendentBlades(nextBuffVars_Blades, nextBuffVars_NewCd), 1, 1, this.effect1[level], BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.IreliaTranscendentBladesSpell(), 4, 4, 10, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                PlayAnimation("Spell4", 1.5f, owner, false, true, true);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0.25f);
            }
        }
    }
}