#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ViktorGravitonField : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {20, 25, 30};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 nextBuffVars_TargetPos;
            int nextBuffVars_ManaCost; // UNUSED
            targetPos = GetCastSpellTargetPos();
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_ManaCost = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.ViktorGravitonField(nextBuffVars_TargetPos), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class ViktorGravitonField : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GlacialStorm",
            BuffTextureName = "Cryophoenix_GlacialStorm.dds",
            IsDeathRecapSource = true,
            SpellToggleSlot = 4,
        };
        Vector3 targetPos;
        Particle particle;
        Particle particle2;
        float damageManaTimer;
        int[] effect0 = {6, 6, 6};
        float[] effect1 = {-0.28f, -0.32f, -0.36f, -0.4f, -0.44f};
        public ViktorGravitonField(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            TeamId teamOfOwner;
            int ownerSkinID;
            //RequireVar(this.manaCost);
            //RequireVar(this.targetPos);
            targetPos = this.targetPos;
            teamOfOwner = GetTeamID(owner);
            ownerSkinID = GetSkinID(owner);
            if(ownerSkinID == 1)
            {
                SpellEffectCreate(out this.particle, out this.particle2, "Viktor_Catalyst_Fullmachine_green.troy", "Viktor_Catalyst_Fullmachine_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            }
            else if(ownerSkinID == 2)
            {
                SpellEffectCreate(out this.particle, out this.particle2, "Viktor_Catalyst_Prototype_green.troy", "Viktor_Catalyst_Prototype_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out this.particle2, "Viktor_Catalyst_green.troy", "Viktor_Catalyst_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat;
            int level;
            TeamId ownerTeamID;
            Particle soundID; // UNUSED
            float baseCooldown;
            float multiplier;
            float newCooldown; // UNUSED
            cooldownStat = GetPercentCooldownMod(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            ownerTeamID = GetTeamID(owner);
            SpellEffectCreate(out soundID, out _, "viktor_gravitonfield_deactivate_sound.troy", default, ownerTeamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, this.targetPos, default, default, this.targetPos, true, false, false, false, false);
            baseCooldown = this.effect0[level];
            multiplier = 1 + cooldownStat;
            newCooldown = baseCooldown * multiplier;
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
        }
        public override void OnUpdateActions()
        {
            ObjAIBase caster;
            int level;
            caster = SetBuffCasterUnit();
            level = GetSlotSpellLevel(caster, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(ExecutePeriodically(0.5f, ref this.damageManaTimer, false))
            {
                float curMana; // UNUSED
                Vector3 targetPos;
                curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                targetPos = this.targetPos;
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_MovementSpeedMod;
                    nextBuffVars_MovementSpeedMod = this.effect1[level];
                    if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.ViktorGravitonFieldStun)) > 0)
                    {
                    }
                    else
                    {
                        AddBuff(attacker, unit, new Buffs.ViktorGravitonFieldDebuff(nextBuffVars_MovementSpeedMod), 100, 1, 1.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.SLOW, 0, true, false, false);
                    }
                }
            }
        }
    }
}