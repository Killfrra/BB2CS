#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingDecoyFade : BBBuffScript
    {
        public override void OnActivate()
        {
            Fade iD; // UNUSED
            iD = PushCharacterFade(owner, 0.2f, 0);
            SetStealthed(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.MonkeyKingSpinToWin)) == 0)
            {
                if(GetBuffCountFromCaster(owner, default, nameof(Buffs.MonkeyKingNimbusKick)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.MonkeyKingDecoyStealth(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INVISIBILITY, 0, true, false, false);
                    DebugSay(owner, "YO!");
                }
            }
            else
            {
                PushCharacterFade(owner, 1, 0);
                SetStealthed(owner, false);
                DebugSay(owner, "NO");
            }
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, true);
        }
    }
}