#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinScoreLowHP : BBBuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            if(!owner.IsDead)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.OdinScoreSurvivor(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}