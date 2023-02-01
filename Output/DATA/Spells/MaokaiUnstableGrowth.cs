#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MaokaiUnstableGrowth : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaokaiUnstableGrowth",
            BuffTextureName = "XenZhao_CrescentSweepNew.dds",
        };
        float baseDamage;
        float rootDuration;
        Particle particle;
        Region unitPerceptionBubble;
        int[] effect0 = {30, 45, 60, 75, 90};
        public MaokaiUnstableGrowth(float baseDamage = default, float rootDuration = default)
        {
            this.baseDamage = baseDamage;
            this.rootDuration = rootDuration;
        }
        public override void OnActivate()
        {
            ObjAIBase caster;
            TeamId teamOfOwner;
            caster = SetBuffCasterUnit();
            //RequireVar(this.baseDamage);
            //RequireVar(this.rootDuration);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            SetForceRenderParticles(owner, true);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            PlayAnimation("Spell2c", 0, owner, true, true, true);
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.particle, out _, "maokai_elementalAdvance_mis.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            this.unitPerceptionBubble = AddUnitPerceptionBubble(teamOfOwner, 10, caster, 5, default, caster, false);
            MoveToUnit(owner, caster, 1300, 0, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, 0, 2000, 0, 0);
        }
        public override void OnDeactivate(bool expired)
        {
            ObjAIBase caster; // UNUSED
            int level;
            int nextBuffVars_DefensiveBonus;
            RemovePerceptionBubble(this.unitPerceptionBubble);
            SetTargetable(owner, true);
            SetGhosted(owner, false);
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
            SetForceRenderParticles(owner, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            if(!owner.IsDead)
            {
                UnlockAnimation(owner, false);
                PlayAnimation("Spell2b", 0.25f, owner, false, true, false);
            }
            SpellEffectRemove(this.particle);
            caster = SetBuffCasterUnit();
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_DefensiveBonus = this.effect0[level];
            StopMoveBlock(owner);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster;
            float baseDamage;
            caster = SetBuffCasterUnit();
            baseDamage = this.baseDamage;
            BreakSpellShields(caster);
            ApplyDamage((ObjAIBase)owner, caster, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 0, false, false, (ObjAIBase)owner);
            AddBuff((ObjAIBase)owner, caster, new Buffs.MaokaiUnstableGrowthRoot(), 1, 1, this.rootDuration, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, true, false);
            SpellBuffRemoveCurrent(owner);
            if(caster is Champion)
            {
                if(caster.Team != owner.Team)
                {
                    IssueOrder(owner, OrderType.AttackTo, default, caster);
                }
            }
        }
    }
}
namespace Spells
{
    public class MaokaiUnstableGrowth : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {80, 115, 150, 185, 220};
        float[] effect1 = {1, 1.25f, 1.5f, 1.75f, 2};
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove;
            bool canCast;
            canMove = GetCanMove(owner);
            canCast = GetCanCast(owner);
            if(!canMove)
            {
                returnValue = false;
            }
            if(!canCast)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nextBuffVars_BaseDamage;
            float nextBuffVars_RootDuration;
            nextBuffVars_BaseDamage = this.effect0[level];
            nextBuffVars_RootDuration = this.effect1[level];
            AddBuff((ObjAIBase)target, owner, new Buffs.MaokaiUnstableGrowth(nextBuffVars_BaseDamage, nextBuffVars_RootDuration), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}