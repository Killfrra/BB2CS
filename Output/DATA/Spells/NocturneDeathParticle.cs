﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NocturneDeathParticle : BBBuffScript
    {
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "NocturneDeath.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false);
        }
    }
}