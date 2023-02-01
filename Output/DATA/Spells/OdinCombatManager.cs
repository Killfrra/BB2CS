#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinCombatManager : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(target.Team != owner.Team)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                if(target != owner)
                {
                }
            }
            if(GetBuffCountFromCaster(target, target, nameof(Buffs.OdinCombatActive)) > 0)
            {
                if(target != owner)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(target != owner)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnKill()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnMiss()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            if(GetBuffCountFromCaster(target, target, nameof(Buffs.OdinCombatActive)) > 0)
            {
                if(target != owner)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.OdinCombatActive(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            return returnValue;
        }
    }
}