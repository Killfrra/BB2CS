#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SwainBeamTransition : BBBuffScript
    {
        int casterID;
        public override void OnActivate()
        {
            this.casterID = PushCharacterData("SwainNoBird", owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            PopCharacterData(owner, this.casterID);
        }
    }
}