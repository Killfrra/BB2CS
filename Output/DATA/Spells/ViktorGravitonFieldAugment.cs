#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ViktorGravitonFieldAugment : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GlacialStorm",
            BuffTextureName = "Cryophoenix_GlacialStorm.dds",
            SpellToggleSlot = 4,
        };
        Vector3 targetPos;
        Particle particle;
        Particle particle2;
        float damageManaTimer;
        float slowTimer;
        int[] effect0 = {6, 6, 6};
        float[] effect1 = {-0.28f, -0.32f, -0.36f, -0.4f, -0.44f};
        public ViktorGravitonFieldAugment(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            TeamId teamOfOwner;
            //RequireVar(this.manaCost);
            //RequireVar(this.targetPos);
            targetPos = this.targetPos;
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.particle, out this.particle2, "Viktor_Catalyst_green.troy", "Viktor_Catalyst_green.troy", teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat;
            int level;
            float baseCooldown;
            float multiplier;
            float newCooldown; // UNUSED
            cooldownStat = GetPercentCooldownMod(owner);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
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
            float curMana; // UNUSED
            Vector3 targetPos;
            float nextBuffVars_MovementSpeedMod;
            bool canCast;
            Vector3 ownerPos;
            float distance;
            caster = SetBuffCasterUnit();
            level = GetSlotSpellLevel(caster, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(ExecutePeriodically(0.5f, ref this.damageManaTimer, false))
            {
                curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                targetPos = this.targetPos;
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    nextBuffVars_MovementSpeedMod = this.effect1[level];
                    if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.HexMageCrystallize)) > 0)
                    {
                    }
                    else
                    {
                        AddBuff(attacker, unit, new Buffs.HexMageChainReaction(), 100, 1, 1.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.SLOW, 0, true, false, false);
                    }
                }
            }
            if(ExecutePeriodically(0.75f, ref this.slowTimer, false))
            {
                canCast = GetCanCast(owner);
                if(!canCast)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                targetPos = this.targetPos;
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                ownerPos = GetUnitPosition(owner);
                distance = DistanceBetweenPoints(ownerPos, targetPos);
                if(distance >= 1200)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}
namespace Spells
{
    public class ViktorGravitonFieldAugment : BBSpellScript
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
            int nextBuffVars_ManaCost;
            targetPos = GetCastSpellTargetPos();
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_ManaCost = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.ViktorGravitonField(nextBuffVars_TargetPos), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}