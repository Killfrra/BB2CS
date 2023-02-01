﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class _332 : BBCharScript
    {
        public override void OnUpdateStats()
        {
            float healthMod;
            healthMod = 30 * talentLevel;
            IncFlatHPPoolMod(owner, healthMod);
        }
    }
}