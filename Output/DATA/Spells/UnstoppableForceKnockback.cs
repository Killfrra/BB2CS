#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UnstoppableForceKnockback : BBBuffScript
    {
        object level;
        int[] effect0 = {50, 100, 150, 200, 250};
        public UnstoppableForceKnockback(object level = default)
        {
            this.level = level;
        }
        public override void OnActivate()
        {
            object level;
            float distance; // UNUSED
            Vector3 landingPos;
            float distanceTwo; // UNUSED
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UnstoppableForceStun)) > 0)
            {
            }
            else
            {
                //RequireVar(this.level);
                level = this.level;
                distance = DistanceBetweenObjects("Attacker", "Owner");
                landingPos = GetRandomPointInAreaUnit(owner, 310, 300);
                distanceTwo = DistanceBetweenObjectAndPoint(attacker, landingPos);
                Move(owner, landingPos, 1000, 35, 0);
                ApplyDamage(attacker, owner, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 1);
            }
        }
    }
}