#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3138 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                int count;
                AddBuff((ObjAIBase)owner, owner, new Buffs.LeviathanCheck(), 1, 1, 1.2f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                count = GetBuffCountFromAll(owner, nameof(Buffs.LeviathanStats));
                if(count == 20)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.LeviathanCap(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
            }
        }
        public override void OnDeath()
        {
            float count;
            count = GetBuffCountFromAll(owner, nameof(Buffs.LeviathanStats));
            if(count > 0)
            {
                count *= 0.33f;
                if(count < 1.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.LeviathanStats), 1);
                }
                else if(count < 2.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.LeviathanStats), 2);
                }
                else if(count < 3.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.LeviathanStats), 3);
                }
                else if(count < 4.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.LeviathanStats), 4);
                }
                else if(count < 5.5f)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.LeviathanStats), 5);
                }
                else if(count < 7)
                {
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.LeviathanStats), 6);
                }
            }
        }
    }
}