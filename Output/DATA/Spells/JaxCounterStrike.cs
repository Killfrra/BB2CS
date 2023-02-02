#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class JaxCounterStrike : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {0, 0, 0, 0, 0};
        float[] effect1 = {1.5f, 1.5f, 1.5f, 1.5f, 1.5f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            PlayAnimation("Spell3", 0, owner, false, false, false);
            charVars.NumCounter = this.effect0[level];
            AddBuff(attacker, target, new Buffs.JaxCounterStrike(), 1, 1, this.effect1[level], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class JaxCounterStrike : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "JaxDodger.troy", },
            BuffName = "JaxEvasion",
            BuffTextureName = "Armsmaster_Disarm.dds",
            OnPreDamagePriority = 3,
        };
        Particle particle;
        int[] effect0 = {10, 15, 20, 25, 30};
        int[] effect1 = {10, 15, 20, 25, 30};
        public override void OnActivate()
        {
            TeamId teamID; // UNITIALIZED
            SpellEffectCreate(out this.particle, out _, "JaxDodger.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, "head", default, false, false, false, false, false);
            //RequireVar(this.movementSpeedBonusPercent);
            charVars.NumCounter = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            if(!owner.IsDead)
            {
                SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 3, SpellSlotType.ExtraSlots, 0, false, true, false, false, false, false);
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatDodgeMod(owner, 100);
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(attacker is Champion)
            {
            }
            if(damageType == DamageSource.DAMAGE_SOURCE_ATTACK)
            {
                DebugSay(owner, "YO!");
            }
        }
        public override void OnDodge()
        {
            int level;
            float minionBonus;
            float championBonus;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            minionBonus = this.effect0[level];
            championBonus = this.effect1[level];
            if(target is Champion)
            {
                charVars.NumCounter += championBonus;
            }
            else
            {
                charVars.NumCounter += minionBonus;
            }
        }
    }
}