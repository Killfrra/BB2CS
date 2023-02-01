#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3082 : BBItemScript
    {
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.RanduinsOmen)) == 0)
            {
                if(RandomChance() < 0.2f)
                {
                    if(attacker is BaseTurret)
                    {
                    }
                    else
                    {
                        nextBuffVars_MoveSpeedMod = -0.35f;
                        AddBuff((ObjAIBase)owner, attacker, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false);
                        nextBuffVars_AttackSpeedMod = -0.35f;
                        AddBuff((ObjAIBase)owner, attacker, new Buffs.Cripple(nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.COMBAT_DEHANCER, 0, true, false);
                    }
                }
            }
        }
    }
}