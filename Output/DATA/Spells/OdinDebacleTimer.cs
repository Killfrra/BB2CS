#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinDebacleTimer : BBBuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            bool nextBuffVars_WillRemove;
            nextBuffVars_WillRemove = false;
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinDebacleCloak(nextBuffVars_WillRemove), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
        }
    }
}