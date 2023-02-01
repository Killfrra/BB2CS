#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ExecutionersCalling : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Executioners Calling",
            BuffTextureName = "3069_Sword_of_Light_and_Shadow.dds",
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is Champion)
            {
                AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, 7, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
            }
        }
    }
}