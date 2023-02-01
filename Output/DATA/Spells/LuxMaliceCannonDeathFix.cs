#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LuxMaliceCannonDeathFix : BBBuffScript
    {
        public override void OnUpdateActions()
        {
            if(attacker.IsDead)
            {
                ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, (ObjAIBase)owner);
            }
        }
    }
}