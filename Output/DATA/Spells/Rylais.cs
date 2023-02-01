#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Rylais : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Rylais",
            BuffTextureName = "3022_Frozen_Heart.dds",
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float nextBuffVars_MovementSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    if(target is ObjAIBase)
                    {
                        if(target is BaseTurret)
                        {
                        }
                        else
                        {
                            nextBuffVars_MovementSpeedMod = -0.35f;
                            nextBuffVars_AttackSpeedMod = 0;
                            AddBuff(attacker, target, new Buffs.Chilled(nextBuffVars_MovementSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 2.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0);
                        }
                    }
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}