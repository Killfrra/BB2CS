#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RenektonPreExecute : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", "", },
            AutoBuffActivateEffect = new[]{ "Renekton_Weapon_Hot.troy", "", "", },
            BuffName = "RenektonExecuteReady",
            BuffTextureName = "Renekton_Execute.dds",
            SpellToggleSlot = 2,
        };
        float spellCooldown;
        bool swung;
        public RenektonPreExecute(float spellCooldown = default)
        {
            this.spellCooldown = spellCooldown;
        }
        public override void OnActivate()
        {
            int level; // UNUSED
            //RequireVar(this.spellCooldown);
            //RequireVar(this.bonusDamage);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            CancelAutoAttack(owner, true);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.swung = false;
        }
        public override void OnDeactivate(bool expired)
        {
            float spellCooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            TeamId ownerVar;
            Particle temp; // UNUSED
            spellCooldown = this.spellCooldown;
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * spellCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            ownerVar = GetTeamID(owner);
            if(!this.swung)
            {
                SpellEffectCreate(out temp, out _, "Renekton_RuthlessPredator_obd-sound.troy", default, ownerVar, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
            }
        }
        public override void OnPreAttack()
        {
            int level;
            float ragePercent;
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    SetDodgePiercing(owner, true);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    SkipNextAutoAttack(owner);
                    ragePercent = GetPARPercent(owner, PrimaryAbilityResourceType.Other);
                    if(ragePercent >= 0.5f)
                    {
                        SpellCast((ObjAIBase)owner, target, default, default, 1, SpellSlotType.ExtraSlots, level, false, false, false, false, true, false);
                        AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonUnlockAnimation(), 1, 1, 0.76f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        this.swung = true;
                    }
                    else
                    {
                        SpellCast((ObjAIBase)owner, target, default, default, 0, SpellSlotType.ExtraSlots, level, false, false, false, false, true, false);
                        AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonUnlockAnimation(), 1, 1, 0.51f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        this.swung = true;
                    }
                    SpellBuffRemove(owner, default, (ObjAIBase)owner, 0);
                }
            }
        }
    }
}
namespace Spells
{
    public class RenektonPreExecute : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {13, 12, 11, 10, 9};
        int[] effect1 = {35, 70, 105, 140, 175};
        public override void SelfExecute()
        {
            int nextBuffVars_SpellCooldown;
            int nextBuffVars_BonusDamage;
            float ragePercent; // UNUSED
            SetSlotSpellCooldownTimeVer2(0, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            nextBuffVars_SpellCooldown = this.effect0[level];
            nextBuffVars_BonusDamage = this.effect1[level];
            ragePercent = GetPAR(owner, PrimaryAbilityResourceType.Other);
            AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonPreExecute(nextBuffVars_SpellCooldown), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}