#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DeceiveCritBonus : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
        bool hasHit;
        float critDmgBonus;
        public DeceiveCritBonus(float critDmgBonus = default)
        {
            this.critDmgBonus = critDmgBonus;
        }
        public override void OnActivate()
        {
            SetDodgePiercing(owner, true);
            this.hasHit = false;
            //RequireVar(this.critDmgBonus);
        }
        public override void OnUpdateStats()
        {
            IncFlatCritDamageMod(owner, this.critDmgBonus);
            IncFlatCritChanceMod(owner, 1);
            if(this.hasHit)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            this.hasHit = true;
            SpellBuffRemove(owner, nameof(Buffs.DeceiveCritBonus), (ObjAIBase)owner, 0);
            ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            damageAmount *= 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SetDodgePiercing(owner, false);
        }
    }
}