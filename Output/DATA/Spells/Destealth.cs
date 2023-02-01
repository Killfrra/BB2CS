#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Destealth : BBBuffScript
    {
        public override void OnActivate()
        {
            SpellBuffClear(owner, nameof(Buffs.Stealth));
            SetStealthed(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, false);
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(type == BuffType.INVISIBILITY)
            {
                returnValue = false;
            }
            return returnValue;
        }
    }
}