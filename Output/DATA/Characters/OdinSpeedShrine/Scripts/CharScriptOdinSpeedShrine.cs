﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptOdinSpeedShrine : BBCharScript
    {
        public override void OnActivate()
        {
            SetInvulnerable(owner, true);
            SetRooted(owner, true);
            SetTargetable(owner, false);
            SetNoRender(owner, false);
            SetCanMove(owner, false);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinSpeedShrineAura(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}