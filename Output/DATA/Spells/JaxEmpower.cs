#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class JaxEmpower : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {25, 35, 45, 55, 65};
        public override void SelfExecute()
        {
            float nextBuffVars_DamagePerStack;
            nextBuffVars_DamagePerStack = this.effect0[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.JaxEmpower(nextBuffVars_DamagePerStack), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class JaxEmpower : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "EmpowerCleave",
            BuffTextureName = "Armsmaster_Empower.dds",
        };
        Particle particle;
        float damagePerStack;
        float lastTimeExecuted;
        public JaxEmpower(float damagePerStack = default)
        {
            this.damagePerStack = damagePerStack;
        }
        public override void OnActivate()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.JaxRelentlessAssaultMarker)) > 0)
            {
            }
            else
            {
                OverrideAutoAttack(1, SpellSlotType.ExtraSlots, owner, 1, false);
            }
            SpellEffectCreate(out this.particle, out _, "Empower_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "weaponstreak", default, owner, default, default, false, false, false, false, false);
            //RequireVar(this.damagePerStack);
        }
        public override void OnDeactivate(bool expired)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.JaxRelentlessAssaultMarker)) > 0)
            {
                OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, false);
            }
            else
            {
                RemoveOverrideAutoAttack(owner, false);
            }
            SpellEffectRemove(this.particle);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.JaxEmpowerSeal(), 3, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            Particle part; // UNUSED
            int count;
            float damageBonus;
            float radiusOfCleave;
            float aoEDamage;
            SpellEffectCreate(out part, out _, "TiamatMelee_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
            count = GetBuffCountFromAll(owner, nameof(Buffs.EmpowerCleave));
            damageBonus = this.damagePerStack * count;
            radiusOfCleave = 125 * count;
            aoEDamage = damageAmount * 0.6f;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, radiusOfCleave, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(target != unit)
                {
                    ApplyDamage(attacker, unit, aoEDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                }
            }
            ApplyDamage(attacker, target, damageBonus, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            SpellBuffRemove(owner, nameof(Buffs.JaxEmpower), (ObjAIBase)owner, 0);
            SpellBuffRemoveStacks(owner, owner, nameof(Buffs.JaxEmpowerSeal), 0);
        }
    }
}