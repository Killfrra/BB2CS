#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3010 : BBItemScript
    {
        int ownerLevel;
        public override void OnUpdateActions()
        {
            int tempLevel;
            tempLevel = GetLevel(owner);
            if(tempLevel > this.ownerLevel)
            {
                AddBuff(attacker, target, new Buffs.CatalystHeal(), 1, 1, 8.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                this.ownerLevel = tempLevel;
            }
        }
        public override void OnActivate()
        {
            this.ownerLevel = GetLevel(owner);
        }
    }
}