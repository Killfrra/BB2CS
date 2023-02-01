#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SkarnerBrushCheck : BBBuffScript
    {
        float brushChecks;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            this.brushChecks = 0;
        }
        public override void OnUpdateActions()
        {
            bool brushCheck;
            if(ExecutePeriodically(3, ref this.lastTimeExecuted, false))
            {
                brushCheck = IsInBrush(owner);
                if(brushCheck)
                {
                    if(this.brushChecks == 12)
                    {
                        if(RandomChance() < 0.05f)
                        {
                            AddBuff((ObjAIBase)owner, owner, new Buffs.SkarnerBrushSound(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                            SpellBuffClear(owner, nameof(Buffs.SkarnerBrushCheck));
                        }
                        else
                        {
                            this.brushChecks = 0;
                        }
                    }
                    else
                    {
                        this.brushChecks += 3;
                    }
                }
                else
                {
                    this.brushChecks = 0;
                }
            }
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            this.brushChecks = 0;
        }
    }
}