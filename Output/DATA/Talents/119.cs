﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _119 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float moveSpeedMod;
            moveSpeedMod = 0.01f * talentLevel;
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}