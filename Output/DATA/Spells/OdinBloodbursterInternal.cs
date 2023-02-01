#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinBloodbursterInternal : BBBuffScript
    {
        public override void OnActivate()
        {
            int count;
            count = GetBuffCountFromAll(owner, nameof(Buffs.OdinBloodbursterInternal));
            if(count >= 3)
            {
                SpellBuffRemoveStacks(owner, owner, nameof(Buffs.OdinBloodbursterInternal), 0);
                AddBuff(attacker, attacker, new Buffs.OdinBloodbursterBuff(), 3, 1, 7, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}