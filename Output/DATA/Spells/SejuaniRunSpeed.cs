#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SejuaniRunSpeed : BBBuffScript
    {
        int runAnim;
        public override void OnActivate()
        {
            this.runAnim = 1;
        }
        public override void OnUpdateActions()
        {
            float mS;
            mS = GetMovementSpeed(owner);
            if(mS >= 405)
            {
                if(this.runAnim != 3)
                {
                    this.runAnim = 3;
                    OverrideAnimation("Run", "Run3", owner);
                }
            }
            else if(mS >= 355)
            {
                if(this.runAnim != 2)
                {
                    this.runAnim = 2;
                    OverrideAnimation("Run", "Run2", owner);
                }
            }
            else
            {
                if(this.runAnim != 1)
                {
                    this.runAnim = 1;
                    ClearOverrideAnimation("Run", owner);
                }
            }
        }
    }
}