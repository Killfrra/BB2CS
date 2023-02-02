#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TurretDamageManager : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is Champion)
            {
                int turretBuffCount;
                int targetBuffCount;
                float buffCount;
                float damageBonus;
                turretBuffCount = GetBuffCountFromCaster(owner, owner, nameof(Buffs.TurretDamageMarker));
                targetBuffCount = GetBuffCountFromCaster(target, owner, nameof(Buffs.TurretDamageMarker));
                buffCount = turretBuffCount + targetBuffCount;
                damageBonus = 0.2f * buffCount;
                damageBonus++;
                damageAmount *= damageBonus;
                if(turretBuffCount >= 3)
                {
                    AddBuff((ObjAIBase)owner, target, new Buffs.TurretDamageMarker(), 3, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false);
                }
                AddBuff((ObjAIBase)owner, owner, new Buffs.TurretDamageMarker(), 3, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false);
            }
        }
    }
}