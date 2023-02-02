#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class YorickRARevive : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 pos;
            Pet other1;
            float temp1;
            pos = GetUnitPosition(target);
            other1 = CloneUnitPet(target, nameof(Buffs.YorickRARevive), 0, pos, 0, 0, false);
            temp1 = GetMaxHealth(other1, PrimaryAbilityResourceType.MANA);
            IncHealth(other1, temp1, other1);
            AddBuff((ObjAIBase)owner, other1, new Buffs.YorickRAPetBuff2(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(other1, owner, new Buffs.YorickRARemovePet(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class YorickRARevive : BBBuffScript
    {
    }
}