#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GarenSlashBuff : BBBuffScript
    {
        public override void OnActivate()
        {
            DebugSay(owner, "Slash Buff On");
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, false);
        }
    }
}