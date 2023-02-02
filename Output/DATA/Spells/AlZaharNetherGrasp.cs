#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class AlZaharNetherGrasp : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 2.5f,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        Particle particleID;
        int[] effect0 = {50, 80, 110};
        public override void ChannelingStart()
        {
            float nextBuffVars_DamageToDeal;
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.IfHasBuffCheck)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AlZaharVoidlingCount(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            BreakSpellShields(target);
            AddBuff((ObjAIBase)owner, owner, new Buffs.AlZaharNetherGraspSound(), 4, 1, 2.5f, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            nextBuffVars_DamageToDeal = this.effect0[level];
            SpellEffectCreate(out this.particleID, out _, "AlzaharNetherGrasp_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, target, "root", default, false, false, false, false, false);
            AddBuff((ObjAIBase)owner, target, new Buffs.AlZaharNetherGrasp(nextBuffVars_DamageToDeal), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            AddBuff((ObjAIBase)owner, target, new Buffs.Suppression(), 100, 1, 2.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SUPPRESSION, 0, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(target, nameof(Buffs.AlZaharNetherGrasp), attacker, 0);
            SpellBuffRemove(owner, nameof(Buffs.AlZaharNetherGraspSound), (ObjAIBase)owner, 0);
            SpellEffectRemove(this.particleID);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(target, nameof(Buffs.Suppression), (ObjAIBase)owner, 0);
            SpellBuffRemove(target, nameof(Buffs.AlZaharNetherGrasp), attacker, 0);
            SpellBuffRemove(owner, nameof(Buffs.AlZaharNetherGraspSound), (ObjAIBase)owner, 0);
            SpellEffectRemove(this.particleID);
        }
    }
}
namespace Buffs
{
    public class AlZaharNetherGrasp : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "root", },
            AutoBuffActivateEffect = new[]{ "", "AlZaharNetherGrasp_tar.troy", },
            BuffName = "AlZaharNetherGrasp",
            BuffTextureName = "AlZahar_NetherGrasp.dds",
        };
        float damageToDeal;
        float ticksRemaining;
        float lastTimeExecuted;
        public AlZaharNetherGrasp(float damageToDeal = default)
        {
            this.damageToDeal = damageToDeal;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageToDeal);
            ApplyDamage(attacker, target, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.26f, 0, false, false, attacker);
            this.ticksRemaining = 4;
        }
        public override void OnUpdateActions()
        {
            float distance;
            distance = DistanceBetweenObjects("Attacker", "Owner");
            if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.Suppression)) == 0)
            {
                StopChanneling(attacker, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                SpellBuffRemoveCurrent(owner);
            }
            else if(distance >= 1500)
            {
                StopChanneling(attacker, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                SpellBuffRemoveCurrent(owner);
            }
            else if(attacker.IsDead)
            {
                StopChanneling(attacker, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                SpellBuffRemoveCurrent(owner);
            }
            else if(this.ticksRemaining > 0)
            {
                if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
                {
                    ApplyDamage(attacker, target, this.damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.26f, 0, false, false, attacker);
                    this.ticksRemaining--;
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if(!expired)
            {
                StopChanneling(attacker, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
            }
        }
    }
}