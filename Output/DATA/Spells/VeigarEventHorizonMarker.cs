#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VeigarEventHorizonMarker : BBBuffScript
    {
        float stunDuration;
        Vector3 targetPos;
        public VeigarEventHorizonMarker(float stunDuration = default, Vector3 targetPos = default)
        {
            this.stunDuration = stunDuration;
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            float speed;
            float plusBonus;
            float upperBound;
            float lowerBound;
            //RequireVar(this.stunDuration);
            //RequireVar(this.targetPos);
            targetPos = this.targetPos;
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            speed = GetMovementSpeed(owner);
            plusBonus = speed * 0.15f;
            plusBonus += 5;
            upperBound = 350 + plusBonus;
            lowerBound = 350 - plusBonus;
            if(distance >= lowerBound)
            {
                if(distance <= upperBound)
                {
                    AddBuff(attacker, owner, new Buffs.VeigarEventHorizonPrevent(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                    BreakSpellShields(owner);
                    ApplyStun(attacker, owner, this.stunDuration);
                    SpellBuffRemove(owner, nameof(Buffs.VeigarEventHorizonMarker), attacker);
                }
            }
        }
        public override void OnUpdateActions()
        {
            Vector3 targetPos;
            Vector3 ownerPos;
            float distance;
            float speed;
            float plusBonus;
            float upperBound;
            float lowerBound;
            targetPos = this.targetPos;
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, targetPos);
            speed = GetMovementSpeed(owner);
            plusBonus = speed * 0.15f;
            plusBonus += 5;
            upperBound = 350 + plusBonus;
            lowerBound = 350 - plusBonus;
            if(distance >= lowerBound)
            {
                if(distance <= upperBound)
                {
                    if(!attacker.IsDead)
                    {
                        AddBuff(attacker, owner, new Buffs.VeigarEventHorizonPrevent(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                        BreakSpellShields(owner);
                        ApplyStun(attacker, owner, this.stunDuration);
                        SpellBuffRemove(owner, nameof(Buffs.VeigarEventHorizonMarker), attacker);
                    }
                }
            }
        }
    }
}